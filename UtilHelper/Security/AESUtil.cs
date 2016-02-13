using System.Security.Cryptography;
namespace UtilHelper.Security
{
    internal class AESUtil : BaseSymmetricAlgorithm
    {
        public AESUtil()
        {
            m_SymAlg = new AesManaged();
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
