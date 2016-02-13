using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using System.Data.Common;
using System.Reflection;
using UtilHelper.Log;

namespace UtilHelper.Database
{
    /// <summary>
    /// 用DataAdapter封装数据库操作，因此只适合少量数据库的操作，
    /// 不适合以十万计的数据量操作
    /// </summary>
    public abstract class AbstractDbHelper : IDisposable
    {
        protected LogHelper<TxtLogWriter> _Log;
        protected DbConnection m_con = null;
        protected StringCollection m_scSelect;
        protected Dictionary<string, object> m_dCustomParam;//自定义参数
        protected Dictionary<string, DbDataAdapter> m_dAdapters;//一张表一个Adapter
        protected List<string> m_TableNames;
        /// <summary>
        /// 设置或获取参数占位符.
        /// </summary>
        /// <value></value>
        protected string ParamSign { get; set; }

        public DbConnection Conn { get { return m_con; } set { m_con = value; } }
        #region 兼容各种版本的数据库操作对象
        protected abstract DbDataAdapter NewDataAdapter();
        protected abstract DbCommand NewCommand(string cmdText, DbConnection con);
        protected abstract DbParameter NewParameter();
        protected abstract DbParameterCollection NewParameterCollection();
        protected abstract DbConnection NewConnection(string sConStr);
        #endregion
        public AbstractDbHelper(string connstr,LogHelper<TxtLogWriter> log)
        {
            if (m_con == null)
            {
                m_con = NewConnection(connstr);
            }
            else if (m_con.ConnectionString != connstr)
            {
                m_con.Close();
                m_con.Dispose();
                m_con = NewConnection(connstr);
            }
            Init(log);
        }
        public AbstractDbHelper(DbConnection con, LogHelper<TxtLogWriter> log)
        {
            if (m_con != null)
            {
                m_con.Close();
                m_con.Dispose();
            }
            m_con = con;
            Init(log);
        }
        private void Init(LogHelper<TxtLogWriter> log)
        {
            _Log = log;
            m_scSelect = new StringCollection();
            m_dCustomParam = new Dictionary<string, object>();
            m_dAdapters = new Dictionary<string, DbDataAdapter>();
            m_TableNames = new List<string>();
            //ParamSign = "@";

            try
            {
                if (m_con.State != ConnectionState.Open)
                    m_con.Open();
            }
            catch (Exception ex)
            {
                _Log.LogError(ex.Message, "Connection open");
                if (m_con != null)
                {
                    m_con.Close();
                    m_con.Dispose();
                    m_con = null;
                }
            }
        }
        /// <summary>
        /// 设置 SQL语句所需的参数，SQL语句执行完后会自动清空.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Dictionary<string, object> AddCustomParam(string name, object value)
        {
            if (m_dCustomParam.ContainsKey(name) == false)
                m_dCustomParam.Add(name, value);
            return m_dCustomParam;
        }
        public void Reset()
        {
            m_scSelect.Clear();
            m_dCustomParam.Clear();
            //释放命令
            foreach (KeyValuePair<string, DbDataAdapter> kv in m_dAdapters)
            {
                if (kv.Value.SelectCommand != null)
                    kv.Value.SelectCommand.Dispose();
                if (kv.Value.DeleteCommand != null)
                    kv.Value.DeleteCommand.Dispose();
                if (kv.Value.UpdateCommand != null)
                    kv.Value.UpdateCommand.Dispose();
                if (kv.Value.InsertCommand != null)
                    kv.Value.InsertCommand.Dispose();
                kv.Value.Dispose();
            }
            m_dAdapters.Clear();
            m_TableNames.Clear();
        }
        #region  增加数据库操作命令

