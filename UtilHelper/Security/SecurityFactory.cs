using System;

namespace UtilHelper.Security
{
    /// <summary>
    /// 所有数据安全工具类获取的公共接口
    /// </summary>
   public sealed class DSUtilFactory
    {
       public const string AES = "UtilHelper.Security.AESUtil";
       public const string RC2 = "UtilHelper.Security.RC2Util";
       public const string TripleDES = "UtilHelper.Security.TripleDESUtil";
       public const string RSA = "UtilHelper.Security.RSAImpl";

       /// <summary>
       /// 获取数据加密器.
       /// </summary>
       /// <param name="sUtilName">加密器类型.</param>
       /// <param name="sKey">官钥.</param>
       /// <returns></returns>
       public static IDCUtil GetDCUtil(string sUtilName, string sKey)
       {
           if (sUtilName.Equals(RSA))
           {
               return new RSAImpl();
           }
           else
           {
               BaseSymmetricAlgorithm util = null;
               try
               {
                   util = System.Reflection.Assembly.GetExecutingAssembly().
                       CreateInstance(sUtilName) as BaseSymmetricAlgorithm;
                   util.SetKey(sKey);
               }
               catch (Exception ex)
               {
                   throw ex;
               }
               return util;
           }

       }
        /// <summary>
        /// 默认为RC2数据加密器.
        /// </summary>
        /// <param name="sKey">The s key.</param>
        /// <returns></returns>
        public static IDCUtil GetDCUtil(string sKey)
        {
            BaseSymmetricAlgorithm bsa = new RC2Impl();
            bsa.SetKey(sKey);
            return bsa;
        }
    }

   /// <summary>
   /// 所有哈希值计算验证工具类获取的公共接口
   /// </summary>
   public sealed class SignUtilFactory
   {
       public const int SHA256 = 1;
       public const int MD5 = 2;
       public const int SHA384 = 3;
       public const int SHA512 = 4;
       public const int SHA1 = 0;
       public const int DSA = 5;
       public const int RSA = 6;
       /// <summary>
       /// 数据签名器.
       /// </summary>
       /// <param name="iLevel">类型.</param>
       /// <returns></returns>
       public static ISignUtil GetSignUtil(int iLevel)
       {
           switch (iLevel)
           {
               case SHA256:
                   return new BaseSignUtil("SHA256");
               case MD5:
                   return new MD5Util();
               case SHA384:
                   return new BaseSignUtil("SHA384");
               case SHA512:
                   return new BaseSignUtil("SHA512");
               case DSA:
                   return new DSAUtil();
               case RSA:
                   return new RSAImpl();
               default:
                   return new BaseSignUtil("SHA1");

           }
       }
       /// <summary>
       /// 默认为MD5数据签名器.
       /// </summary>
       /// <returns></returns>
       public static ISignUtil GetSignUtil()
       {
           return new MD5Util();
       }
   }
}
