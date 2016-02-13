using System.Text;
using System.Security.Cryptography;
namespace UtilHelper.Security
{
    internal class TripleDESImpl : BaseSymmetricAlgorithm
    {

        public TripleDESImpl()
        {
            m_SymAlg = new TripleDESCryptoServiceProvider();
        }
        public override  void SetKey(string strKey)
        {
            ISignUtil md5 = SignUtilFactory.GetSignUtil();
            byte[] bTmp = InteralUtil.ISOEncoding.GetBytes(strKey);
            m_IV = md5.Sign(bTmp);
            bTmp = InteralUtil.ISOEncoding.GetBytes(strKey + strKey);
            m_Key = md5.Sign(bTmp);
            if (TripleDES.IsWeakKey(m_Key))
                throw new System.ArgumentException("The Key is Weak.");
        }
        
    }
}
