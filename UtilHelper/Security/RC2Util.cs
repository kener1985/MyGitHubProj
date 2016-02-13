using System.Security.Cryptography;

namespace UtilHelper.Security
{
    /// <summary>
    /// 加解密工具
    /// </summary>
    internal class RC2Util : BaseSymmetricAlgorithm
    {
        public RC2Util()
        {
            m_SymAlg = new RC2CryptoServiceProvider();
        }
        public override void SetKey(string sKey)
        {
            IHashUtil md5 = HashUtilFactory.GetHashUtil();
            byte[] bTmp = InteralUtil.ISOEncoding.GetBytes(sKey);
            m_IV = md5.GetHashValue(bTmp);
            m_Key = m_IV;
            
        }
    }
}
