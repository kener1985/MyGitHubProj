using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace UtilHelper.Security
{
    /// <summary>
    /// 数据加密接口
    /// </summary>
    public interface IDCUtil
    {
        byte[] Encrypt(byte[] bzToEncrypt);
        byte[] Decrypt(byte[] bzToDecrypt);

        byte[] EncryptString(string sToEncrypt);
        string DecryptString(byte[] bzToDecrypt);
        string DecryptFromFile(string path);
        bool EncryptToFile(string path, string value);

        byte[] EncryptString(string sToEncrypt,Encoding encoding);
        string DecryptString(byte[] bzToDecrypt, Encoding encoding);
        string DecryptFromFile(string path, Encoding encoding);
        bool EncryptToFile(string path, string value, Encoding encoding);
    }
    /// <summary>
    /// 签名接口
    /// </summary>
    public interface ISignUtil
    {
        string Sign(string sSrc);
        bool Verify(string sSrc, string sSign);

        byte[] Sign(byte[] bSrc);
        bool Verify(byte[] bSrc, byte[] bSign);
    }
    public interface IAsymVisior 
    {
        AsymmetricAlgorithm Provider { get; set; }
        byte[] PublicKey{get;set;}
        byte[] PrivateKey { get; set; }
    }
}
