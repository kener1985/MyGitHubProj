using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BaseLib.Extends
{
    public static class Extends
    {
        public static void Init(this DataTable table, string name, string fields)
        {
            string[] fs = fields.Split(new char[]{','});
            foreach (string f in fs)
            {
                table.Columns.Add(f);
            }

            table.TableName = name;
        }

    }

}
