using System.Text;
using System.IO;
using System.Security.Cryptography;
using System;

namespace UtilHelper.Security
{
    #region 对称加密
    /// <summary>
    /// 对称加密基类,默认字符编码为UTF-8
    /// </summary>
    internal abstract class BaseSymmetricAlgorithm : IDCUtil
    {
        protected byte[] m_Key;
        protected byte[] m_IV;
        protected System.Security.Cryptography.SymmetricAlgorithm m_SymAlg;
        public abstract void SetKey(string sKey);

        public byte[] EncryptString(string sToEncrypt)
        {
            return EncryptString(sToEncrypt, InteralUtil.ISOEncoding);
        }
        public string DecryptString(byte[] bzToDecrypt)
        {
            return DecryptString(bzToDecrypt, InteralUtil.ISOEncoding);
        }
        public string DecryptFromFile(string path)
        {
            return DecryptFromFile(path, InteralUtil.ISOEncoding);
        }
        public bool EncryptToFile(string path, string value)
        {
            return EncryptToFile(path, value, InteralUtil.ISOEncoding);
        }

        #region ISecurityUtil 成员

        public byte[] EncryptString(string sToEncrypt, Encoding encoding)
        {
            byte[] bOri = encoding.GetBytes(sToEncrypt);
            return Encrypt(bOri);
        }
        public string DecryptString(byte[] bzToDecrypt, Encoding encoding)
        {
            byte[] bzRst = Decrypt(bzToDecrypt);
            return encoding.GetString(bzRst).TrimEnd('\0');
        }

        public string DecryptFromFile(string path, Encoding encoding)
        {
            FileStream fs = null;
            BinaryReader br = null;
            string dec = string.Empty;
            try
            {
                fs = File.Open(path, FileMode.Open);
                br = new BinaryReader(fs, encoding);

                byte[] toDec = br.ReadBytes((int)fs.Length);

                dec = DecryptString(toDec,encoding);
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                System.Windows.Forms.MessageBox.Show("非法加密数据", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                if (br != null)
                    br.Close();
            }
            return dec.Trim();
        }
        public bool EncryptToFile(string path, string value, Encoding encoding)
        {
            byte[] enc = EncryptString(value,encoding);
            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                fs = File.Open(path, FileMode.Create);
                bw = new BinaryWriter(fs, encoding);
                bw.Write(enc);
            }
            catch (System.Exception)
            {
                //throw ex;
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                if (bw != null)
                    bw.Close();
            }
            return true;
        }

