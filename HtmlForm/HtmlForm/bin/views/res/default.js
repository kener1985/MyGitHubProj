function Response(text,secid) {

	var div = document.getElementById("main" + secid);
    div.innerHTML = text;
}

function setdata(text,secid)
{

	var div = document.getElementById("inner");
    div.innerHTML = text;
}
function goto()
{
	var anc = document.getElementById("anc");
	var h = document.getElementById("host");
	anc.href = h.value;
	return false;
}
function MyCallback()
{
	//alert("1");
	SentAction("initview","","view=views/temp&invoke=Response&secid=1");
	//SentAction("initview","view=views/temp&invoke=Response&secid=1");
	//alert("2");
	SentAction("initview","","view=views/qihuo&invoke=Response&secid=2");
}
  function test(id)
  {
	  alert("what");
		alert(id);
	  return;
	SentAction("test","","");
	  return;
	var ini = new Ini();
	
	var m = new Map();
	var mv = new MapVector();
	

	
	if(GetKVPair("divid",m) == false)
	return;
	//m.add("a","b");
	//m.add("a","b");
	mv.add(m);
	mv.add(m);
	mv.add(m);
	ini.add("sec1",mv);
	ini.add("sec2",mv);
	ini.add("sec6",mv);

	alert(m.see());
	ini = null;
	}
    
function show()
{
	//var te = document.getElementById("text1");
	//ShowProperty(te,15,"Top");
	//alert("offsetLeft"+te.offsetLeft);
	//alert("scrollLeft"+te.scrollLeft);
	//alert("clientLeft"+te.offsetHeight);
	var mv = GetTableText("mtbl");
	var ini = new Ini();
	ini.add("sec",mv);
	//var s = "query:?data=" + escape(ini);
	//alert(s);
	//window.location = s;
	//alert(mv.see());

	PostData(ini.toString(),"cacheid=test");
	return ;
var te2 = document.getElementById("text2");
	
	//t = new Tip(te);
	
	var t1 = new Tip(te,"");
	var t2 = new Tip(te2,"");
	t1.tip();
	t2.tip();
}
function Show(c,s)
{
	var c = new Calendar(c,s);
	c.show();
}

function GetAction()
{
	return "DefaultAction";
}

function navigate(page)
{

	var query = "cacheid=test&page="+page+"&invoke=showlist";
//	alert(query);
	SentAction('navigate',query);
}

function showlist(list)
{
	document.getElementById("list").innerHTML = list;
}

function ReturnJson(json)
{
	var j = Str2JObj(json);
	var ini = new Ini();
	ini.fromDJson(j);
	alert(j[0].Key);
}