        /// <summary>
        /// [SELECT] 添加查询命令,如果参数通过占位符传递，参数值可通过AddCustomParam设置
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="fields">The filterFields. 该过滤参数会被自动加到where后面，默认为=过滤，定制需用AddSelectWithLimit</param>
        /// <returns></returns>
        public virtual AbstractDbHelper AddSelect(string table, string fields, string filterFields)
        {
            StringBuilder sb = new StringBuilder();
            if (String.IsNullOrEmpty(fields))
                fields = "*";
            sb.Append("SELECT ").Append(fields).Append(" FROM ").Append(table);

            if (filterFields != String.Empty)
            {
                string[] filters = System.Text.RegularExpressions.Regex.Split(filterFields, ",");
                sb.Append(" WHERE ").Append(MakeWhereSql(filters));
            }
            m_TableNames.Add(table);
            m_scSelect.Add(sb.ToString());
            return this;
        }
        public AbstractDbHelper AddSelect(string table, string fields)
        {
            return AddSelect(table, fields, String.Empty);
        }
        /// <summary>
        /// 加查询命令,如果参数通过占位符传递，参数值可通过AddCustomParam设置
        /// </summary>
        /// <param name="talbe">The talbe.</param>
        /// <param name="fields">The fields.该过滤参数会被自动加到where后面</param>
        /// <param name="sWhere">The sLimit.特殊查询语句，如order by 等</param>
        /// <returns></returns>
        public AbstractDbHelper AddSelectWithLimit(string talbe, string fields, string sLimit)
        {
            StringBuilder sb = new StringBuilder();
            if (String.IsNullOrEmpty(fields))
                fields = "*";
            AddSelect(talbe, fields);
            sb.Append(m_scSelect[m_scSelect.Count - 1]).Append(" WHERE ").Append(sLimit);
            m_scSelect[m_scSelect.Count - 1] = sb.ToString();
            return this;
        }
        /// <summary>
        /// [DELETE] 增加删除命令
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="sID">The s ID.</param>
        /// <param name="param">The param.</param>
        /// <returns>AbstractDbHelper.</returns>
        public AbstractDbHelper AddDelete(string table, string sID, Dictionary<string, object> param)
        {
            StringBuilder sb = new StringBuilder();
            DbCommand cmd;
            string[] ids = sID.Split(',');
            //SQL语句
            sb.Append("DELETE FROM ").Append(table).Append(" WHERE ").
                Append(MakeWhereSql(ids));
            //为表table添加一个Adapter
            if (m_dAdapters.ContainsKey(table) == false)
                m_dAdapters[table] = NewDataAdapter();
            //设置参数
            cmd = NewCommand(sb.ToString(), m_con);

            AddDbParamMapping(cmd, ids, param);
            if (m_dAdapters[table].DeleteCommand != null)
                m_dAdapters[table].DeleteCommand.Dispose();
            m_dAdapters[table].DeleteCommand = cmd;
            return this;
        }
        public AbstractDbHelper AddDelete(string table, string sID)
        {
            return AddDelete(table, sID, null);
        }
        /// <summary>
        /// [INSERT INTO]增加插入命令
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="fields">要插入到数据库的字段，传入之前要Trim过.</param>
        /// <returns></returns>
        public AbstractDbHelper AddInsert(string table, string fields, Dictionary<string, object> param)
        {
            StringBuilder sb = new StringBuilder();
            DbCommand cmd;
            if (String.IsNullOrEmpty(fields) == true)
                return this;
            //SQL语句
            string fs = fields.Replace(ParamSign, "");
            string values = ParamSign + fs.Replace(",", "," + ParamSign);
            sb.Append("INSERT INTO ").Append(table).Append("(").
                Append(fs).Append(')').Append(" VALUES(").Append(values).Append(')');
            //为表table添加一个Adapter
            if (m_dAdapters.ContainsKey(table) == false)
                m_dAdapters[table] = NewDataAdapter();
            //设置参数
            cmd = NewCommand(sb.ToString(), m_con);
            AddDbParamMapping(cmd, fields.Split(','), param);
            if (m_dAdapters[table].InsertCommand != null)
                m_dAdapters[table].InsertCommand.Dispose();
            m_dAdapters[table].InsertCommand = cmd;
            return this;
        }
        public AbstractDbHelper AddInsert(string table, string fields)
        {
            return AddInsert(table, fields, null);
        }
        /// <summary>
        /// [UPDATE] 增加更新命令
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public AbstractDbHelper AddUpdate(string table, string fields, string id, Dictionary<string, object> param)
        {
            StringBuilder sb = new StringBuilder();
            string[] fs = fields.Split(',');

            sb.Append("UPDATE ").Append(table).Append(" SET ");
            foreach (string f in fs)
            {
                if (f.StartsWith(ParamSign))
                {
                    string temp = f.Substring(1);
                    sb.Append(temp).Append("=").Append(ParamSign).Append(temp).Append(',');
                }
                else
                { sb.Append(f).Append("=").Append(ParamSign).Append(f).Append(','); }

            }
            //去掉最后一个 , 号
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" WHERE ").Append(MakeWhereSql(id.Split(',')));
            if (m_dAdapters.ContainsKey(table) == false)
                m_dAdapters[table] = NewDataAdapter();

