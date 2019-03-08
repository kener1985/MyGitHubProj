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

            GlobalVar.DBHelper.AddSelectWithLimit("products", sd["fields"], "productid=@productid AND status!='1'");

            GlobalVar.DBHelper.AddCustomParam("@productid", sd["data"]);

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

            GlobalVar.DBHelper.BeginBatch();
            string oriId = null;//原id
            DateTime dtNow = DateTime.Now;
            long billseq = dtNow.Ticks;
            //修改订单,先将之前的信息冲掉
            if (sd["oprtype"].Equals("modify"))
            {
                //保存原id
                GlobalVar.DBHelper.AddCustomParam("@seqnbr", sd["seqnbr"]);
                oriId = Convert.ToString(GlobalVar.DBHelper.ExcuteForUnique("select id from bills where seqnbr=@seqnbr", true));
                billseq = Convert.ToInt64(sd["seqnbr"]);
                if (!restoreBalance(sd["seqnbr"]))
                {
                    MessageBox.Show("修改订单失败");
                    GlobalVar.DBHelper.EndBatch(true);//异常强制回滚
                    return;
                }
            }else if(sd["oprtype"].Equals("delete"))
            {
                //删除账单，并回滚库存及相关记录

                bool success = restoreBalance(sd["seqnbr"]);
                GlobalVar.DBHelper.EndBatch(!success);
                
                GlobalVar.Container.InvokeScript(sd["invoke"],new object[]{success});
                return;
            }

            string oprcode = sd["operator"];
            GlobalVar.DBHelper.AddInsert("billitem", "productid,num,type,saleoff,mark,seqnbr,saleprice,c_num");
            GlobalVar.DBHelper.AddInsert("bills", "id,seqnbr,payer,purunit,mobile,pid,operator,pagecode,type");


            //billitem表
            EasyUITable etb = new EasyUITable();
            etb.Parse(sd["data"]);
            
            //if (sd.ContainsKey("seqnbr") && sd["seqnbr"] != null && !sd["seqnbr"].Equals(String.Empty))
            //{
            //    billseq = Convert.ToInt64(sd["seqnbr"]);
            //}

            etb.Table.Columns.Add("seqnbr");
            foreach (DataRow r in etb.Table.Rows)
            {
                r.SetField<long>("seqnbr", billseq);
                r.AcceptChanges();
                r.SetAdded();
            }

            etb.Table.TableName = "billitem";

            //bills 表
            DataTable tbBill = new DataTable();
            tbBill.Init("bills", "id,seqnbr,operator,mobile,payer,pid,purunit,pagecode,type");
            DataRow row = tbBill.NewRow();
            if (oriId != null)
                row.SetField<long>("id", Convert.ToInt64(oriId));
            row.SetField<long>("seqnbr", billseq);
            row.SetField<string>("operator", oprcode);
            row.SetField<string>("mobile", sd["mobile"]);
            row.SetField<string>("payer", sd["payer"]);
            row.SetField<string>("purunit", sd["purunit"]);
            row.SetField<string>("pid", sd["pid"] == "" ? "0" : sd["pid"]);
            row.SetField<string>("pagecode", sd["pagecode"]);
            row.SetField<string>("type", sd["type"]);
            tbBill.Rows.Add(row);

            //products 表
            string idset = GetIdSet(etb.Table, "productid");
            DataTable tbProd = new DataTable("products");
            GlobalVar.DBHelper.AddSelectWithLimit("products", "id,storenum", "id IN " + idset);
            GlobalVar.DBHelper.AddUpdate("products", "id,storenum", "id");
            GlobalVar.DBHelper.Fill(ref tbProd);

            //storehistory表
            GlobalVar.DBHelper.AddInsert("storenumhistory", "mark,num,billseqnbr,seqnbr,type,productid,finalstore,opr,customer");
            DataTable tblHis = new DataTable();
            tblHis.Init("storenumhistory", "mark,num,billseqnbr,seqnbr,type,productid,finalstore,opr,customer");

            //账务明细表
            GlobalVar.DBHelper.AddInsert("debtdetail", "main_seqnbr,billseq,opr,mark,seqnbr,amount,type");
            DataTable tblDebt = new DataTable();
            tblDebt.Init("debtdetail", "main_seqnbr,billseq,seqnbr,opr,mark,date,amount,type");
            DataRow debtrow = tblDebt.NewRow();
            debtrow.SetField<string>("opr", oprcode);
            debtrow.SetField<long>("seqnbr", dtNow.Ticks);
            debtrow.SetField<string>("amount", sd["amount"]);
            debtrow.SetField<string>("type", sd["type"]);
            debtrow.SetField<string>("main_seqnbr", billseq.ToString());
            debtrow.SetField<string>("billseq", billseq.ToString());
            
            if (sd["pid"] != "0" && sd["pid"] != "" && sd["pid"] != null)
            {
                string mainSeqnbr = GlobalVar.DBHelper.ExcuteForUnique<string>("select seqnbr from bills where id=" + sd["pid"]);
                debtrow.SetField<string>("main_seqnbr", mainSeqnbr);
            }
            
            
            tblDebt.Rows.Add(debtrow);
            //平衡货品数量
            Balance(etb.Table, tbProd, tblHis, sd, billseq);
            DataSet ds = new DataSet();
            ds.Tables.AddRange(new DataTable[] { etb.Table, tbProd, tbBill, tblHis, tblDebt });
            

            //新增账单数据 更新货品数据
            int ret = GlobalVar.DBHelper.Update(ds, "billitem,products,bills,storenumhistory,debtdetail");
            //int ret = GlobalVar.DBHelper.Update(ds, "billitem,bills,debtdetail");
            if (ret == -1)
            {
                GlobalVar.DBHelper.EndBatch(true);
                GlobalVar.Container.InvokeScript("resultCallback", new object[] { false });
                return;
            }
            //若更新主单，需同时更新关联的子单
            /*if (oriPid != null)
            {
                try
                {
                    GlobalVar.DBHelper.AddCustomParam("@seqnbr", billseq);
                    string newPid = Convert.ToString(GlobalVar.DBHelper.ExcuteForUnique("select id from bills where seqnbr=@seqnbr", true));
                    GlobalVar.DBHelper.AddCustomParam("@newPid", newPid);
                    GlobalVar.DBHelper.AddCustomParam("@oldPid", oriPid);
                    GlobalVar.DBHelper.ExcuteNonQuery("update bills set pid=@newPid where pid=@oldPid");

                    GlobalVar.DBHelper.AddCustomParam("@newSeqnbr", dtNow.Ticks);
                    GlobalVar.DBHelper.AddCustomParam("@oldSeqnbr", sd["seqnbr"]);
                    GlobalVar.DBHelper.ExcuteNonQuery("update debtdetail set main_seqnbr=@newSeqnbr where main_seqnbr=@oldSeqnbr");
                }
                catch (Exception e)
                {
                    GlobalVar.DBHelper.EndBatch(true);
                    GlobalVar.Container.InvokeScript("resultCallback", new object[] { false });
                    return;
                }
            }*/
            bool noErr = Utility.ProcessSqlError(ds);
            GlobalVar.DBHelper.EndBatch(noErr == false);
            //账单录入版暂不进行打印
            //if (noErr)
            //    BillPrinter.Print(etb.Table, sd);
            GlobalVar.Container.InvokeScript("resultCallback", new object[] { noErr });
        }
        private void Balance(DataTable tbBill, DataTable tbProd, DataTable tblHis, StrDictionary sd, long billseq)
        {
            DateTime dtNow = DateTime.Now;
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
                nr.SetField<long>("billseqnbr", billseq);
                nr.SetField<string>("seqnbr",dtNow.Ticks.ToString());
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

        /**
         *  billseq原订单流水
         **/
        private bool restoreBalance(string billseq)
        {
            try
            {
                GlobalVar.DBHelper.AddCustomParam("@seqnbr", billseq);
                //加载原账单项
                GlobalVar.DBHelper.AddSelectWithLimit("billitem", "productid,type,num", "seqnbr=@seqnbr");
                DataTable items = new DataTable("billitem");
                GlobalVar.DBHelper.Fill(ref items);

                //还原产品数量
                foreach(DataRow r in items.Rows)
                {
                    long productid = r.Field<long>("productid");
                    string type = r.Field<string>("type");
                    int num = r.Field<Int32>("num");
                    string opr = type.Equals("O") ? "+" : "-";//原先为出货，恢复后数量增加
                    GlobalVar.DBHelper.ExcuteNonQuery("update products set storenum=storenum" + opr + num + " where id=" + productid);
                }
                
                GlobalVar.DBHelper.AddCustomParam("@seqnbr",billseq);
                //删除原订单
                GlobalVar.DBHelper.ExcuteForUnique("delete from bills where seqnbr=@seqnbr",false);
                //删除原订单项
                GlobalVar.DBHelper.ExcuteForUnique("delete from billitem where seqnbr=@seqnbr", false);
                //删除原账务信息
                GlobalVar.DBHelper.ExcuteForUnique("delete from debtdetail where billseq=@seqnbr", false);
                //删除原出货历史
                GlobalVar.DBHelper.ExcuteForUnique("delete from storenumhistory where billseqnbr=@seqnbr", true);
            }
            catch (Exception e)
            {
                GlobalVar.Log.LogError("还原订单信息异常:"+e.Message);
                return false;
            }
            return true;
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
            string mainSeqnbr = sd["seqnbr"];
            string ivk = String.Empty;
            if (mainSeqnbr[0] == '#')//查账务统计表
            {
                mainSeqnbr = mainSeqnbr.Substring(1);
                GlobalVar.DBHelper.AddSelectWithLimit("debtdetail", "amount,seqnbr,mark,opr,type", "main_seqnbr=@seq");
                GlobalVar.DBHelper.AddCustomParam("@seq", mainSeqnbr);
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
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string phis = String.Empty;
            int pagecode = 1;

            //查询该订单操作人工号
            GlobalVar.DBHelper.AddSelect("members","name,code");
            GlobalVar.DBHelper.AddSelect("bills","operator,pagecode","id");
            DataSet ds = new DataSet();
            ds.Tables.Add(new DataTable("members"));
            ds.Tables.Add(new DataTable("bills"));
            GlobalVar.DBHelper.AddCustomParam("@id",sd["id"]);
            GlobalVar.DBHelper.Fill(ds);

            EasyUITable etblMembers = new EasyUITable();
            etblMembers.Table = ds.Tables["members"];
            string code = null;
            if(ds.Tables["bills"].Rows.Count == 1)
            {
                DataRow row = ds.Tables["bills"].Rows[0];
                code = row.Field<string>("operator");
                pagecode = row.Field<int>("pagecode");
            }
            
            //查询所有员工工号
            if (sd.ContainsKey("id") == false || sd["id"].Equals(String.Empty) || sd["id"].Equals("0"))
            {
                pagecode = 1;
            }else
            {
                //初始化页码
                //主单id
                string mainId = sd["id"];
                object result = GlobalVar.DBHelper.ExcuteForUnique("select pid from bills where id=" + sd["id"]);
                long pid = 0;
                if (result != null)
                    pid = Convert.ToInt64(result);

                if (pid != -1 && pid != 0)
                    mainId = pid.ToString();

                if (!sd.ContainsKey("type") || sd["type"].Equals("append"))
                {
                    DbDataReader r = GlobalVar.DBHelper.ExcuteSQL("select max(pagecode) from bills where pid=" + mainId);
                    r.Read();
                    if (r.IsDBNull(0))
                        pagecode = 2;
                    else
                        pagecode = r.GetInt32(0) + 1;
                    r.Close();
                }
                //初始化历史价格
                string lmt = "id=" + mainId + " OR pid=" + mainId;
                DataTable tblBill = new DataTable("bills");
                GlobalVar.DBHelper.AddSelectWithLimit("bills", "seqnbr,operator", lmt);
                GlobalVar.DBHelper.Fill(ref tblBill);
                string seqset = GetSeqnbrSet(tblBill);
                StringBuilder sz = new StringBuilder();
                sz.Append("SELECT p.productid,p.innerid,p.colornum,b.saleprice,b.seqnbr")
                    .Append(" FROM billitem as b,products as p where p.id=b.productid AND b.seqnbr IN ")
                    .Append(seqset)
                    .Append(" order by b.seqnbr desc");
                    
                EasyUITable etb = new EasyUITable();
                etb.Table = GlobalVar.DBHelper.MultiTableSelect(sz.ToString(), false);
                GlobalVar.AddDateFildFromSeqnbr(etb.Table, false);
                phis = etb.ToJson();
            }

            GlobalVar.Container.InvokeScript(sd["invoke"], new object[] {etblMembers.ToJson(),code, date, pagecode, phis });

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
    public class QueryProductIdAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            EasyUITable etb = new EasyUITable();
            DataTable tbl = new DataTable("products");
            string keyword = sd["data"].Trim();
            GlobalVar.DBHelper.AddSelectWithLimit("products", "productid", "productid LIKE @keyword AND status!='1' GROUP BY productid");
            GlobalVar.DBHelper.AddCustomParam("@keyword", "%" + keyword + "%");
            GlobalVar.DBHelper.Fill(ref tbl);
            etb.Table = tbl;

            GlobalVar.Container.InvokeScript(sd["invoke"], new object[] { etb.ToJson() });
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("queryproductid");

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

            PaybackFrm frm = new PaybackFrm(sd["mainseqnbr"]);
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
            string sql = "SELECT p.id as pid,p.name,p.colornum,p.innerid,p.productid,s.id,s.mark,s.type,s.num,s.finalstore,s.opr,s.customer,s.seqnbr " +
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
            lmt += @" ORDER BY s.id DESC";           
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

    public class QueryBillItemsAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            AbstractDbHelper helper  = GlobalVar.DBHelper;
            helper.AddCustomParam("@seqnbr",sd["seqnbr"]);
            DataTable tbl = helper.MultiTableSelect("select bi.seqnbr,bi.num,bi.c_num,bi.type,bi.saleoff,bi.saleprice,bi.mark,p.id,p.colornum,p.innerid,p.productid,p.size " +
            "from billitem bi,products p where bi.seqnbr=@seqnbr and p.id=bi.productid", true);

            EasyUITable etb = new EasyUITable();
            etb.Table = tbl;
            GlobalVar.Container.InvokeScript(sd["invoke"], new Object []{ etb.ToJson()});
        }

        public bool IsMe(string schema)
        {
            return schema.Equals("queryBillItems");
        }

        #endregion
    }

    
    public class TestAction : IAction
    {

        #region IAction 成员

        public void DoAction(StrDictionary sd)
        {
            GlobalVar.DBHelper.AddDelete("products", "id");
            GlobalVar.DBHelper.AddInsert("members", "name");

            DataSet ds = new DataSet();
            DataTable delTbl = new DataTable("products");
            DataTable addTbl = new DataTable("members");
            delTbl.Init("products", "id");
            addTbl.Init("members", "name");

            DataRow dr = delTbl.NewRow();
            dr.SetField<Int64>("id", 32);
            delTbl.Rows.Add(dr);
            delTbl.AcceptChanges();
            dr.Delete();

            DataRow dr1 = addTbl.NewRow();
            dr1.SetField<string>("name", "测试");
            addTbl.Rows.Add(dr1);

            ds.Tables.Add(delTbl);
            ds.Tables.Add(addTbl);

            GlobalVar.DBHelper.BeginBatch();
            try
            {
                GlobalVar.DBHelper.Update(ds, "members,products");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            GlobalVar.DBHelper.EndBatch();



        }

        public bool IsMe(string schema)
        {
            return schema.Equals("test");
        }

        #endregion
    }
}
