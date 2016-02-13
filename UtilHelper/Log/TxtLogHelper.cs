using System.Text;

namespace UtilHelper.Log
{
    public class TxtLogWriter : ILogWriter
    {

        #region ILogWriter 成员
        private string _baseDir;

        public TxtLogWriter()
        {
           
        }
        public void SetBaseDir(string basedir)
        {
            _baseDir = basedir;
            if (System.String.IsNullOrEmpty(_baseDir))
                _baseDir = "./";
        }
        public void LogCustom(LogObject logObj)
        {
            System.IO.File.AppendAllText(_baseDir + "log.log", logObj.ToString("\t"), Encoding.UTF8);
        }
        #endregion
    }
}
