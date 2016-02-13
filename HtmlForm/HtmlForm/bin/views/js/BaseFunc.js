/************************************************************************/
/*                        基础函数                                 */
/************************************************************************/
/*
 *	获取obj下所有的Key-Value对，以下情况会有效：
 *  1：元素有key,value属性
 *  2：元素为文本，且父节点有key属性
 *  3: 如果元素声明ignore，则会被忽略
 *  4: 如果vtype有DataType声明的值，则会进行验证
 *  [in] obj可以是字符串，也可以是节点对象
 */
function GetKVPair(obj,map)
{
    if(typeof(obj) == "string")
        obj = document.getElementById(obj);
    if(obj == null)
        return false;
    var ignore = obj.getAttribute("ignore");
    if(ignore != null)
        return true;
    var childs = obj.childNodes;
    var key = obj.getAttribute("key");
    var value = obj.getAttribute("value");
    var vt = obj.getAttribute("vtype");
    var hasError = false;
    if(key != null && value != null)
        {
            if(vt != null && IsValidType(value,vt) == false)
            {
                var t = new Tip(obj,"类型不匹配！")
                t.tip();
                return false;
            }
            map.add(key,value);
        }
    if(obj.hasChildNodes() == true)
    {
        for(var i=0;i<childs.length;i++)
        {
            var n = childs[i];
            switch(n.nodeType)
            {
                case 1:
                {
                    //Element
                    if(GetKVPair(n,map) == false)
                    {
                        hasError = true;
                        continue;
                    }
                    break;
                }
                case 3: //Text
                {
                    
                    if(key != null && n.nodeValue.trim() != "")
                    {
                        if(vt != null && IsValidType(n.nodeValue,vt) == false)
                        {
                            var t = new Tip(obj,"类型不匹配！")
                            t.tip();
                            return false;
                        }
                        map.add(key,n.nodeValue);
                    }
                    break;
                }
                default :
                    break;
            }
        }
    }
    return !hasError;
}

/*
 *	显示对象内部方法及属性
 */
function ShowProperty(obj,cnt,fltstr)
{
    var s = "";
    var i = 0;
    for(var p in obj)
    {
        if(i == cnt)
        {
            alert(s);
            i = 0;
            s = "";
        }else
        {
            if(fltstr == null || p.indexOf(fltstr) != -1)
            {
                s += p;
                s += "\r\n";
                i++;
            }
        }
    }
     if(i!=0)
        alert(s);
}

/*
 *	根据Table 的id获取表格数据，
 *  note: 该Table 必须设置fields属性描述各列字段的名称
 *  [return] 返回包含Table数据的MapVector数据结构
 */
function GetTableText(tid)
{
    var tbl = document.getElementById(tid);
    var fields = tbl.getAttribute("fields").split(',');

    var trs = tbl.getElementsByTagName("TR");
    var mv = new MapVector();
    for(var i=0;i<trs.length;i++)
    {
        var row = trs[i];
        var sel = row.getAttribute("selected");
        var m = new Map();
        if(sel ==null || sel == false)
            continue;
        var fldcnt = Math.min(fields.length,row.cells.length);
            for(var j=0;j<fldcnt;j++)
            {
                var td = row.cells[j];
                var v = td.innerText;
                m.add(fields[j],v);
            }  
            mv.add(m);
    }
    return mv;
}
/*
 *	列表行选中，根据selected属性判断，true为选中，否则未选中
 *  selcls和unselecls为选中和未选中的样式，默认为selected和unselected
 */
function RowSelect(row,selcls,unselcls) 
{ 
	switch(arguments.length)
	{
	    case 1:
	    {
	        selcls = "selected";
	        unselcls = "unselected";
	        break;
	    }
	    case 2:
	    {
	        unselcls = "unselected";
	    }
	}
    if(null == row.getAttribute("selected"))
    {
        row.setAttribute("selected",false);
    }
    var sel = row.getAttribute("selected");
    if(sel == true)
    {
        sel = false;
    }else
    {
        sel = true;
    }
    //alert(typeof(sel));
    var color = sel ? selcls : unselcls;
    row.className = color;
    
    row.setAttribute("selected",sel);
}
/*
 *	数据类型验证
 */

