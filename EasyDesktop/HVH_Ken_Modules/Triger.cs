using System;
using System.Collections.Generic;
using System.Text;
using UtilHelper.Database;


namespace HVH_Ken_Modules
{
    public interface ITrigerable : ICloneable
    {
        bool CanBeTriger();
        void CreateMessage(out MessageObj msg);
    }
    public enum TaskType { DAY = 0, WEEK, MONTH, YEAR };
    public class TaskItem :  ITrigerable
    {
        [Column(ColumnName = "shortcut")]
        private string _name;
        [Column(ColumnName = "condition")]
        private string _condition;
        [Column(ColumnName = "type", DataType = typeof(Int32))]
        private int _type;
        [Column(ColumnName = "fid", DataType = typeof(Int64))]
        private long id;
        public TaskItem()
        {
            this._condition = String.Empty;
            this._name = String.Empty;
            this._type = (int)TaskType.DAY;
        }
        public TaskItem(string name, string condition)
        {
            this._name = name;
            this._condition = condition;
            this._type = (int)TaskType.DAY;
        }

        public TaskItem(string name, string condition, TaskType type)
        {
            this._name = name;
            this._condition = condition;
            this._type = (int)type;
        }
        public long Id
        {
            get { return id; }
            set{id = value;}
        }
        public string Name
        {
            get { return _name; }
            set { this._name = value; }
        }
        public string Condition
        {
            get { return _condition; }
            set { this._condition = value; }
        }
        public TaskType CircleType
        {
            get { return (TaskType)this._type; }
            set { this._type = (int)value; }
        }
        public object Clone()
        {
            return new TaskItem(this._name, this._condition, (TaskType)this._type);
        }

        #region ITrigerable 成员

        bool ITrigerable.CanBeTriger()
        {
            DateTime dtNow = DateTime.Now;
            //string sNow = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string sNow = dtNow.ToString("HHmmss");
            int iTime = Convert.ToInt32(sNow);
            //int iTrigerTime = Convert.ToInt32();
            if (_type == (int)TaskType.DAY)
                return sNow.Equals(this._condition);
            else if (_type == (int)TaskType.WEEK)
            {
                int iPos = _condition.IndexOf('|');//时间分隔符
                string sDayOfWeek = "W" + ((int)dtNow.DayOfWeek).ToString();
                string sTime = _condition.Substring(iPos + 1, 6);
                bool bIsCan = (_condition.IndexOf(sDayOfWeek) != -1 &&
                    sTime.Equals(sNow));

                return bIsCan;
            }

            return false;
        }
        public void CreateMessage(out MessageObj msg)
        {
            msg = new MessageObj(_name,null);
        }

        public void Init()
        {
            
        }

        #endregion
    }

    public class Notice : ITrigerable
    {

        [Column(ColumnName = "id",DataType=typeof(long))]
        private long _Id;
        [Column(ColumnName = "condition")]
        private string _Condition;
        [Column(ColumnName = "info")]
        private string _Info;
        [Column(ColumnName = "dur_times", DataType = typeof(double))]
        private double _DurMin;//间隔,以分钟为单位
        
        private int _Times;//次数,0表示无限
        [Column(ColumnName = "is_temp", DataType = typeof(bool))]
        private bool _IsTemp;//是否为临时通知
       
        //private int  TimesRest;
        //private bool IsValid = true;//一次性的通知，触发后便无效
        public bool Trigered = false;
        
        public Notice()
        {
            _DurMin = 0.0;
            _Times = 1;
            //TimesRest = 1;
        }
        public Notice(string info,string cdt,double dur, int times,bool isTemp)
        {
            _Info = info;
            _Condition = cdt;
            _DurMin = dur;
            _Times = times;
            //TimesRest = times;
            _IsTemp = isTemp;
        }
        public string Condition
        {
            get { return _Condition; }
            set { _Condition = value; }
        }
        public string Info
        {
            get { return _Info; }
            set { _Info = value; }
        }
        public double DurMin
        {
            get { return _DurMin; }
            set{_DurMin = value;}
        }
        [Column(ColumnName = "times", DataType = typeof(int))]
        public int Times
        {
            get {
                if (_Times == Int32.MaxValue)
                    return 0;
                return _Times; }
            set {
                if (value == 0)
                    _Times = Int32.MaxValue;
                else
                    _Times = value;
                //TimesRest = _Times;
            }
        }
        public bool IsTemp
        {
            get { return _IsTemp; }
            set { _IsTemp = value; }
        }
        #region ICloneable 成员
        
        object ICloneable.Clone()
        {
            Notice n = new Notice(_Info,_Condition,_DurMin,_Times,_IsTemp);
            return n;
        }
        
        #endregion

        #region ITrigerable 成员

        public bool CanBeTriger()
        {
            if (GlobalVar.Instanse.StopNotice)
                return false;

            DateTime dtNow = DateTime.Now;
            DateTime dtCdt;
            bool bTriger;

            if (DateTime.TryParse(_Condition, out dtCdt) == false)
                    return false;
            

            bTriger = IsTimeEqual(dtNow, dtCdt);
            if (bTriger)
                RecordState(dtNow);
            
            return bTriger;
        }
        public void CreateMessage(out MessageObj msg)
        {
            string info = _Info;

            msg = new MessageObj("$notice$", info);
        }

        #endregion
        private bool IsTimeEqual(DateTime dtNow,DateTime dtCdt)
        {
            bool bEql = false;
            //因重启而被忽略的通知才激活
            if (dtCdt > dtNow)
                return false;

            //去掉毫秒
            DateTime.TryParse(dtNow.ToLongTimeString(), out dtNow);

            double span = (dtNow - dtCdt).TotalMinutes * 10;
            double dur = _DurMin * 10;
            int times = (int)(span / dur);
            bEql = (span % dur) == 0 && times < _Times;
            if (bEql && _Times > 0)
            {
                //if (Trigered == false)
                //    TimesRest -= times;
                bEql = (_Times - times) > 0;
            }
           
            return bEql;
        }
        private void RecordState(DateTime dtNow)
        {
            if(_IsTemp)
            {
                GlobalVar.Helper.AddCustomParam("id", _Id);
                GlobalVar.Helper.ExcuteForUnique("delete from notices where id=@id");
                Trigered = true;
                _IsTemp = false;
                return;
            }
           
            //if (_Times == 1)
            //    return;

            ////去掉毫秒
            //DateTime.TryParse(dtNow.ToLongTimeString(), out dtNow);
            ////保证时间一致,所以不用DateTime.Now
            //if (Trigered && _DurMin > 0 && TimesRest > 0)//有间隔才减次数
            //{
            //    TimesRest--;
            //}
            //Trigered = true;
        }


    }


}
