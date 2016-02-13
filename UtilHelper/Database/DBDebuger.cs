using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace UtilHelper.Database
{
    public sealed class DBDebuger
    {
        private Tip.MB _Box = new UtilHelper.Tip.MB("DBDebuger");
        #region 调试用

        public void ShowTable(DataTable tbl, string sFields)
        {
            string[] fields = sFields.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            DataRowCollection rows = tbl.Rows;
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in rows)
            {
                foreach (string field in fields)
                {
                    sb.Append(field).Append("=").Append(row[field].ToString()).Append('\t');
                }
                sb.Append("\r\n");
            }
            _Box.Info(sb.ToString());
        }
        public void ShowTable(DataTable table)
        { 
            if (table == null || table.Columns.Count == 0)
                _Box.Error("no columns");
            else
            {
                StringBuilder sz = new StringBuilder();
                foreach (DataColumn dc in table.Columns)
                {
                    sz.Append(dc.ColumnName).Append(',');
                }
                ShowTable(table, sz.ToString());
            }
        }
        
        public void ShowRow(DataRow row, string sFields)
        {
            StringBuilder sb = new StringBuilder();
            string[] fields = System.Text.RegularExpressions.Regex.Split(sFields, ",");

            foreach (string field in fields)
            {
                if (String.IsNullOrEmpty(field))
                    continue;
                sb.Append(field).Append("=").Append(row[field].ToString()).Append('\t');
            }
            sb.Append(row.RowState.ToString());
            sb.Append("\r\n");
            _Box.Info(sb.ToString());
        }
        public void ShowRow(DataRow row)
        {
            if (row ==null || row.Table == null || row.Table.Columns.Count == 0)
                _Box.Error("table is empty");
            else
            {
                StringBuilder sz = new StringBuilder();
                foreach (DataColumn dc in row.Table.Columns)
                {
                    sz.Append(dc.ColumnName).Append(',');
                    
                }
                ShowRow(row, sz.ToString());
            }
        }
        public void ShowColumns(DataRow row)
        {
            ShowColumns(row.Table);
        }
        public void ShowColumns(DataTable table)
        {
            StringBuilder sz = new StringBuilder();
            foreach (DataColumn dc in table.Columns)
            {
                sz.AppendLine(dc.ColumnName);
            }
            _Box.Info(sz.ToString());
        }
        #endregion
    }
}
