using System;
using System.Text;

namespace UtilHelper.Log
{
    public interface ILogWriter
    {
        /// <summary>
        /// 自定义写日志方法，线程安全.
        /// </summary>
        void LogCustom(LogObject logObj);
        void SetBaseDir(string basedir);
    }
    public class LogHelper<T> where T : ILogWriter, new()
    {
        private static LogHelper<T> me;
        private ILogWriter writer;
        private readonly string InfoLevel;
        private readonly string WarmLevel;
        private readonly string DebugLevel;
        private readonly string ErrorLevel;
        private readonly string FatalLevel;
        private string _basedir;
        private bool _IsSync;//是否同步
        private LogHelper(string basedir)
        {
            InfoLevel = "INFO";
            WarmLevel = "WARM";
            DebugLevel = "DEBUG";
            ErrorLevel = "ERROR";
            FatalLevel = "FATAL";
            writer = new T();
            writer.SetBaseDir(basedir);
            _IsSync = false;
        }

        static LogHelper()
        {
            me = null;
        }
        public void  SetBaseDir(){}
        public bool IsSync
        {
            get { return _IsSync; }
            set { _IsSync = value; }
        }
        public string BaseDir
        {
            get{return _basedir;}
            set{_basedir = value;}
        }
        /// <summary>
        /// LogHelper 单实例.
        /// </summary>
        /// <value>Me.</value>
        public static LogHelper<T> CreateLogger(string basedir)
        {
          
                lock (typeof(LogHelper<T>))
                {
                    if (me == null)
                        me = new LogHelper<T>(basedir);
                    return me;
                }

        }
       
        /// <summary>
        /// 通过ILogWriter的实例，自定义写日志动作.
        /// </summary>
        /// <param name="logObj">The log obj.</param>
        protected void LogCustom(LogObject logObj)
        {
            if (writer != null)
            {
                if(_IsSync == true)
                {
                    lock (me)
                    {
                        writer.LogCustom(logObj);
                    }
                }else
                {
                    writer.LogCustom(logObj);
                }
            }
        }
        #region 日志操作
        /// <summary>
        /// 线程安全记录日志.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="msg">The MSG.</param>
        private void LogMsg(string level, string msg,string remark)
        {
            lock (me)
            {
                LogObject obj = new LogObject(level, msg, remark);
                LogCustom(obj);
            }
        }
        public void LogInfo(string msg)
        {
            LogMsg(this.InfoLevel, msg,string.Empty);
        }
        public void LogInfo(string msg, string remark)
        {
            LogMsg(this.InfoLevel, msg, remark);
        }
        public void LogDebug(string msg)
        {
            #if DEBUG
            LogMsg(this.DebugLevel, msg, string.Empty);
            #endif
        }
        public void LogDebug(string msg, string remark)
        {
            #if DEBUG
            LogMsg(this.DebugLevel, msg, remark);
            #endif
        }
        public void LogWarm(string msg)
        {
            LogMsg(this.WarmLevel, msg, string.Empty);
        }
        public void LogWarm(string msg, string remark)
        {
            LogMsg(this.WarmLevel, msg, remark);
        }
        public void LogError(string msg)
        {
            LogMsg(this.ErrorLevel, msg, string.Empty);
        }
        public void LogError(string msg, string remark)
        {
            LogMsg(this.ErrorLevel, msg,remark);
        }
        public void LogFatal(string msg)
        {
            LogMsg(this.FatalLevel, msg, string.Empty);
        }
        public void LogFatal(string msg,string remark)
        {
            LogMsg(this.FatalLevel, msg,remark);
        }
        #endregion


    }
    #region 日志信息对象
    public class LogObject
    {
        private string _level;
        private string _msg;
        private long _when;
        private string _remark;
        public LogObject(string level,string msg,string remark)
        {
            _level = level;
            _msg = msg;
            _when = DateTime.Now.ToBinary();
            _remark = remark;
        }
        public string Level
        {
            get { return _level; }
        }
        public string Msg
        {
            get { return _msg; }
        }
        public string Remark
        {
            get { return _remark; }
        }
        public long When
        {
            get { return _when; }
        }
        public string ToString(string sep)
        {
            DateTime dt = DateTime.FromBinary(_when);
            StringBuilder sb = new StringBuilder();
            sb.Append(dt.ToLongDateString()).Append(dt.ToLongTimeString()).
                Append(" [").Append(_level).Append(']').Append(sep).
                Append(_msg).Append(sep).
                Append(sep).Append(_remark).Append("\r\n");
            return sb.ToString();
        }
    }
    #endregion
}
