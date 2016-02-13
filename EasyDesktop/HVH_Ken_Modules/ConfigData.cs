using System;
using UtilHelper.Database;
using System.Data;

namespace HVH_Ken_Modules
{
    /// <summary>
    /// 程序配置信息
    /// </summary>
    public class ConfigData : ICloneable
    {
        [Column(ColumnName = "id",DataType = typeof(long))]
        private long _Id;
        [Column(ColumnName="path")]
        private string _path;
        [Column(ColumnName = "title")]
        private string _title;
        [Column(ColumnName = "shortcut")]
        private string _shortcut;//快捷运行
        [Column(ColumnName = "is_auto_run", DataType = typeof(Boolean))]
        private bool _isAutRun;//是否为启动项
        [Column(ColumnName = "has_taskitem", DataType = typeof(Boolean))]
        private bool _hasTaskItem;
        [Column(ColumnName = "hints", DataType = typeof(Int32))]
        private int _hints;
        //private TaskItem item;
        private DataRow _Row;
        public ConfigData()
        {
            _Id = 0;
            _path = "";
            _title = "";
            _shortcut = "";
            _isAutRun = false;
            //item = null;
            _hints = 0;
        }
        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public DataRow Row
        {
            get{return _Row;}
            set{_Row = value;}
        }
        public int Hints
        {
            get { return _hints; }
            set{_hints = value;}
        }
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string Shortcut
        {
            get { return _shortcut; }
            set { _shortcut = value; }
        }
        public bool IsAutRun
        {
            set { _isAutRun = value; }
            get { return _isAutRun; }
        }
        public bool HasTaskitem
        {
            get { return _hasTaskItem; }
            set{_hasTaskItem = value;}
        }
        public void Merge(ConfigData data)
        {
            this._isAutRun = data._isAutRun;
            this.Row["is_auto_run"] = _isAutRun;
            this._title = data._title;
            this.Row["title"] = _title;
            this._path = data.Path;
            this.Row["path"] = _path;
            this._shortcut = data.Shortcut;
            this.Row["shortcut"] = _shortcut;
            this._hints = data._hints;
            this.Row["hints"] = data._hints;
        }
        #region ICloneable 成员

        public object Clone()
        {
            ConfigData data = new ConfigData();
            data.IsAutRun = this._isAutRun;
            data.Path = this._path;
            //if(this.item != null)
            //    data.TaskItem = this.item.Clone() as TaskItem;
            data.Shortcut = this._shortcut;
            data.Title = this._title;

            return data;
        }

        #endregion
    }
    /// <summary>
    /// 搜索引擎配置信息
    /// </summary>
    internal class SrcEngData
    {
        private string m_sTitle;
        private string m_sUrl;
        public string Title
        {
            get { return m_sTitle;}
            set{m_sTitle = value;}
        }
        public string Url
        {
            get{return m_sUrl;}
            set { m_sUrl = value; }
        }
    
    }
}
