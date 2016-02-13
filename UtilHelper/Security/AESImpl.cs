using System.Security.Cryptography;
namespace UtilHelper.Security
{
    internal class AESUImpl : BaseSymmetricAlgorithm
    {
        public AESUImpl()
        {
            //m_SymAlg = new AesManaged();
            m_SymAlg = new AesCryptoServiceProvider();
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
