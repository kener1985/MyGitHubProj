using System.Text;
using System.Security.Cryptography;
namespace UtilHelper.Security
{
    internal class TripleDESUtil : BaseSymmetricAlgorithm
    {

        public TripleDESUtil()
        {
            m_SymAlg = new TripleDESCryptoServiceProvider();
        }
        public override  void SetKey(string strKey)
        {
            IHashUtil md5 = HashUtilFactory.GetHashUtil();
            byte[] bTmp = InteralUtil.ISOEncoding.GetBytes(strKey);
            m_IV = md5.GetHashValue(bTmp);
            bTmp = InteralUtil.ISOEncoding.GetBytes(strKey + strKey);
            m_Key = md5.GetHashValue(bTmp);
            if (TripleDES.IsWeakKey(m_Key))
                throw new System.ArgumentException("The Key is Weak.");
        }
        
    }
}
