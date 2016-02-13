using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace UtilHelper.Security
{



    #region 对称加密
    /// <summary>
    /// 对称加密基类,默认字符编码为UTF-8
    /// </summary>
    internal abstract class BaseSymmetricAlgorithm : ISecurityUtil
    {
        protected byte[] m_Key;
        protected byte[] m_IV;
        protected System.Security.Cryptography.SymmetricAlgorithm m_SymAlg;
        public abstract void SetKey(string sKey);

        public byte[] Encrypt(string sToEncrypt)
        {
            return Encrypt(sToEncrypt, InteralUtil.UTF8Encoding);
        }
        public string Decrypt(byte[] bzToDecrypt)
        {
            return Decrypt(bzToDecrypt, InteralUtil.UTF8Encoding);
        }
        public string DecryptFromFile(string path)
        {
            return DecryptFromFile(path, InteralUtil.UTF8Encoding);
        }
        public bool EncryptToFile(string path, string value)
        {
            return EncryptToFile(path, value, InteralUtil.UTF8Encoding);
        }

        #region ISecurityUtil 成员


        public byte[] Encrypt(string sToEncrypt, Encoding encoding)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] bzToRtn = null;
            try
            {
                byte[] bOri = encoding.GetBytes(sToEncrypt);
                ICryptoTransform encKey = m_SymAlg.CreateEncryptor(m_Key, m_IV);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, encKey, CryptoStreamMode.Write);
                cs.Write(bOri, 0, bOri.Length);
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

        public string Decrypt(byte[] bzToDecrypt, Encoding encoding)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            string sToRtn = string.Empty;

            try
            {
                ICryptoTransform decKey = m_SymAlg.CreateDecryptor(m_Key, m_IV);
                ms = new MemoryStream(bzToDecrypt);
                cs = new CryptoStream(ms, decKey, CryptoStreamMode.Read);
                byte[] bzRst = new byte[bzToDecrypt.Length];
                cs.Read(bzRst, 0, bzToDecrypt.Length);
                sToRtn = encoding.GetString(bzRst);
            }
            catch (System.Exception)
            {
                //throw ex;
                return string.Empty;
            }
            finally
            {
                if (cs != null)
                    cs.Close();
                if (ms != null)
                    ms.Close();
            }
            return sToRtn.TrimEnd('\0');
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

                dec = Decrypt(toDec);
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
            byte[] enc = Encrypt(value);
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

        #endregion
    }
    #endregion

    #region Hash(数字签名)
    internal class BaseHashUtil : IHashUtil
    {
        private string m_sHashName;
        public BaseHashUtil(string sHashName)
        {
            m_sHashName = sHashName;
        }
        #region IHashUtil 成员
        public string GetHashValue(string sSrc)
        {
            byte[] data = null;
            data = GetHashValue(InteralUtil.ISOEncoding.GetBytes(sSrc));
            return FormatData(data);
        }
        public bool Verify(string sSrc, string sHash)
        {
            string sHashValue = GetHashValue(sSrc);
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
        public byte[] GetHashValue(byte[] bSrc)
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
            byte[] bHashValue = GetHashValue(bSrc);
            System.StringComparer comparer = System.StringComparer.Ordinal;
            string sHashValue = InteralUtil.ISOEncoding.GetString(bHashValue);
            string sHash = InteralUtil.ISOEncoding.GetString(bHash);
            return (0 == comparer.Compare(sHashValue, sHash));
        }

        #endregion
    }
    internal class MD5Util : BaseHashUtil
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
    #endregion
}