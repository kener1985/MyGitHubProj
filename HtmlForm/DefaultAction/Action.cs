using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilHelper.Database;
using System.Data;
using BaseLib.Extends;
using System.IO;
using DefaultAction;
using UtilHelper.Http;
using UtilHelper.Converter;
using System.Xml;
using System.Data.Common;

namespace BaseLib
{
    public class GetAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {

            //StrDictionary sd = Utility.ParseQuery(url.OriginalString);
            //int iData = sDec.IndexOf("?");
            //    string sDec = System.Web.HttpUtility.UrlDecode(sd["data"]);

            //    string json = "{\"data\":" + sDec + "}";
            //    MessageBox.Show(json);
            //    Package p = new Package();

            //    try
            //    {
            //        p = UtilHelper.Converter.JSonXmlConverter.Json2Obj<Package>(json);

            //    }
            //    catch (System.Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}

            //public override void ParseQuery(Uri url, out StrDictionary sd)
            //{
            //    sd = Utility.ParseQuery(url.OriginalString);
        }
        #endregion
        public bool IsMe(string schema)
        {
            return schema.Equals("get");
        }
    }
    public class PostAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            string action = "default";
            if (sd.ContainsKey("action") && !String.IsNullOrEmpty(sd["action"]))
                action = sd["action"];
            Utility.InvokeAction(action, sd["cmd"], sd);
        }
        #endregion
        public bool IsMe(string schema)
        {
            return schema.Equals("post");
        }
    }


    public class SelectAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            AbstractDbHelper helper = GlobalVar.DBHelper;

            string name = sd["table"];
            if (sd.ContainsKey("limit") && String.IsNullOrEmpty(sd["limit"]) == false)
                helper.AddSelectWithLimit(name, sd["fields"], sd["limit"]);
            else
                helper.AddSelect(name, sd["fields"]);
            //helper.AddSelect(name, sd["fields"]);
            EasyUITable tbl = new EasyUITable();
            DataTable t = new DataTable(name);
            helper.Fill(ref t);
            GlobalVar.transferCodeToName(t, "opr");
            tbl.Table = t;
            bool isLong = (sd.ContainsKey("longdate") && sd["longdate"].Equals("true")) ? true : false;
            GlobalVar.AddDateFildFromSeqnbr(tbl.Table, isLong);
            GlobalVar.Container.InvokeScript(sd["invoke"], new object[] { tbl.ToJson() });
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("select");

        }

        #endregion
    }
    public class UpdateAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {

            EasyUITable etb = new EasyUITable();
            etb.Parse(sd["data"]);
            etb.Table.TableName = sd["table"];
            etb.Table.AcceptChanges();
            if (etb.Table.Columns.Contains("id") == false)
            {
                GlobalVar.Log.LogError("找不到id字段", "UpdateAction");
                throw new Exception();
            }
            GlobalVar.DBHelper.AddUpdate(sd["table"], sd["fields"], "id");
            GlobalVar.DBHelper.AddInsert(sd["table"], sd["fields"]);
            GlobalVar.DBHelper.AddDelete(sd["table"], "id");
            string[] delIds = sd["delids"].Split(new char[] { ',' });

            foreach (DataRow r in etb.Table.Rows)
            {
                string id = r.Field<string>("id");
                if (String.IsNullOrEmpty(id))//新增记录
                {
                    r["id"] = null;//不设为null，插入时失败
                    if (sd.ContainsKey("statusfield") &&
                        etb.Table.Columns.Contains(sd["statusfield"]) &&
                        String.IsNullOrEmpty(r.Field<String>(sd["statusfield"])))
                        r.SetField<int>(sd["statusfield"], 0);

                    r.AcceptChanges();
                    r.SetAdded();
                }
                else if (delIds.Contains(id))//删除记录
                {
                    if (sd.ContainsKey("statusfield"))
                    {
                        r.SetField<char>(sd["statusfield"], '1');
                    }
                    else
                        r.Delete();
                }
                else
                {
                    r.SetModified();//更新记录
                }
            }
            int cnt = GlobalVar.DBHelper.Update(etb.Table);
            if (cnt > 0 && sd.ContainsKey("invoke"))
            {
                GlobalVar.Container.InvokeScript(sd["invoke"], null);
            }
            if (cnt > 0)
                MessageBox.Show("更新成功!");
            else
                MessageBox.Show("更新失败，请查看日志!");
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("update");
        }

        #endregion
    }
    public class SelPicAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图像文件 (*.jpg)|*.jpg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
                string prefix = GlobalVar.AppPath + "views\\images\\";
                if (String.IsNullOrEmpty(sd["src"]) == false)//删除原有样版
                {
                    File.Delete(prefix + sd["src"]);
                }

                string dest = prefix + filename;
                File.Copy(ofd.FileName, dest);
                GlobalVar.Container.InvokeScript("setSelectedPicture", new object[] { filename });
            }

        }

        public bool IsMe(string schema)
        {
            return schema.Equals("selpic");

        }

        #endregion
    }

    public class QueryProduct : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            EasyUITable etb = new EasyUITable();
            DataTable tbl = new DataTable("products");

            GlobalVar.DBHelper.AddSelectWithLimit("products", sd["fields"], "innerid=@innerid AND status!='1'");

            GlobalVar.DBHelper.AddCustomParam("@innerid", sd["data"]);

            GlobalVar.DBHelper.Fill(ref tbl);
            etb.Table = tbl;
            GlobalVar.Container.InvokeScript(sd["invoke"], new object[] { etb.ToJson() });
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("query_product");

        }

        #endregion
    }
    public class BalancebillAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            GlobalVar.DBHelper.AddInsert("billitem", "productid,num,type,saleoff,mark,seqnbr,saleprice,c_num");
            GlobalVar.DBHelper.AddInsert("bills", "seqnbr,payer,purunit,mobile,pid,operator,pagecode,type");


            //billitem表
            EasyUITable etb = new EasyUITable();
            etb.Parse(sd["data"]);
            DateTime dtNow = DateTime.Now;

            etb.Table.Columns.Add("seqnbr");
            foreach (DataRow r in etb.Table.Rows)
            {
                r.SetField<long>("seqnbr", dtNow.Ticks);
                r.AcceptChanges();
                r.SetAdded();
            }

            etb.Table.TableName = "billitem";

            //bills 表
            DataTable tbBill = new DataTable();
            tbBill.Init("bills", "seqnbr,operator,mobile,payer,pid,purunit,pagecode,type");
            DataRow row = tbBill.NewRow();
            row.SetField<long>("seqnbr", dtNow.Ticks);
            row.SetField<string>("operator", GlobalVar.LogInfo.WorkCode);
            row.SetField<string>("mobile", sd["mobile"]);
            row.SetField<string>("payer", sd["payer"]);
            row.SetField<string>("purunit", sd["purunit"]);
            row.SetField<string>("pid", sd["pid"]);
            row.SetField<string>("pagecode", sd["pagecode"]);
            row.SetField<string>("type", sd["type"]);
            tbBill.Rows.Add(row);

            //products 表
            string idset = GetIdSet(etb.Table, "productid");
            DataTable tbProd = new DataTable("products");
            GlobalVar.DBHelper.AddSelectWithLimit("products", "id,storenum", "id IN " + idset);
            GlobalVar.DBHelper.AddUpdate("products", "id,storenum,size,position", "id");
            GlobalVar.DBHelper.Fill(ref tbProd);

            //storehistory表
            GlobalVar.DBHelper.AddInsert("storenumhistory", "mark,num,seqnbr,type,productid,finalstore,opr,customer");
            DataTable tblHis = new DataTable();
            tblHis.Init("storenumhistory", "mark,num,seqnbr,type,productid,finalstore,opr,customer");

            //账务明细表
            GlobalVar.DBHelper.AddInsert("debtdetail", "billseq,opr,mark,seqnbr,amount,type");
            DataTable tblDebt = new DataTable();
            tblDebt.Init("debtdetail", "billseq,seqnbr,opr,mark,date,amount,type");
            DataRow debtrow = tblDebt.NewRow();
            debtrow.SetField<string>("opr", GlobalVar.LogInfo.WorkCode);
            debtrow.SetField<long>("seqnbr", dtNow.Ticks);
            debtrow.SetField<string>("amount", sd["amount"]);
            debtrow.SetField<string>("type", sd["type"]);

            if (sd["pid"] != "0")
            {
                string seq = GlobalVar.DBHelper.ExcuteForUnique<string>("select seqnbr from bills where id=" + sd["pid"]);
                debtrow.SetField<string>("billseq", seq);
            }
            else
            {
                debtrow.SetField<long>("billseq", dtNow.Ticks);
            }
            tblDebt.Rows.Add(debtrow);
            //平衡货品数量
            Balance(etb.Table, tbProd, tblHis, sd);
            DataSet ds = new DataSet();
            ds.Tables.AddRange(new DataTable[] { etb.Table, tbProd, tbBill, tblHis, tblDebt });
            GlobalVar.DBHelper.BeginBatch();

            //新增账单数据 更新货品数据
            int ret = GlobalVar.DBHelper.Update(ds, "billitem,products,bills,storenumhistory,debtdetail");
            //int ret = GlobalVar.DBHelper.Update(ds, "billitem,bills,debtdetail");
            bool noErr = Utility.ProcessSqlError(ds);
            GlobalVar.DBHelper.EndBatch(noErr == false);
            //账单录入版暂不进行打印
            //if (noErr)
            //    BillPrinter.Print(etb.Table, sd);
            GlobalVar.Container.InvokeScript("resultCallback", new object[] { noErr });
        }
        private void Balance(DataTable tbBill, DataTable tbProd, DataTable tblHis, StrDictionary sd)
        {
            foreach (DataRow r in tbBill.Rows)
            {
                DataRow pr = FindRow(tbProd, "id", r.Field<string>("productid"));
                if (pr == null) throw new Exception("结算时，数据丢失!");

                int n1 = pr.Field<int>("storenum");
                int result = 0;
                if (n1 > 0)
                {
                    int n2 = Convert.ToInt32(r.Field<string>("num"));
                    //1 出货 2 退货
                    result = (r.Field<string>("type").Equals("O")) ? (n1 - n2) : (n1 + n2);
                    pr.SetField<int>("storenum", result);
                }
                //
                DataRow nr = tblHis.NewRow();
                nr.SetField<string>("customer", sd["purunit"]);//#表示系统未对库存进行修改
                nr.SetField<string>("mark", r.Field<string>("mark"));
                nr.SetField<string>("num", r.Field<string>("num"));
                nr.SetField<long>("seqnbr", DateTime.Now.Ticks);
                nr.SetField<string>("type", r.Field<string>("type"));
                nr.SetField<string>("productid", r.Field<string>("productid"));
                nr.SetField<int>("finalstore", result);
                nr.SetField<string>("opr", GlobalVar.LogInfo.WorkCode);

                tblHis.Rows.Add(nr);
            }
        }
        private DataRow FindRow(DataTable tbl, string field, string value)
        {
            foreach (DataRow r in tbl.Rows)
            {
                object tv = r[field];
                if (tv != null && tv.ToString().Equals(value))
                    return r;
            }
            return null;
        }
        private string GetIdSet(DataTable tbl, string idfield)
        {
            StringBuilder sz = new StringBuilder(64);
            sz.Append('(');
            for (int i = 0; i < tbl.Rows.Count; ++i)
            {
                if (i != 0)
                    sz.Append(',');
                sz.Append(tbl.Rows[i].Field<string>(idfield));
            }
            sz.Append(')');
            return sz.ToString();
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("balancebill");

        }

        #endregion
    }

    public class PopupFrmAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            DefaultAction.PopupFrm frm = new DefaultAction.PopupFrm(sd);
            frm.ShowDialog();

        }

        public bool IsMe(string schema)
        {
            return schema.Equals("popup");

        }

        #endregion
    }

    public class QueryBillDetailAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            EasyUITable etb = new EasyUITable();
            string seqnbr = sd["seqnbr"];
            string ivk = String.Empty;
            if (seqnbr[0] == '#')//查账务统计表
            {

                seqnbr = seqnbr.Substring(1);
                GlobalVar.DBHelper.AddSelectWithLimit("debtdetail", "amount,seqnbr,mark,opr,type", "billseq=@seq");
                GlobalVar.DBHelper.AddCustomParam("@seq", seqnbr);
                DataTable tbl = new DataTable("debtdetail");
                GlobalVar.DBHelper.Fill(ref tbl);
                GlobalVar.AddDateFildFromSeqnbr(tbl, true);
                etb.Table = tbl;
                ivk = "showDebtSummary";
            }
            else
            {
                GlobalVar.DBHelper.AddCustomParam("@seqnbr", sd["seqnbr"]);
                StringBuilder sz = new StringBuilder();
                sz.Append("SELECT p.name,p.productid,p.innerid,p.position,p.colornum,p.packagenum,p.size,b.c_num,b.num,b.saleprice,b.type,b.mark,b.saleoff")
                .Append(" FROM billitem as b,products as p where p.id=b.productid AND b.seqnbr=@seqnbr");
                etb.Table = GlobalVar.DBHelper.MultiTableSelect(sz.ToString(), false);//第二个参数无作用
                ivk = "showDetail";
            }
            GlobalVar.transferCodeToName(etb.Table, "opr");
            GlobalVar.Container.InvokeScript(ivk, new object[] { etb.ToJson() });
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("billdetail");

        }

        #endregion
    }
    public class LogoutAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            GlobalVar.MainFrame.Hide();
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("logout");

        }

        #endregion
    }
    public class QueryRoleInfoAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            GlobalVar.Container.InvokeScript(sd["invoke"], new object[] { GlobalVar.LogInfo.Role, GlobalVar.LogInfo.Name });
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("queryroleinfo");

        }

        #endregion
    }
    public class InitBillInfoAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            string name = GlobalVar.LogInfo.Name;
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string phis = String.Empty;
            int pagecode;
            if (sd.ContainsKey("id") == false || sd["id"] == "" || sd["id"] == "0")
                pagecode = 1;
            else
            {
                string pc = GlobalVar.DBHelper.ExcuteForUnique<string>("select max(pagecode) from bills where pid=" + sd["id"]);
                DbDataReader r = GlobalVar.DBHelper.ExcuteSQL("select max(pagecode) from bills where pid=" + sd["id"]);
                r.Read();
                if (r.IsDBNull(0))
                    pagecode = 2;
                else
                    pagecode = r.GetInt32(0) + 1;
                r.Close();
                string lmt = "id=" + sd["id"] + " OR pid=" + sd["id"];
                //if (pagecode > 2)
                //{
                //    string pid = GlobalVar.DBHelper.ExcuteForUnique<string>("select pid from bills where id=" + sd["id"]);
                //    lmt += " OR id="+pid;
                // }
                DataTable tblBill = new DataTable("bills");
                GlobalVar.DBHelper.AddSelectWithLimit("bills", "seqnbr", lmt);
                GlobalVar.DBHelper.Fill(ref tblBill);

                string seqset = GetSeqnbrSet(tblBill);
                //GlobalVar.DBHelper.AddCustomParam("@seqset", seqset);
                StringBuilder sz = new StringBuilder();
                sz.Append("SELECT p.productid,p.innerid,p.colornum,b.saleprice,b.seqnbr")
                    .Append(" FROM billitem as b,products as p where p.id=b.productid AND b.seqnbr IN ")
                    .Append(seqset);
                EasyUITable etb = new EasyUITable();
                etb.Table = GlobalVar.DBHelper.MultiTableSelect(sz.ToString(), false);
                GlobalVar.AddDateFildFromSeqnbr(etb.Table, false);
                //MessageBox.Show(tblitems.Rows.Count.ToString());
                //etb.Table = FilterForsingle(etb.Table);
                phis = etb.ToJson();
            }

            GlobalVar.Container.InvokeScript(sd["invoke"], new object[] { name, date, pagecode, phis });

        }
        private DataTable FilterForsingle(DataTable tbl)
        {
            DataTable rst = new DataTable();
            StrDictionary sd = new StrDictionary();
            rst.Init("", "item,price");
            foreach (DataRow r in tbl.Rows)
            {
                string item = r.Field<string>("innerid");
                if (String.IsNullOrEmpty(item))
                    item = r.Field<string>("productid");

                item += " - ";
                item += r.Field<string>("colornum");

                if (sd.ContainsKey(item) == false)
                {
                    sd[item] = "";
                    DataRow nr = rst.NewRow();
                    nr.SetField<string>("item", item);
                    nr.SetField<decimal>("price", r.Field<decimal>("saleprice"));
                    rst.Rows.Add(nr);
                }
            }
            return rst;
        }
        private string GetSeqnbrSet(DataTable bills)
        {
            StringBuilder sz = new StringBuilder();
            sz.Append('(');
            for (int i = 0; i < bills.Rows.Count; ++i)
            {
                DataRow r = bills.Rows[i];
                if (i != 0)
                    sz.Append(',');
                sz.Append(r.Field<string>("seqnbr"));
            }
            sz.Append(')');

            return sz.ToString();
        }
        public bool IsMe(string schema)
        {
            return schema.Equals("initbillinfo");

        }

        #endregion
    }
    public class QueryInneridAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            EasyUITable etb = new EasyUITable();
            DataTable tbl = new DataTable("products");
            string keyword = sd["data"].Trim();
            GlobalVar.DBHelper.AddSelectWithLimit("products", "innerid", "innerid LIKE @keyword AND status!='1' GROUP BY innerid");
            GlobalVar.DBHelper.AddCustomParam("@keyword", "%" + keyword + "%");
            GlobalVar.DBHelper.Fill(ref tbl);
            etb.Table = tbl;

            GlobalVar.Container.InvokeScript("inneridAutoComplete", new object[] { etb.ToJson() });
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("queryinnenid");

        }

        #endregion
    }
    public class StoreChangeAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            int num = Convert.ToInt32(sd["num"]);
            string fields = "mark,num,seqnbr,type,productid,finalstore,opr,customer";
            GlobalVar.DBHelper.AddInsert("storenumhistory", fields);
            GlobalVar.DBHelper.AddUpdate("products", "id,storenum", "id");
            DefaultAction.StoreChangeFrm frm = new DefaultAction.StoreChangeFrm(num);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataTable his = new DataTable();
                DataTable upt = new DataTable();
                his.Init("storenumhistory", fields);
                upt.Init("products", "id,storenum,actcnt");

                //Products表更新
                DataRow r = upt.NewRow();
                decimal final = frm.Value;
                if (frm.OprType == 'I')//进货
                    final = num + frm.Value;
                else if (frm.OprType == 'O')//出货
                    final = num - frm.Value;

                r.SetField<string>("id", sd["data"]);
                r.SetField<decimal>("storenum", final);
                upt.Rows.Add(r);
                r.AcceptChanges();
                r.SetModified();
                DataSet ds = new DataSet();
                int want = 2;//理想影响条数
                r = his.NewRow();
                //storenumhistory 表增加记录

                r.SetField<string>("mark", frm.Mark);
                r.SetField<decimal>("finalstore", final);

                if (frm.OprType == 'M')
                    r.SetField<decimal>("num", num);
                else
                    r.SetField<decimal>("num", frm.Value);

                r.SetField<long>("seqnbr", DateTime.Now.Ticks);
                r.SetField<char>("type", frm.OprType);
                r.SetField<string>("productid", sd["data"]);
                r.SetField<string>("opr", GlobalVar.LogInfo.WorkCode);
                r.SetField<string>("customer", frm.Customer);
                his.Rows.Add(r);

                ds.Tables.Add(his);
                ds.Tables.Add(upt);

                int rst = GlobalVar.DBHelper.Update(ds, "storenumhistory,products");
                try
                {
                    GlobalVar.DBHelper.ExcuteNonQuery("update products set actcnt=actcnt+1 where id=" + sd["data"]);
                }
                catch (Exception) { }
                GlobalVar.Container.InvokeScript("updateStoreNum", new object[] { final, rst == want });
            }


        }

        public bool IsMe(string schema)
        {
            return schema.Equals("storechange");

        }

        #endregion
    }

    public class AddProductAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            AddProductFrm frm = new AddProductFrm();
            frm.ShowDialog();
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("addproduct");

        }

        #endregion
    }
    public class WidgeCalAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            WidgeCalFrm frm = new WidgeCalFrm();
            frm.ShowDialog();

        }

        public bool IsMe(string schema)
        {
            return schema.Equals("widgecal");

        }

        #endregion
    }

    public class PrintBillAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            EasyUITable etb = new EasyUITable();
            etb.Parse(sd["data"]);
            BillPrinter.Print(etb.Table, sd);
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("printbill");

        }

        #endregion
    }
    public class QueryBillsAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {

            string qs = "";
            if (String.IsNullOrEmpty(sd["data"]))
            {
                qs = MakeQueryBillStr(sd);
                if (qs == null)
                {
                    GlobalVar.Container.InvokeScript(sd["invoke"], new object[] { "" });
                    return;
                }
            }
            else//查明细
            {
                StringBuilder sz = new StringBuilder();
                string id = sd["data"];
                if (sd.ContainsKey("main") && sd["main"] == "true")//记录为主单
                {
                    sz.Append("id=").Append(id).Append(" OR pid=").Append(id);
                }
                else//记录为副单，先查出主单id
                {
                    object pid = GlobalVar.DBHelper.ExcuteForUnique("select pid from bills where id=" + id);
                    sz.Append("id=").Append(pid).Append(" OR pid=").Append(pid);
                }
                sz.Append(" ORDER BY seqnbr DESC");
                qs = sz.ToString();
            }


            EasyUITable etb = new EasyUITable();
            DataTable tbl = new DataTable("bills");
            string fields = "id,payer,seqnbr,purunit,operator,mobile,pid,pagecode,type";

            //MessageBox.Show(qs);
            GlobalVar.DBHelper.AddSelectWithLimit("bills", fields, qs);

            GlobalVar.DBHelper.Fill(ref tbl);
            //MessageBox.Show(tbl.Rows.Count.ToString());
            etb.Table = tbl;
            GlobalVar.AddDateFildFromSeqnbr(etb.Table, false);
            GlobalVar.transferCodeToName(etb.Table, "operator");
            GlobalVar.Container.InvokeScript(sd["invoke"], new object[] { etb.ToJson(), sd["data"] });

        }
        private string MakeQueryBillStr(StrDictionary sd)
        {
            string b = sd["begdat"];
            string e = sd["enddat"];
            string punit = sd["purunit"];
            string opr = sd["operator"];
            bool mo = sd["mainonly"] == "true";
            StringBuilder sz = new StringBuilder();
            if (String.IsNullOrEmpty(b) == false && String.IsNullOrEmpty(e) == false)
            {
                long bt = Utility.MakeDate(b, true).Ticks;
                long et = Utility.MakeDate(e, false).Ticks;
                sz.Append("seqnbr>@begdat AND seqnbr<@enddat");
                GlobalVar.DBHelper.AddCustomParam("@begdat", bt.ToString());
                GlobalVar.DBHelper.AddCustomParam("@enddat", et.ToString());
            }
            if (String.IsNullOrEmpty(punit) == false)
            {
                if (sz.Length > 0)
                    sz.Append(" AND ");

                sz.Append("purunit LIKE @punit");
                GlobalVar.DBHelper.AddCustomParam("@punit", "%" + punit + "%");
            }
           
            if (String.IsNullOrEmpty(opr) == false)
            {
                if (sz.Length > 0)
                    sz.Append(" AND ");

                sz.Append("operator = @operator");
                GlobalVar.DBHelper.AddCustomParam("@operator", opr);
            }
           
            if (mo)//只查询主单
            {
                if (sz.Length > 0)
                    sz.Append(" AND ");
                sz.Append("pid=0");
            }

            if (sz.Length == 0)
                sz.Append("1=1");
            sz.Append(" ORDER BY seqnbr DESC");
            return sz.ToString();
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("querybills");

        }

        #endregion
    }

    public class ModifyBillInfoAction : IAction
    {
        #region IAction 成员
        public void DoAction(StrDictionary sd)
        {
            string bid = sd["billid"];
            ModifyBillInfoFrm modifyFrm = new ModifyBillInfoFrm(bid);
            DialogResult result =  modifyFrm.ShowDialog();
            if (result == DialogResult.OK)
            {
                GlobalVar.Container.InvokeScript("query",null);
            }
        }
        public bool IsMe(string schema)
        {
            return schema.Equals("modifybill");

        }
        #endregion

    }
    public class UpdateUserInfoAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {

            string name = sd["name"];
            string psw = sd["psw"];
            string oripsw = sd["oripsw"];

            if (oripsw.Equals(GlobalVar.LogInfo.Password) == false)
            {
                GlobalVar.Container.InvokeScript("cb", new object[] { "密码不正确！" });
                return;
            }
            if (name != GlobalVar.LogInfo.Name)
            {
                GlobalVar.DBHelper.AddCustomParam("@name", name);

                object n = GlobalVar.DBHelper.ExcuteForUnique("select count(name) from members where name=@name");
                if (Convert.ToInt32(n) == 1)
                {
                    GlobalVar.Container.InvokeScript("cb", new object[] { "用户名已存在！" });
                    return;
                }
            }

            DataTable tbl = new DataTable();
            tbl.Init("members", "name,password,code");
            DataRow r = tbl.NewRow();
            r.SetField<string>("name", name);
            r.SetField<string>("password", psw);
            r.SetField<string>("code", GlobalVar.LogInfo.WorkCode);
            tbl.Rows.Add(r);
            r.AcceptChanges();
            r.SetModified();
            GlobalVar.DBHelper.AddUpdate("members", "name,password", "code");
            if (GlobalVar.DBHelper.Update(tbl) != 1)
                GlobalVar.Container.InvokeScript("cb", new object[] { "用户信息无更新" });
            else
            {
                GlobalVar.LogInfo.Name = name;
                GlobalVar.LogInfo.Password = psw;
                GlobalVar.MainFrame.Visible = false;
                GlobalVar.MainFrame.Visible = true;

                GlobalVar.Container.InvokeScript("cb", new object[] { "更新成功" });
            }
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("updateuserinfo");

        }

        #endregion
    }

    public class PayBackAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {

            PaybackFrm frm = new PaybackFrm(sd["id"]);
            frm.ShowDialog();
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("payback");

        }

        #endregion
    }
    public class BillBackupAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            string p = GlobalVar.AppPath + "temp/tmp.tmp";

            if (sd["type"] == "save")
            {
                File.WriteAllText(p, sd["data"], Encoding.UTF8);
                MessageBox.Show("保存好了");
            }
            else if (File.Exists(p))
            {
                string s = File.ReadAllText(p, Encoding.UTF8);
                GlobalVar.Container.InvokeScript("restoreList", new object[] { s });

            }

        }

        public bool IsMe(string schema)
        {
            return schema.Equals("billbackup");

        }

        #endregion
    }

    public class QueryAllStoreAction : IAction
    {
        #region IAction 成员
        public void DoAction(StrDictionary sd)
        {
            StringBuilder sb = new StringBuilder();
            string cus = sd["customer"].Trim();
            string mark = sd["mark"].Trim();

            bool hasCon = false;
            string sql = "SELECT p.id as pid,p.name,p.colornum,p.innerid,p.productid,s.id,s.mark,s.seqnbr,s.type,s.num,s.finalstore,s.opr,s.customer " +
               "FROM products as p,storenumhistory as s WHERE p.id=s.productid";
            if (String.IsNullOrEmpty(cus) == false)
            {
                hasCon = true;
                sb.Append(" AND s.customer LIKE @customer");
                GlobalVar.DBHelper.AddCustomParam("@customer", "%" + cus + "%");
            }
            if (String.IsNullOrEmpty(mark) == false)
            {
                hasCon = true;
                sb.Append(" AND s.mark LIKE @mark");
                GlobalVar.DBHelper.AddCustomParam("@mark", "%" + mark + "%");
            }
            if (hasCon == false)
            {
                MessageBox.Show("请输入客户名称或备注");
                return;
            }
            string lmt = sb.ToString();
            lmt += @" ORDER BY s.seqnbr DESC";           
            DataTable tbl = GlobalVar.DBHelper.MultiTableSelect(sql + lmt, true);

            GlobalVar.transferCodeToName(tbl, "opr");
            EasyUITable eut = new EasyUITable();
            GlobalVar.AddDateFildFromSeqnbr(tbl, true);
            eut.Table = tbl;
            GlobalVar.Container.InvokeScript(sd["invoke"], new object[] { eut.ToJson() });
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("queryallstore");

        }

        #endregion
    }
    

    public class UpdateStoreAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            GlobalVar.DBHelper.AddUpdate("storenumhistory", "customer,mark", "id");
            DataTable tbl = new DataTable();
            tbl.Init("storenumhistory","customer,mark,id");
            DataRow r = tbl.NewRow();
            r.SetField<string>("id", sd["id"]);
            r.SetField<string>("customer", sd["customer"]);
            r.SetField<string>("mark", sd["mark"]);
            tbl.Rows.Add(r);
            r.AcceptChanges();
            r.SetModified();
            int i  = GlobalVar.DBHelper.Update(tbl);
            
            GlobalVar.Container.InvokeScript(sd["invoke"], new object[] { i == 1 });
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("updateStore");

        }

        #endregion
    }
    public class TestAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {

            string num = "hello1";
            int i = 10;
            bool isInt = Int32.TryParse(num, out i);
            
            MessageBox.Show(i.ToString());
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("test");
        }

        #endregion
    }
}