var DataType = 
{
    INT : "int",
    FLOAT: "float"
    
}
function IsValidType(value,type)
{
    
    var reg = new RegExp();
    var flag = "g";//全局
    switch(type) 
    {
    case DataType.INT: 
        reg.compile("^\\d+$",flag);
    	break;
    case DataType.FLOAT:
        reg.compile("^\\d+\\.?\\d+$",flag);
    	break;
    default:
        return true;
    }
    return reg.test(value);
}

  function LoadCssFile(file)
  {
	  var tags = document.getElementsByTagName("HEAD");
	  if(tags.length == 0)
		  return;
	var head = tags.item(0);
	var style = document.createElement("link");
	style.href = file;
	style.rel = "stylesheet";
	style.type ="text/css";
	head.appendChild(style); 
}

function PostData(data,query)
{
    var action = "?" + query;
    var form = document.createElement("FORM");
    var dataEle = document.createElement("INPUT");
    form.setAttribute("method","post");
    form.setAttribute("action",action);
    //var i1 = document.createElement("INPUT");
    //var s = "<input type='hidden' name='data' value=\"" + data + "\">";
	dataEle.setAttribute("type","hidden");
	dataEle.setAttribute('name','data');
	dataEle.setAttribute('value',data);

	form.appendChild(dataEle);
    //s += "<input type='hidden' name='type' value='test'>";
   // form.innerHTML = s;
    document.body.appendChild(form);
    form.submit();
    document.body.removeChild(form);
}
function SentForm(form) 
{
    var method = form.method.toLowerCase();
    if(method == "get")
        form.action = "receipt:///get"
    else 
        form.action = "";
    form.submit();
}
function SentAction(action,cmd,query)
{
    var str = "receipt://" + action + "/" + cmd + "?"+ query;
    window.location = str;
}

 function Str2JObj(str)
 {
	return eval('('+str+')');
 }

 function ToEasyUIJson(fields,rows)
 {
	var fs = fields.split(',');
	var json = '{"rows":[';
		for(var i =0;i<rows.length;++i)
			{
				if(i != 0)
					json += ',';
				json += '{';
				for(var j =0; j<fs.length;++j)
				{
					if(j != 0)
						json += ',';
					var v =  rows[i][fs[j]] ;
					if(typeof v === 'string')
					{
						v = v.replace(/\\/g,"\\\\");
						v= v.replace(/\'/g,"\\'");
						v = v.replace(/\"/g,"\\\"");
					}
					if(typeof v === 'undefined')
						v = '';
					json += '"' + fs[j] + '":';
					json += '"' + v + '"';
					
				}
				json += '}';
			}
			json += ']}';

			return json;
 }

 String.prototype.trim = function() {
return this.replace(/(^\s*)|(\s*$)/g, "");
}

function pagerFilter(data){
			if (typeof data.length == 'number' && typeof data.splice == 'function'){	// is array
				data = {
					total: data.length,
					rows: data
				}
			}
			var dg = $(this);
			var opts = dg.datagrid('options');
			var pager = dg.datagrid('getPager');
			pager.pagination({
				onSelectPage:function(pageNum, pageSize){
					opts.pageNumber = pageNum;
					opts.pageSize = pageSize;
					//alert(pageSize);
					pager.pagination('refresh',{
						pageNumber:pageNum,
						pageSize:pageSize
					});
					dg.datagrid('loadData',data);
				}
			});
			if (!data.originalRows){
				data.originalRows = (data.rows);
			}
			var start = (opts.pageNumber-1)*parseInt(opts.pageSize);
			var end = start + parseInt(opts.pageSize);
			data.rows = (data.originalRows.slice(start, end));
			return data;
		}

function ToUpperCase(num)
{
	var strOutput = "",
	strUnit = '仟佰拾亿仟佰拾万仟佰拾元角分';
	num += "00";
	var intPos = num.indexOf('.');
	if (intPos >= 0){
		num = num.substring(0, intPos) + num.substr(intPos + 1, 2);
	}
	strUnit = strUnit.substr(strUnit.length - num.length);
	for (var i=0; i < num.length; i++){
	strOutput += '零壹贰叁肆伍陆柒捌玖'.substr(num.substr(i,1),1) + strUnit.substr(i,1);
	}
	return strOutput.replace(/零角零分$/, '整').replace(/零[仟佰拾]/g, '零').replace(/零{2,}/g, '零').replace(/零([亿|万])/g, '$1').replace(/零+元/, '元').replace(/亿零{0,3}万/, '亿').replace(/^元/, "零元");
}
//window.onerror = function (){return true;}

