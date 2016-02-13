using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace UtilHelper.HttpExtention
{
    public static class DataTransferExtention
    {
        /// <summary>
        /// Gets the single condition.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public static DataRow GetSingleCondition(this DataTable table)
        {
            if (table.Rows.Count < 1)
                throw new Exception("no any data found");

            return table.Rows[0];
        }
        /// <summary>
        /// Gets the single condition by key.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetSingleConditionByKey(this DataTable table, string key)
        {
            if (table.Rows.Count < 1)
                throw new Exception("no any data found");

            if (table.Columns.Contains(key))
            {
                return table.Rows[0][table.Columns[key]].ToString();
            }
            return String.Empty;
        }
        /// <summary>
        /// Toes the dictionary.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public static System.Collections.Specialized.StringDictionary ToDictionary(this DataRow row)
        {
            DataColumnCollection columns = row.Table.Columns;
            System.Collections.Specialized.StringDictionary dic = new System.Collections.Specialized.StringDictionary();
            foreach (DataColumn dc in columns)
            {
                if(row.IsNull(dc) == false)
                {
                    dic.Add(dc.ColumnName, row[dc].ToString());
                }
            }
            return dic;
        }
        public static void FromDictionary(this DataRow row,System.Collections.Specialized.StringDictionary sd)
        {
            if(row.Table == null)
                return;

            //建立row的schema
            DataColumnCollection dcs = row.Table.Columns;
            foreach (string key in sd.Keys)
            {
                DataColumn dc = new DataColumn(key);
                dcs.Add(dc);
                row[key] = sd[key];
            }
        }
    }
}