            DbCommand cmd = NewCommand(sb.ToString(), m_con);
            string sParam = fields + "," + id;

            AddDbParamMapping(cmd, sParam.Split(','), param);
            if (m_dAdapters[table].UpdateCommand != null)
                m_dAdapters[table].UpdateCommand.Dispose();
            m_dAdapters[table].UpdateCommand = cmd;
            return this;
        }
        public AbstractDbHelper AddUpdate(string table, string fields, string id)
        {
            return AddUpdate(table, fields, id, null);
        }
        #endregion
        private string MakeWhereSql(string[] ids)
        {
            StringBuilder sb = new StringBuilder();
            string sAnd = " AND ";
            int iAndLen = sAnd.Length;
            foreach (string id in ids)
            {
                //
                if (id.StartsWith(ParamSign))
                {
                    string temp = id.Substring(1);
                    sb.Append(temp).Append("=").Append(ParamSign).Append(temp).Append(sAnd);
                }
                else
                {
                    sb.Append(id).Append("=").Append(ParamSign).Append(id).Append(sAnd);
                }
            }
            sb.Remove(sb.Length - iAndLen, iAndLen);
            return sb.ToString();
        }

        /// <summary>
        /// 添加数据集中的列和参数映射.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <param name="fields">The fields.</param>
        private void AddDbParamMapping(DbCommand cmd, string[] fields, Dictionary<string, object> param)
        {
            foreach (string f in fields)
            {
                DbParameter p = NewParameter();
                if (f.StartsWith(ParamSign))
                {
                    string temp = f.Substring(1);
                    if (param != null && param.ContainsKey(temp))
                    {
                        p.ParameterName = temp;
                        p.Value = param[temp];
                    }
                }
                else
                {
                    p.SourceColumn = f;
                    p.ParameterName = ParamSign + f;
                }
                if (cmd.Parameters.IndexOf(p.ParameterName) == -1)
                cmd.Parameters.Add(p);
            }
        }
        public List<string> GetSchema(string table)
        {
            if (m_con == null || m_con.State != ConnectionState.Open)
                throw new Exception("Connection not open!");

            string[] res = new string[4];
            res[2] = table;
            List<string> cols = new List<string>();
            DataTable dt = m_con.GetSchema("Columns", res);
            foreach (DataRow dr in dt.Rows)
            {
                cols.Add(dr["COLUMN_NAME"].ToString());
            }
            return cols;
        }
        #region 同步数据库操作
        /// <summary>
        /// 填充数据集.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="table">要填充的表，用逗号隔开.</param>
        public void Fill(DataSet ds, string[] tbls)
        {
            StringBuilder sb = new StringBuilder();
            DbDataAdapter SelectAdapter = NewDataAdapter();
            foreach (string s in m_scSelect)
            {
                sb.Append(s).Append(";");
            }

            DbCommand selectCmd = NewCommand(sb.ToString(), m_con);
            selectCmd.CommandType = CommandType.Text;
            SelectAdapter.SelectCommand = selectCmd;
            //设置参数
            AddParametersTo(selectCmd, true);
            //同步内存与数据库的表名
            //string[] tbls = table.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            if (tbls.Length < 1)
                return;
            string baseName = tbls[0];

            for (int i = 1; i < tbls.Length; i++)
            {
                SelectAdapter.TableMappings.Add(baseName + i.ToString(), tbls[i]);
            }
            try
            {
                SelectAdapter.Fill(ds, baseName);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                m_scSelect.Clear();
                m_TableNames.Clear();
                selectCmd.Dispose();
                SelectAdapter.Dispose();
            }
        }
        public void Fill(DataSet ds, string tables)
        {
            string[] tbls = tables.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Fill(ds, tbls);
        }
        public void Fill(DataSet ds)
        {
            string[] tbls = m_TableNames.ToArray();
            Fill(ds, tbls);
        }
        public void Fill(ref DataTable table)
        {
            DataSet ds = new DataSet();
            if (String.IsNullOrEmpty(table.TableName))
                throw new Exception("source table name can not be empty");
            Fill(ds);
            if (ds.Tables.Contains(table.TableName))
                table = ds.Tables[table.TableName];

            ds.Tables.Remove(table);
        }
        virtual protected bool ProcessError(DataTable tbl,out string msg)
        {
            msg = "";
            return true;
        }
        /// <summary>
        /// 将数据集同步到数据库.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="tables">同Fill方法.</param>
        public int Update(DataSet ds, string tables)
        {
            int ret = 0;
            string[] ts = tables.Split(',');
            bool opened = BeginBatch();
            try
            {
                foreach (string t in ts)
                {
                    if (m_dAdapters.ContainsKey(t) == false)
                    {
                        _Log.LogError("未找到可用的Adapter : " + t, "Update");
                        continue;
                    }
                    DbDataAdapter adapter = m_dAdapters[t];
                    if (ds.Tables.Contains(t) == false)
                        continue;
                    ret += adapter.Update(ds, t);
                    ds.Tables[t].AcceptChanges();
                    string msg;
                    if(ProcessError(ds.Tables[t],out msg) == false)
                    {
                        _Log.LogError(msg, "Adapter Update");
                        ret = -1;
                        break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                _Log.LogError(ex.Message, "Database update");
                ret = -1;
            }
            finally
            {
                if(opened == true)
                    EndBatch(ret == -1);
            }
            return ret;
        }
        public int Update(DataTable table, AbstractIdGengerator generator)
        {

            DataSet ds = null;
            if (table.DataSet == null)
            {
                ds = new DataSet();
                ds.Tables.Add(table);
            }
            else
                ds = table.DataSet;
            if (generator != null)
            {
                generator.Helper = this;
                if (generator.Gengerate(table) == false)
                    throw new Exception("Id generator error!");
            }
            return Update(ds, table.TableName);
        }
        public int Update(DataTable table)
        {
            return Update(table, null);
        }
        #endregion

        #region 数据表和对象映射(下面的代码需要重构，重复性太强)
        private Dictionary<MemberInfo, ColumnAttribute> EnumColumnMembers(Type type, string table)
        {
            MemberInfo[] mi = type.GetMembers(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            Dictionary<MemberInfo, ColumnAttribute> list = new Dictionary<MemberInfo, ColumnAttribute>();
            foreach (MemberInfo m in mi)
            {
                if (Attribute.IsDefined(m, typeof(ColumnAttribute)))
                {
                    ColumnAttribute dca = Attribute.GetCustomAttribute(m, typeof(ColumnAttribute)) as ColumnAttribute;
                    if (String.IsNullOrEmpty(table) == false &&
                        String.IsNullOrEmpty(dca.TableName) == false &&
                        dca.TableName != table)
                        continue;
                    list.Add(m, dca);
                }
            }
            return list;
        }
        #region 对象和表映射操作
        private Dictionary<MemberInfo, ColumnAttribute> EnumColumnFields(Type type)
        {
            return EnumColumnMembers(type, null);
        }
        public void MakeSchemaFromObj(DataTable table, Type type)
        {

            Dictionary<MemberInfo, ColumnAttribute> ms = EnumColumnMembers(type, table.TableName);
            foreach (KeyValuePair<MemberInfo, ColumnAttribute> p in ms)
            {
                ColumnAttribute dca = p.Value;
                if (dca != null)
                {
                    DataColumn dc = new DataColumn(dca.ColumnName);
                    dc.DataType = dca.DataType;
                    table.Columns.Add(dc);
                }
            }
        }
        public DataRow FillDataFromObj(DataTable table, object obj, DataRowState state)
        {
            if (table.Columns.Count == 0)
                MakeSchemaFromObj(table, obj.GetType());
            DataRow dr = table.NewRow();

            Dictionary<MemberInfo, ColumnAttribute> ms = EnumColumnMembers(obj.GetType(), dr.Table.TableName);
            foreach (KeyValuePair<MemberInfo, ColumnAttribute> p in ms)
            {
                ColumnAttribute dca = p.Value;
                MemberInfo m = p.Key;
                if (dca != null && dr.Table.Columns.Contains(dca.ColumnName))
                {

                    //dr[dca.ColumnName] = m.GetValue(obj);
                    if (m.MemberType.ToString() == "Field")
                    {
                        FieldInfo fi = m as FieldInfo;
                        dr[dca.ColumnName] = fi.GetValue(obj);
                    }
                    else
                    {
                        PropertyInfo pi = m as PropertyInfo;
                        dr[dca.ColumnName] = pi.GetValue(obj, null);
                    }
                }
            }
            table.Rows.Add(dr);
            if (state != DataRowState.Added)
                dr.AcceptChanges();
            if (state == DataRowState.Deleted)
                dr.Delete();
            else if (state == DataRowState.Modified)
                dr.SetModified();

            return dr;
        }
        public DataRow FillDataFromObj(DataTable table, object obj)
        {
            return FillDataFromObj(table, obj, DataRowState.Added);
        }
        public void Row2DbObj(DataRow dr, object obj)
        {

            //Type t = obj.GetType();
            //FieldInfo[] fi = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            //foreach (FieldInfo f in fi)
            //{
            //    if (Attribute.IsDefined(f, typeof(ColumnAttribute)))
            //    {

            //        ColumnAttribute dca = Attribute.GetCustomAttribute(f, typeof(ColumnAttribute)) as ColumnAttribute;
            //        if (String.IsNullOrEmpty(dca.TableName) == false && dca.TableName != dr.Table.TableName)
            //            continue;
            //        if (dca != null && dr.Table.Columns.Contains(dca.ColumnName))
            //        {

            //            object value = Convert.ChangeType(dr[dca.ColumnName], f.FieldType);
            //            f.SetValue(obj, value);
            //        }
            //    }
            //}
            Dictionary<MemberInfo, ColumnAttribute> ms = EnumColumnMembers(obj.GetType(), dr.Table.TableName);
            foreach (KeyValuePair<MemberInfo, ColumnAttribute> p in ms)
            {
                ColumnAttribute dca = p.Value;
                MemberInfo m = p.Key;
                if (dca != null && dr.Table.Columns.Contains(dca.ColumnName))
                {
                    object value;
                    if (m.MemberType.ToString() == "Field")
                    {
                        FieldInfo fi = m as FieldInfo;
                        value = Convert.ChangeType(dr[dca.ColumnName], fi.FieldType);
                        fi.SetValue(obj, value);
                    }
                    else
                    {
                        PropertyInfo pi = m as PropertyInfo;
                        value = Convert.ChangeType(dr[dca.ColumnName], pi.PropertyType);
                        pi.SetValue(obj, value, null);
                    }
                }

            }
        }
        public void Obj2Row(object obj, DataRow row)
        {
            Dictionary<MemberInfo, ColumnAttribute> fs = EnumColumnMembers(obj.GetType(), row.Table.TableName);
            foreach (KeyValuePair<MemberInfo, ColumnAttribute> p in fs)
            {
                ColumnAttribute dca = p.Value;
                MemberInfo m = p.Key;
                if (dca != null && row.Table.Columns.Contains(dca.ColumnName))
                {
                    object value = null;
                    if (m.MemberType.ToString() == "Field")
                    {
                        FieldInfo fi = m as FieldInfo;
                        value = fi.GetValue(obj);
                        fi.SetValue(obj, value);
                    }
                    else
                    {
                        PropertyInfo pi = m as PropertyInfo;
                        value = pi.GetValue(obj, null);
                        pi.SetValue(obj, value, null);
                    }

                    row[dca.ColumnName] = value;
                }
            }
        }
        #endregion
        #endregion
        /// <summary>
        /// 将自传递的参数(通过AddCustomParam函数传递的参数)转成DbParameter对象，
        /// 放进DbCommand的参数集合中.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        private void AddParametersTo(DbCommand cmd, bool ClearParams)
        {
            foreach (KeyValuePair<string, object> kv in m_dCustomParam)
            {
                DbParameter param = NewParameter();
                param.ParameterName = kv.Key;
                param.DbType = DbType.String;
                param.Value = kv.Value;
                //param.
                cmd.Parameters.Add(param);
            }
            //用户参数用完后都进行清空操作
            if (ClearParams)
                m_dCustomParam.Clear();
        }
        #region 直接执行SQL语句
        public DbDataReader ExcuteSQL(string SqlString)
        {
            return ExcuteSQL(SqlString, true);
        }
        public DbDataReader ExcuteSQL(string SqlString, bool ClearParams)
        {
            DbCommand cmd = NewCommand(SqlString, m_con);
            AddParametersTo(cmd, ClearParams);
            DbDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                _Log.LogError(ex.Message, "ExcuteSQL");
                return null;
            }

            return reader;
        }
        /// <summary>
        /// 执行SQL命令，只返回第一行第一列的值，适合只返回一个值的查询，如聚合查询.
        /// </summary>
        /// <param name="SqlString">The SQL string.</param>
        /// <returns>如果返回对象为空，则为空字符串</returns>
        public object ExcuteForUnique(string SqlString, bool ClearParams)
        {
            DbCommand cmd = NewCommand(SqlString, m_con);
            
            AddParametersTo(cmd, ClearParams);
            object obj = cmd.ExecuteScalar();
            if (obj == null || obj.GetType() == typeof(System.DBNull))
                obj = null;
            return obj;
        }
        public object ExcuteForUnique(string SqlString)
        {
            return ExcuteForUnique(SqlString, true);
        }
        public T ExcuteForUnique<T>(string SqlString, bool ClearParams) where T : class
        {
            DbCommand cmd = NewCommand(SqlString, m_con);
            AddParametersTo(cmd, ClearParams);
            T rtn = cmd.ExecuteScalar() as T;
            return rtn;
        }
        public T ExcuteForUnique<T>(string SqlString) where T : class
        {
            return ExcuteForUnique<T>(SqlString, true);
        }
        public void ExcuteNonQuery(string SqlString)
        {
            DbCommand cmd = NewCommand(SqlString, m_con);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 多表查询.
        /// </summary>
        /// <param name="sqlSelect">The SQL select.</param>
        /// <param name="AddTableName">if set to <c>true</c> [add table name].</param>
        /// <returns></returns>
        public abstract DataTable MultiTableSelect(string sqlSelect, bool AddTableName);
        private DbTransaction m_Trx = null;
        public bool BeginBatch()
        {
            //存在事务，直接返回
            if (m_Trx != null)
                return true;
            try
            {
                m_Trx = m_con.BeginTransaction();
            }
            catch (System.Exception ex)
            {
                _Log.LogError(ex.Message, "BeginBatch");
                return false;
            }
            return true;
        }
        public bool EndBatch()
        {
            return EndBatch(false);
        }
        /// <summary>
        /// 更新修改的数据时，如果数据不变报错有时可忽略，其它的异常需处理，处理时需自行调用BeginBatch和EndBatch
        /// </summary>
        /// <param name="forceRollback">if set to <c>true</c> [force rollback].</param>
        public bool EndBatch(bool forceRollback)
        {
            if (m_Trx == null)
            {
                this._Log.LogWarm("不存在事务上下文");
                return false;
            }
            try
            {
                if (forceRollback)
                    m_Trx.Rollback();
                else
                    m_Trx.Commit();
            }
            catch (System.Exception ex)
            {
                _Log.LogError(ex.Message, "EndBatch");
                m_Trx.Rollback();
                return false;
            }
            finally
            {
                m_Trx.Dispose();
                m_Trx = null;
            }
            return true;
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            Reset();
            if (m_con != null)
            {
                m_con.Close();
                m_con.Dispose();
                m_con = null;
            }
        }

        #endregion
    }
    /// <summary>
    /// 标注类的字段或属性，只有标注，调用Obj2Row或Row2Obj才会自动填值
    /// 示例：  [Column(ColumnName="id",DataType=typeof(int),TableName="mytable")]
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {

        private string _ColumnName;
        private Type _type;
        private string _TableName;
        public ColumnAttribute()
        {
            _type = typeof(object);
            _TableName = String.Empty;
        }
        public string ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        public Type DataType
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}