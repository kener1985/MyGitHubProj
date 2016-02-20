using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BaseLib;

namespace DefaultAction
{
    class BillPrinter
    {
        /*
         sd : puruint,购货单位; pagecode,页码; mobile,联系电话;totalamt,总计 
         */
        public static void Print(DataTable tbl, StrDictionary sd)
        {
            StringBuilder sz = new StringBuilder();
            string date = sd["date"];
            string opr = sd["opr"];
            if (String.IsNullOrEmpty(date))
                date = DateTime.Now.ToString("yyyy-MM-dd");
            if (String.IsNullOrEmpty(opr))
                opr = GlobalVar.LogInfo.WorkCode;

            sz.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">")
            .AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />")
            .AppendLine("<title>账单</title>")
            .AppendLine("<style type=\"text/css\"> body{font-family:\"Times New Roman\",Georgia,Serif;margin:2px;padding:0px;} .myTable,.myTable td,th {margin:0;font-size:16px;border:1px solid #000;border-collapse:collapse;height:22px;}</style>")
            .Append("<script type=\"text/javascript\">function printpage(){window.print();window.opener=null;window.open('','_self');window.close();}</script>")
            .AppendLine("</head><body onload=\"printpage()\">")
            .AppendLine("<div style=\"width:700px;margin:0;padding:0;\"><center><b style=\"font-size:28px\">蓝城区金煌建材  商品结算单</b><br/>")
            .AppendLine("<span style=\"font-size:14px\">地址：蓝城区霖磐镇洋稠岗　电话：0663-3590487　微信：JHJC_3590487</span><br/>")
            .AppendLine("<span style=\"font-size:14px\">主营：抛釉砖、抛光砖、仿古砖、仿木地板砖、立体彩雕背景砖、洁具等</span><br/>")
            .AppendLine("<div style=\"font-size:16px;text-align:left;margin:2px 0 2px 2px;width:680px;border:0px red solid;\">")
            .AppendLine("<div style=\"border:0px red solid;width:480px;float:left;\">客户名称:")
            .Append(sd["purunit"]).Append("</div>日期:").Append(date)
            .Append("</div></center><TABLE style=\"text-align:center;table-layout:fixed;word-break:break-all;word-wrap:true\" class=\"myTable\"><tbody>")
            .Append("<td width=\"120\">品名</td><td width=\"80\">规格</td><td width=\"80\">色号</td><td width=\"120\">数量</td>")
            .Append("<td  width=\"80\">单价(元)</td><td width=\"100\">合计(元)</td><td width=\"100\">备注</td>");
            foreach (DataRow r in tbl.Rows)
            {
                sz.Append("<tr>");
                sz.Append("<td>").Append(r.Field<string>("prodnbr")).Append("</td>")
                  .Append("<td>").Append(r.Field<string>("size")).Append("</td>")
                  .Append("<td>").Append(r.Field<string>("sh")).Append("</td>")
                  .Append("<td>").Append(r.Field<string>("c_num")).Append("</td>")
                  .Append("<td>").Append(r.Field<string>("saleprice")).Append("</td>")
                  .Append("<td>").Append(r.Field<string>("total")).Append("</td>")
                  .Append("<td>").Append(r.Field<string>("mark")).Append("</td>");
                  
                sz.Append("</tr>");
            }
            for (int i = 0; i < (12 - tbl.Rows.Count); ++i)
            {
                sz.Append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            }
            sz.Append("<tr style=\"text-align:left\"><td colspan=7>")
              .Append("总计:").Append(sd["totalamt"]).Append("</td></tr>")
              .Append("<tr style=\"text-align:left\"><td colspan=4>")
              .Append("实收款:</td><td colspan=3>结欠款:</td></tr>")
              .Append("<tr style=\"text-align:left\"><td colspan=7><div style=\"width:200px;float:left;\">制单人：").Append(opr)
              .Append("</div><div style=\"width:200px;float:left;\">提货人:</div>")
              .Append("联系电话:").Append(sd["mobile"]).Append("</td></tr>")
              .Append("<tr style=\"text-align:left\"><td colspan=7><div style=\"float:left;\">账号：农业银行揭东县白塔营业部：6228 4813 9935 5180 071　张尔勤</td></tr>")
              .Append("</tbody></table></div>")
              .Append("<span style=\"font-size:14px;\">温馨提示:尊敬的客户,收到货时请当场验收,如有问题请立即与本部联系,一经签收恕不负责。</span>");
            sz.AppendLine("</body></html>");
            string pre = DateTime.Now.ToString("yyyyMMddHHmmss");
            string path = GlobalVar.AppPath + "temp\\";
            path += pre + ".html";
            System.IO.File.WriteAllText(path, sz.ToString());
            //System.Diagnostics.ProcessStartInfo info;
            //info = new System.Diagnostics.ProcessStartInfo();
            //info.FileName = path;
           // System.Diagnostics.Process.Start(info);
            System.Diagnostics.Process.Start("iexplore.exe", path);

        }


    }
}