        public byte[] Encrypt(byte[] bzToEncrypt)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] bzToRtn = null;
            try
            {
                ICryptoTransform encKey = m_SymAlg.CreateEncryptor(m_Key, m_IV);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, encKey, CryptoStreamMode.Write);
                cs.Write(bzToEncrypt, 0, bzToEncrypt.Length);
                cs.FlushFinalBlock();
                bzToRtn = ms.ToArray();
            }
            catch (System.Exception)
            {
                return null;
            }
            finally
            {
                if (cs != null)
                    cs.Close();
                if (ms != null)
                    ms.Close();
            }

            return bzToRtn;
        }
        public byte[] Decrypt(byte[] bzToDecrypt)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] bzRst = null;
            try
            {
                ICryptoTransform decKey = m_SymAlg.CreateDecryptor(m_Key, m_IV);
                ms = new MemoryStream(bzToDecrypt);
                cs = new CryptoStream(ms, decKey, CryptoStreamMode.Read);
                bzRst = new byte[bzToDecrypt.Length];
                cs.Read(bzRst, 0, bzToDecrypt.Length);
            }
            catch (System.Exception)
            {
                //throw ex;
                return null;
            }
            finally
            {
                if (cs != null)
                    cs.Close();
                if (ms != null)
                    ms.Close();
            }
            return bzRst;
        }
        #endregion
    }
    #endregion

    #region Sign(数字签名)
    internal class BaseSignUtil : ISignUtil
    {
        private string m_sHashName;
        public BaseSignUtil(string sHashName)
        {
            m_sHashName = sHashName;
        }
        #region ISignUtil 成员
        public string Sign(string sSrc)
        {
            byte[] data = null;
            data = Sign(InteralUtil.ISOEncoding.GetBytes(sSrc));
            return FormatData(data);
        }
        public bool Verify(string sSrc, string sHash)
        {
            string sHashValue = Sign(sSrc);
            System.StringComparer comparer = System.StringComparer.Ordinal;
            return (0 == comparer.Compare(sHashValue, sHash));
        }
        /// <summary>
        /// 格式化数据，默认不进行格式化.
        /// </summary>
        /// <param name="sData">需要格式化的数据.</param>
        /// <returns></returns>
        protected virtual string FormatData(byte[] bzData)
        {
            return InteralUtil.ISOEncoding.GetString(bzData);
        }
        public byte[] Sign(byte[] bSrc)
        {
            System.Security.Cryptography.HashAlgorithm Hasher =
                System.Security.Cryptography.HashAlgorithm.Create(m_sHashName);
            byte[] data = null;
            try
            {
                data = Hasher.ComputeHash(bSrc);
            }
            catch (System.ArgumentNullException)//sSrc为空
            {
                data = null;
            }

            Hasher.Clear();
            return data;
        }

        public bool Verify(byte[] bSrc, byte[] bHash)
        {
            byte[] bHashValue = Sign(bSrc);
            System.StringComparer comparer = System.StringComparer.Ordinal;
            string sHashValue = InteralUtil.ISOEncoding.GetString(bHashValue);
            string sHash = InteralUtil.ISOEncoding.GetString(bHash);
            return (0 == comparer.Compare(sHashValue, sHash));
        }
        #endregion
    }
    internal class MD5Util : BaseSignUtil
    {
        public MD5Util() : base("MD5") { }
        protected override string FormatData(byte[] bzData)
        {
            StringBuilder sBuilder = new StringBuilder(64);
            foreach (byte b in bzData)
                sBuilder.Append(b.ToString("X2"));//转换成十六进制
            return sBuilder.ToString();
        }
    }

    internal class DSAUtil : ISignUtil, IAsymVisior
    {
        private DSACryptoServiceProvider _provider;
        public DSAUtil()
        {
            _provider = new DSACryptoServiceProvider();
        }
        #region ISignUtil 成员

        public string Sign(string sSrc)
        {
            byte[] bSrc = InteralUtil.ISOEncoding.GetBytes(sSrc);
            byte[] bSign = Sign(bSrc);
            return InteralUtil.ISOEncoding.GetString(bSign);
        }

        public bool Verify(string sSrc, string sSign)
        {
            byte[] bSrc = InteralUtil.ISOEncoding.GetBytes(sSrc);
            byte[] bSign = InteralUtil.ISOEncoding.GetBytes(sSign);
            return Verify(bSrc, bSign);
        }

        public byte[] Sign(byte[] bSrc)
        {
            return _provider.SignData(bSrc);
        }

        public bool Verify(byte[] bSrc, byte[] bSign)
        {
            return _provider.VerifyData(bSrc, bSign);
        }
        
        #endregion

        #region IAsymVisior 成员

        AsymmetricAlgorithm IAsymVisior.Provider
        {
            get
            {
                return _provider;
            }
            set
            {
                _provider = value as DSACryptoServiceProvider;
            }
        }

        byte[] IAsymVisior.PrivateKey
        {
            get { return _provider.ExportCspBlob(true); }
            set { _provider.ImportCspBlob(value); }
        }

        byte[] IAsymVisior.PublicKey
        {
            get { return _provider.ExportCspBlob(false); }
            set { _provider.ImportCspBlob(value); }
        }

        #endregion
    }
    #endregion

    #region 非对称加密
    class RSAImpl : IDCUtil,IAsymVisior,ISignUtil
    {
        private RSACryptoServiceProvider _provider;
        public RSAImpl()
        {
            _provider = new RSACryptoServiceProvider();
        }

        #region IDCUtil 成员

        public byte[] EncryptString(string sToEncrypt)
        {
            return EncryptString(sToEncrypt, InteralUtil.ISOEncoding);
        }

        public string DecryptString(byte[] bzToDecrypt)
        {
            return DecryptString(bzToDecrypt, InteralUtil.ISOEncoding);
        }

        public byte[] EncryptString(string sToEncrypt, Encoding encoding)
        {
            byte[] bSrc = encoding.GetBytes(sToEncrypt);
            return Encrypt(bSrc);
        }

        public string DecryptString(byte[] bzToDecrypt, Encoding encoding)
        {
            byte[] szDec = Decrypt(bzToDecrypt);
            return encoding.GetString(szDec);
        }
        public byte[] Encrypt(byte[] bzToEncrypt)
        {
            return _provider.Encrypt(bzToEncrypt, false);
        }

        public byte[] Decrypt(byte[] bzToDecrypt)
        {
           return  _provider.Decrypt(bzToDecrypt, false);
        }
        #region 非对称加密不支持文件操作

        public string DecryptFromFile(string path)
        {
            throw new NotImplementedException("非对称加密不适合于大量数据加密");
        }

        public bool EncryptToFile(string path, string value)
        {
            throw new NotImplementedException("非对称加密不适合于大量数据加密");
        }
        public string DecryptFromFile(string path, Encoding encoding)
        {
            throw new NotImplementedException("非对称加密不适合于大量数据加密");
        }

        public bool EncryptToFile(string path, string value, Encoding encoding)
        {
            throw new NotImplementedException("非对称加密不适合于大量数据加密");
        }
    #endregion
        #endregion
        #region IAsymVisior 成员

        AsymmetricAlgorithm IAsymVisior.Provider
        {
            get { return _provider; }
            set { _provider = value as RSACryptoServiceProvider; }
        }

        byte[] IAsymVisior.PrivateKey
        {
            get { return _provider.ExportCspBlob(true); }
            set { _provider.ImportCspBlob(value); }
        }

        byte[] IAsymVisior.PublicKey
        {
            get { return _provider.ExportCspBlob(false); }
            set { _provider.ImportCspBlob(value); }
        }
        #endregion

        #region ISignUtil 成员

        public string Sign(string sSrc)
        {
            byte[] bSrc = InteralUtil.ISOEncoding.GetBytes(sSrc);
            byte[] bSign = Sign(bSrc);
            return InteralUtil.ISOEncoding.GetString(bSign);
        }

        public bool Verify(string sSrc, string sSign)
        {
            byte[] bSrc = InteralUtil.ISOEncoding.GetBytes(sSrc);
            byte[] bSign = InteralUtil.ISOEncoding.GetBytes(sSign);
            return Verify(bSrc, bSign);
        }

        public byte[] Sign(byte[] bSrc)
        {
            return _provider.SignData(bSrc,"SHA1");
        }

        public bool Verify(byte[] bSrc, byte[] bSign)
        {
            return _provider.VerifyData(bSrc, "SHA1",bSign);
        }
        #endregion
    }
    #endregion
}