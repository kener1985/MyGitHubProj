using System;
using System.Collections.Generic;
using System.Text;

using System.Data.Common;
using System.Data;
using UtilHelper.Log;
using MySql.Data.MySqlClient;

namespace UtilHelper.Database
{
    public class MySQLHelper : AbstractDbHelper
    {
        //Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;
        public MySQLHelper(string connStr,LogHelper<TxtLogWriter> log) : base(connStr,log)
        {
            ParamSign = "@";
            //if (this.Conn.State == System.Data.ConnectionState.Open)
            //{
                
            //    this.ExcuteNonQuery("set character_set_results=" + charset);
            //    this.ExcuteNonQuery("set character_set_client=" + charset);
            //    this.ExcuteNonQuery("set character_set_connection=" + charset);
            //}
        }
        /// <summary>
        /// News the command.
        /// </summary>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="con">The con.</param>
        /// <returns></returns>
        protected override System.Data.Common.DbCommand NewCommand(string cmdText, System.Data.Common.DbConnection con)
        {
            MySqlConnection conn = con as MySqlConnection;
            if (conn == null)
                throw new ArgumentException("Not MySqlConnection");
            
            return new MySqlCommand(cmdText, conn);
        }
        /// <summary>
        /// News the data adapter.
        /// </summary>
        /// <returns></returns>
        protected override System.Data.Common.DbDataAdapter NewDataAdapter()
        {
            MySqlDataAdapter adpter = new MySqlDataAdapter();
            adpter.ContinueUpdateOnError = true;//Update时，如果更新内容不变，会报错
            return adpter;
        }
        /// <summary>
        /// News the parameter.
        /// </summary>
        /// <returns></returns>
        protected override System.Data.Common.DbParameter NewParameter()
        {
            return new MySqlParameter();
        }
        protected override System.Data.Common.DbConnection NewConnection(string sConStr)
        {

            return new MySqlConnection(sConStr);
        }
        protected override System.Data.Common.DbParameterCollection NewParameterCollection()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 多表查询.
        /// </summary>
        /// <param name="sqlSelect">多表关联查询语句，MySQL 如果多表中有重复列名，需用as重命名，否则会报错.</param>
        /// <param name="AddTableName">这里无用</param>
        /// <returns>返回查询结果</returns>
        public override System.Data.DataTable MultiTableSelect(string sqlSelect, bool AddTableName)
        {
            DbDataReader r = ExcuteSQL(sqlSelect);
            //MySql将结果集放于Schema中
            if (r == null)
                throw new ArgumentException("[MultiTableSelect] - 查询出错");
            DataTable schema = r.GetSchemaTable();
            DataTable result = new DataTable();
            foreach (DataRow row in schema.Rows)
            {
                string cname = row.Field<string>("ColumnName");
                result.Columns.Add(cname);
            }
            while(r.Read())
            {
                DataRow nr = result.NewRow();
                foreach (DataColumn c in result.Columns)
                {
                    nr[c.ColumnName] = r[c.ColumnName];
                    
                }
                result.Rows.Add(nr);
            }
            if(r.IsClosed == false)
                r.Close();

            return result;
        }
        protected override bool ProcessError(DataTable tbl, out string msg)
        {
            //string except = "违反并发性: UpdateCommand 影响了预期 1 条记录中的 0 条。";
            //string except_eng = "Concurrency violation: the UpdateCommand affected 0 of the expected 1 records.";
            foreach (DataRow r in tbl.Rows)
            {
                if(r.HasErrors)
                {
                    msg = r.RowError;
                    return false;
                }
            }
            msg = "";
            return true;
        }
        
    }
}

