using System;
using System.Data;
using System.Data.Common;

namespace UtilHelper.Database
{
    public class SQLiteHelper : AbstractDbHelper
    {
        public SQLiteHelper(System.Data.SQLite.SQLiteConnection sqliteCon)
            : base(sqliteCon, UtilHelper.Log.LogHelper<UtilHelper.Log.TxtLogWriter>.CreateLogger(String.Empty))
        {
            ParamSign = "@";
        }
        /// <summary>
        /// News the command.
        /// </summary>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="con">The con.</param>
        /// <returns></returns>
        protected override System.Data.Common.DbCommand NewCommand(string cmdText, System.Data.Common.DbConnection con)
        {
            System.Data.SQLite.SQLiteConnection sqliteCon = con as System.Data.SQLite.SQLiteConnection;
            if (sqliteCon == null)
                throw new ArgumentException("Not SQLiteConnection");
            return new System.Data.SQLite.SQLiteCommand(cmdText, sqliteCon);
        }
        /// <summary>
        /// News the data adapter.
        /// </summary>
        /// <returns></returns>
        protected override System.Data.Common.DbDataAdapter NewDataAdapter()
        {
            return new System.Data.SQLite.SQLiteDataAdapter();
        }
        /// <summary>
        /// News the parameter.
        /// </summary>
        /// <returns></returns>
        protected override System.Data.Common.DbParameter NewParameter()
        {
            return new System.Data.SQLite.SQLiteParameter();
        }
        protected override System.Data.Common.DbConnection NewConnection(string sConStr)
        {
            return new System.Data.SQLite.SQLiteConnection(sConStr);
        }
        protected override System.Data.Common.DbParameterCollection NewParameterCollection()
        {
            throw new NotImplementedException();
        }
        public override DataTable MultiTableSelect(string sqlSelect, bool AddTableName)
        {
            DbDataReader r = ExcuteSQL(sqlSelect);
            DataTable st = r.GetSchemaTable();
            DataTable tbl = new DataTable();
            var schemas = from s in st.AsEnumerable()
                          select new
                          {
                              TableName = s.Field<string>("BaseTableName"),
                              ColumnName = s.Field<string>("ColumnName"),
                              DataType = s.Field<Type>("DataType")
                          };

            foreach (var v in schemas)
            {
                string name = v.ColumnName;
                if (AddTableName)
                    name = v.TableName + "." + name;
                DataColumn dc = new DataColumn(name);
                dc.DataType = v.DataType;
                tbl.Columns.Add(dc);
            }

            int Count = r.FieldCount;
            while (r.Read())
            {
                DataRow row = tbl.NewRow();
                for (int i = 0; i < Count; i++)
                {
                    row[i] = r.GetValue(i);
                }

                tbl.Rows.Add(row);
            }
            return tbl;
        }
        //#region 直接执行SQL语句
        //public System.Data.SQLite.SQLiteDataReader Excute()
        //{
        //    return base.ExcuteSQL("") as System.Data.SQLite.SQLiteDataReader;
        //}
        //#endregion
    }
}
