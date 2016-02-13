using HVH_Ken_Modules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace PRTest
{
    
    
    /// <summary>
    ///这是 ProgFinderTest 的测试类，旨在
    ///包含所有 ProgFinderTest 单元测试
    ///</summary>
    [TestClass()]
    public class ProgFinderTest
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
        //    M_cmdObj = new CommanderData();
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        private void Cleer(CommanderData cmdObj)
        {
            cmdObj.Args = string.Empty;
            cmdObj.ParsedCmd = string.Empty;
            cmdObj.ExtCmds = string.Empty;
        }
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
            
        //}
        
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //    Cleer(M_cmdObj);
        //}
        
        #endregion




        /// <summary>
        ///ParseCommand 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HVH_Ken_Modules.dll")]
        public void ParseCommandTest()
        {
            string shortcut = string.Empty; // TODO: 初始化为适当的值
            CommanderData cmdObj = new CommanderData();
            //测试单个快捷命令
            shortcut = "cmd";
            ProgFinder_Accessor.ParseCommand(shortcut, cmdObj);
            Assert.AreEqual<string>("cmd", cmdObj.ParsedCmd);
            Assert.AreEqual<string>(string.Empty, cmdObj.Args.Trim().Trim());
            Assert.AreEqual<string>(string.Empty, cmdObj.ExtCmds);
            Cleer(cmdObj);

            //测试单个完整路径,带空格路径情况已经包含普通情况(不带空格)
            shortcut = @"C:\Windows\program files\Commander.exe";
            ProgFinder_Accessor.ParseCommand(shortcut, cmdObj);
            Assert.AreEqual<string>(@"C:\Windows\program files\Commander.exe", cmdObj.ParsedCmd);
            Assert.AreEqual<string>(string.Empty, cmdObj.Args.Trim());
            Assert.AreEqual<string>(string.Empty, cmdObj.ExtCmds);
            Cleer(cmdObj);

            //测试带参数命令
            shortcut = "cmd args";
            ProgFinder_Accessor.ParseCommand(shortcut, cmdObj);
            Assert.AreEqual<string>("cmd", cmdObj.ParsedCmd);
            Assert.AreEqual<string>("args", cmdObj.Args.Trim());
            Assert.AreEqual<string>(string.Empty, cmdObj.ExtCmds);
            Cleer(cmdObj);

            //测试带参数带空格完整路径
            shortcut = @"C:\Windows\program files\Commander.exe args";
            ProgFinder_Accessor.ParseCommand(shortcut, cmdObj);
            Assert.AreEqual<string>(@"C:\Windows\program files\Commander.exe", cmdObj.ParsedCmd);
            Assert.AreEqual<string>("args", cmdObj.Args.Trim());
            Assert.AreEqual<string>(string.Empty, cmdObj.ExtCmds);
            Cleer(cmdObj);

            //测试URL
            shortcut = "http://www.Commander.com";
            ProgFinder_Accessor.ParseCommand(shortcut, cmdObj);
            Assert.AreEqual<string>("http://www.Commander.com", cmdObj.ParsedCmd);
            Assert.AreEqual<string>(string.Empty, cmdObj.Args.Trim());
            Assert.AreEqual<string>(string.Empty, cmdObj.ExtCmds);
            Cleer(cmdObj);

            //测试FTP路径
            shortcut = "\\88.88.88.88";
            ProgFinder_Accessor.ParseCommand(shortcut, cmdObj);
            Assert.AreEqual<string>("\\88.88.88.88", cmdObj.ParsedCmd);
            Assert.AreEqual<string>(string.Empty, cmdObj.Args.Trim());
            Assert.AreEqual<string>(string.Empty, cmdObj.ExtCmds);
            Cleer(cmdObj);

            //测试内部命令(内部命令只能出现在配置中，不能由用户在运行窗口输入)
            shortcut = @"C:\windows\program files\Commander.exe args{+$check| +before| after}";
            ProgFinder_Accessor.ParseCommand(shortcut, cmdObj);
            Assert.AreEqual<string>(@"C:\windows\program files\Commander.exe", cmdObj.ParsedCmd);
            Assert.AreEqual<string>("args", cmdObj.Args.Trim());
            Assert.AreEqual<string>("+$check| +before| after", cmdObj.ExtCmds);

            Cleer(cmdObj);

            //测试内部命令 无参数，命令前后可以有空格
            shortcut = @"C:\windows\program files\Commander.exe {+   $check| +before  | after}";
            ProgFinder_Accessor.ParseCommand(shortcut, cmdObj);
            Assert.AreEqual<string>(@"C:\windows\program files\Commander.exe", cmdObj.ParsedCmd);
            Assert.AreEqual<string>(string.Empty, cmdObj.Args.Trim());
            Assert.AreEqual<string>("+   $check| +before  | after", cmdObj.ExtCmds);
            Cleer(cmdObj);
        }
    }
}
