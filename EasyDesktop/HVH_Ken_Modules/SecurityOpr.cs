using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace HVH_Ken_Modules
{
    /// <summary>
    /// 加解密工具
    /// </summary>
    public class SecurityOpr
    {

        private byte[] Key;
        private byte[] IV;
        public SecurityOpr(string strKey)
        {
            byte[] bzKey = Encoding.ASCII.GetBytes(strKey);
            Key = MD5.Create().ComputeHash(bzKey);
            IV = Key;
        }

        public string ReadFromFile(string path)
        {
            
            FileStream fs = null;
            BinaryReader br = null;
            string dec = string.Empty;
            try
            {
                fs = File.Open(path, FileMode.Open);
                br = new BinaryReader(fs, Encoding.UTF8);

                byte[] toDec = br.ReadBytes((int)fs.Length);

                dec = RC2Decrypt(toDec);
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                System.Windows.Forms.MessageBox.Show("非法加密数据", "Error", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (FileNotFoundException)
            { }
            finally
            {
                if(fs != null)
                fs.Close();
                if(br != null)
                br.Close();
            }
            return dec.Trim();
        }

        public  void WriteToFile(string path,string value)
        {
            byte[] enc = RC2Encrypt(value);
            FileStream fs = File.Open(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs,Encoding.UTF8);
            bw.Write(enc);
            fs.Close();
            bw.Close();
            
        }
        /// <summary>
        /// 加密数据.
        /// </summary>
        /// <param name="toEncrypt">要加密的数据.</param>
        /// <returns>之所以不直接返回string是因为当将该string再转成byte[]后，解密会出现异常</returns>
        private byte[] RC2Encrypt(string toEncrypt)
        {
            RC2CryptoServiceProvider myRC2 = new RC2CryptoServiceProvider();
            ICryptoTransform myCryptoTrans = myRC2.CreateEncryptor(Key, IV);
            MemoryStream ms = new MemoryStream();
            CryptoStream CStream = new CryptoStream(ms , myCryptoTrans, CryptoStreamMode.Write);
            StringBuilder sb = new StringBuilder();
            
            byte[] buf = Encoding.UTF8.GetBytes(toEncrypt);
            CStream.Write(buf, 0, buf.Length);
            CStream.FlushFinalBlock();
            byte[] enc = ms.ToArray();
            ms.Close();
            CStream.Close();

            return enc;
        }
        /// <summary>
        /// 解密数据.
        /// </summary>
        /// <param name="toDecrypt">要解密的密文.</param>
        /// <returns></returns>
        private string RC2Decrypt(byte[] toDecrypt)
        {
            RC2CryptoServiceProvider myRC2 = new RC2CryptoServiceProvider();
            ICryptoTransform myCryptoTrans = myRC2.CreateDecryptor(Key, IV);
            MemoryStream ms = new MemoryStream(toDecrypt);
            CryptoStream cs = new CryptoStream(ms, myCryptoTrans, CryptoStreamMode.Read);
            byte[] tmp = new byte[toDecrypt.Length];

            cs.Read(tmp, 0, tmp.Length);
            ms.Close();
            cs.Close();
            return Encoding.UTF8.GetString(tmp);
        }
    }
}
