using System.Security.Cryptography;

namespace UtilHelper.Security
{
    /// <summary>
    /// 加解密工具
    /// </summary>
    internal class RC2Impl : BaseSymmetricAlgorithm
    {
        public RC2Impl()
        {
            m_SymAlg = new RC2CryptoServiceProvider();
        }
        public override void SetKey(string sKey)
        {
            ISignUtil md5 = SignUtilFactory.GetSignUtil();
            byte[] bTmp = InteralUtil.ISOEncoding.GetBytes(sKey);
            m_IV = md5.Sign(bTmp);
            m_Key = m_IV;
            
        }
    }
}
