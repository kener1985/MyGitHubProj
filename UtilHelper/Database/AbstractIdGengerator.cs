using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace UtilHelper.Database
{
    /// <summary>
    /// ID生成喊叫虚拟类，通过实现gennerate将生成的值设到变更集的id字段中，主要用于Helper的Update方法
    /// </summary>
    public abstract class AbstractIdGengerator
    {
        /// <summary>
        /// The _ table name
        /// </summary>
        protected string _TableName;
        /// <summary>
        /// The _ id
        /// </summary>
        protected string _Id;
        /// <summary>
        /// The _ helper
        /// </summary>
        protected AbstractDbHelper _Helper;
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public string ID
        {
            get { return _Id; }
            set { _Id = value; }
        }
        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public AbstractDbHelper Helper
        {
            get {return _Helper; }
            set { _Helper = value; }
        }
        /// <summary>
        /// Gengerates the specified table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public abstract bool Gengerate(DataTable table);
    }

    /// <summary>
    /// 整数类型自动+1 ID生成器
    /// </summary>
    public class IncreatableIdGengerator : AbstractIdGengerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IncreatableIdGengerator"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        public IncreatableIdGengerator(string table)
        {
            _TableName = table;
            _Id = "id";
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="IncreatableIdGengerator"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="id">The id.</param>
        public IncreatableIdGengerator(string table, string id)
        {
            _Id = id;
            _TableName = table;
        }
        /// <summary>
        /// Gengerates the specified table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        /// <exception cref="System.NullReferenceException">DbHelper can't be null!</exception>
        public override bool Gengerate(DataTable table)
        {
            if (_Helper == null)
                throw new NullReferenceException("DbHelper can't be null!");

            if(String.IsNullOrEmpty(_TableName) || 
                String.IsNullOrEmpty(_Id) ||
                table.Columns.Contains(_Id) == false
                )
                return false;

            DataTable adds = table.GetChanges(DataRowState.Added);
            if(adds == null)
                return true;

            long iStart;
            object rtn = _Helper.ExcuteForUnique("select max(" + _Id + ") from " + _TableName);
            iStart = rtn == null ? 0 : Convert.ToInt64(rtn);
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    row.SetField<long>(_Id, ++iStart);
                }
            }
            return true;
        }
    }
}
