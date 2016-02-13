using HVH_Ken_Modules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace PRTest
{
    
    
    /// <summary>
    ///这是 SecurityOprTest 的测试类，旨在
    ///包含所有 SecurityOprTest 单元测试
    ///</summary>
    [TestClass()]
    public class SecurityOprTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试属性
        // 
        //编写测试时，还可使用以下属性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///RC2Encrypt 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HVH_Ken_Modules.dll")]
        public void RC2EncryptTest()
        {
            PrivateObject param0 = new PrivateObject(new SecurityOpr("aabbccddee")); // TODO: 初始化为适当的值
            SecurityOpr_Accessor target = new SecurityOpr_Accessor(param0); // TODO: 初始化为适当的值
            string toEncrypt = "origenal text"; // TODO: 初始化为适当的值
            string expected = "origenal text"; // TODO: 初始化为适当的值
            byte[] enText;
            string actual;

            enText = target.RC2Encrypt(toEncrypt);
            actual = target.RC2Decrypt(enText).Replace("\0", "");
            Assert.AreEqual(expected, actual);
        }

    }
}
