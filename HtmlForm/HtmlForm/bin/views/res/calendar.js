function Calendar(ctrid,optid,s)
{
    this.container = document.getElementById(ctrid);
    var tmp = optid.split('.');
    if(tmp.length < 2)
    {
        throw Error("param error, must format like obj.attribute");
    }

    this.output = document.getElementById(tmp[0]);
    //this.opacity = 0;
    this.div;

    if(arguments.length < 3)
        this.sep = '-';
    else
        this.sep = s;

	
    this.isLeap = function (The_Year)
    {
        if ((The_Year%400==0) || ((The_Year%4==0) && (The_Year%100!=0)))
        return true;
        else
        return false;
    }
    this.getWeekday = function (The_Year,The_Month)
    {
        var Allday;
        Allday = 0;
        if (The_Year>2000)
        {
         
        for (i=2000 ;i<The_Year; i++) 
        if (this.isLeap(i)) 
        Allday += 366;
        else
        Allday += 365;
        for (i=2; i<=The_Month; i++)
        {
            switch (i)
            {
                case 2 : 
                if (this.isLeap(The_Year))
                Allday += 29;
                else
                Allday += 28;
                break;
                case 3 : Allday += 31; break;
                case 4 : Allday += 30; break;
                case 5 : Allday += 31; break;
                case 6 : Allday += 30; break;
                case 7 : Allday += 31; break;
                case 8 : Allday += 31; break;
                case 9 : Allday += 30; break;
                case 10 : Allday += 31; break;
                case 11 : Allday += 30; break;
                case 12 : Allday += 31; break;
        }

        }
    }

    return (Allday+6)%7;

    }

    this.chooseDay = function (The_Year,The_Month,The_Day)
    {
    var Firstday;
    var completely_date;
    
    if (The_Day!=0)
    {
        completely_date = The_Year + this.sep + The_Month + this.sep + The_Day;
        this.output.setAttribute(tmp[1], completely_date);
    }

    //showdate 只是一个为了显示而采用的东西，
    //如果外部想引用这里的时间，可以通过使用 completely_date引用完整日期
    //也可以通过The_Year,The_Month,The_Day分别引用年，月，日
    //当进行月份和年份的选择时，认为没有选择完整的日期
    
    Firstday = this.getWeekday(The_Year,The_Month);
        this.showCalender(The_Year,The_Month,The_Day,Firstday);

    }

    this.nextMonth = function (The_Year,The_Month)
    {
    if (The_Month==12)
        this.chooseDay(The_Year+1,1,0);
    else
        this.chooseDay(The_Year,The_Month+1,0);
    }

    this.prevMonth = function (The_Year,The_Month)
    {
    if (The_Month==1)
        this.chooseDay(The_Year-1,12,0);
    else
        this.chooseDay(The_Year,The_Month-1,0);
    }

    this.prevYear = function (The_Year,The_Month)
    {
        this.chooseDay(The_Year-1,The_Month,0);
    }

    this.nextyear = function (The_Year,The_Month)
    {
        this.chooseDay(The_Year+1,The_Month,0);
    }
    this.showCalender = function (The_Year,The_Month,The_Day,Firstday)
    {
    var today = new Date();
    var showstr;
    var Month_Day;
    var ShowMonth;
    this.opacity = 0;
    switch (The_Month)
    {
        case 1 : ShowMonth = "一月"; Month_Day = 31; break;
        case 2 :
        ShowMonth = "二月";
        if (this.isLeap(The_Year))
            Month_Day = 29;
        else
            Month_Day = 28;
        break;
        case 3 : ShowMonth = "三月"; Month_Day = 31; break;
        case 4 : ShowMonth = "四月"; Month_Day = 30; break;
        case 5 : ShowMonth = "五月"; Month_Day = 31; break;
        case 6 : ShowMonth = "六月"; Month_Day = 30; break;
        case 7 : ShowMonth = "七月"; Month_Day = 31; break;
        case 8 : ShowMonth = "八月"; Month_Day = 31; break;
        case 9 : ShowMonth = "九月"; Month_Day = 30; break;
        case 10 : ShowMonth = "十月"; Month_Day = 31; break;
        case 11 : ShowMonth = "十一月"; Month_Day = 30; break;
        case 12 : ShowMonth = "十二月"; Month_Day = 31; break;

    }
//    this.div = document.createElement("div");
//    this.div.style.filter = "alpha(opacity=100)";
    showstr = "<Table cellpadding=2 cellspacing=0 border=3  "+
    "style='position:absolute;border-collapse:collapse;border-color:black;color:#EEEE00;background-color:#444444;font-size:13px;'>"; 
    showstr += "<tr>"+
    "<td style='cursor:pointer;' _type='pyear' args='"+The_Year+"," + The_Month + "''>&lt;&lt;</td><td> " + 
    The_Year + " </td>"+
    "<td  _type='nyear' args='"+The_Year+","+The_Month+"' style='cursor:pointer;'>&gt;&gt;</td>"+
    "<td  style='cursor:pointer' _type='pmonth' args='" + The_Year+","+The_Month+"'>&lt;&lt</td>"+
    "<td  align=center>" + ShowMonth + 
    "</td><td width=0 _type='nmonth' args='"+The_Year+","+The_Month+"' style='cursor:pointer'>&gt;&gt;</td></tr>";
    showstr += "<tr><td align=center width=100% colspan=6>";
    showstr += "<table cellpadding=1 cellspacing=0 border=1 "+
    "style='border-collapse:collapse;border-color:#FFFF00;color:#99BBFF;background-color:#444444;' width=100%>";
    showstr += "<Tr align=center >";
    showstr += "<td><b>日</b></td>";
    showstr += "<td><b>一</b></td>";
    showstr += "<td><b>二</b></td>";
    showstr += "<td><b>三</b></td>";
    showstr += "<td><b>四</b></td>";
    showstr += "<td><b>五</b></td>";
    showstr += "<td><b>六</b></td>";
    showstr += "</Tr><tr>";
    
    for (i=1; i<=Firstday; i++)
    showstr += "<Td align=center > </Td>";

    for (i=1; i<=Month_Day; i++)
    {
    var bgColor = "#444444";
    if ((The_Year==today.getYear()) && (The_Month==today.getMonth()+1) && (i==today.getDate()))
    bgColor = "#FFFF00";
   // else
    //bgColor = "#CCCCCC";

    if (The_Day==i) bgColor = "#FFFFCC";
    showstr += "<td align=center bgcolor=" + bgColor + " style='color:#9F88FF;cursor:pointer;' _type='sday' args='" + The_Year + "," + The_Month + "," + i + "'>" + i + "</td>";
    Firstday = (Firstday + 1)%7;
    if ((Firstday==0) && (i!=Month_Day)) showstr += "</tr><tr>";
    }
    if (Firstday!=0) 
    {
    for (i=Firstday; i<7; i++) 
    showstr += "<td align=center bgcolor=#444444> </td>";
    showstr += "</tr>";
    }

    showstr += "</tr>";
    
    showstr +="</tr></table></td></tr><tr>" ; 
    showstr += "<td colspan=2 align='center'><b style='cursor:pointer;' _type='today'>当天</b></td>"
	showstr += "<td colspan=5 align='right'><b style='cursor:pointer;' _type='close' title='关闭'>×</b></td>";
    showstr += "</tr></table>";
    //this.div.innerHTML = showstr;
    this.container.innerHTML = showstr;
    //var t = new Interval(this.ItvCb,this);
    //t.start(50);
    this.setClickEvent(this.container);
    
    }  
    this.ItvCb = function (t,me) 
    {
        if(me.opacity >= 100)
        {
            t.stop();
            me.container.removeNode(me.div);
        }else
        {
            me.opacity += 5;
        }
    }
    this.setClickEvent = function (ele) 
    {
        if(ele.nodeType != 1)
            return;
        var type = ele.getAttribute("_type");
        var ar = ele.getAttribute("args");
         var args ;
        if(ar != null)
            args = ar.split(',');
        //如果有类型定义，注册方法
        if(type != null)
        {
            var cb = new this.Callback(this,type,args);
            ele.onclick = cb.callback;    
        }
        if(ele.hasChildNodes() == true)
        {
            for(var i=0;i<ele.childNodes.length;i++)
            {
                var cn = ele.childNodes[i];
                this.setClickEvent(cn);
            }
        }
    }
    
    this.show = function () 
    {
        var The_Year,The_Month,The_Day,Firstday;
        var today = new Date();
	    The_Year = today.getYear();
	    The_Month = today.getMonth() + 1;
	    The_Day = today.getDate();
	        
        var opt = this.output.getAttribute(tmp[1])
        if(opt != null && opt != "")
        {
            var a = opt.split(this.sep);
            if(a.length >= 3)
            {
                The_Year = parseInt(a[0]);
	            The_Month = parseInt(a[1]);
	            The_Day = parseInt(a[2]);
            }
        }
        Firstday = this.getWeekday(The_Year,The_Month);
        
	    
	    this.showCalender(The_Year,The_Month,The_Day,Firstday);
    }
    this.showToday = function () 
    {
         var The_Year,The_Month,The_Day,Firstday;
        var today = new Date();
	    The_Year = today.getYear();
	    The_Month = today.getMonth() + 1;
	    The_Day = today.getDate();    
	    Firstday = this.getWeekday(The_Year,The_Month);
        
	    
	    this.showCalender(The_Year,The_Month,The_Day,Firstday);
    }
    /*
    *	onclick 回调
    */
    this.Callback = function (obj,type,params)
    {
        this.callback = function()
        {
            var p1,p2,p3;
            if(params != null)
            {
                p1 = parseInt(params[0]);
                p2 = parseInt(params[1]);
                p3 = parseInt(params[2]);
            }
           
            switch(type) {
            case "pyear":
                obj.prevYear(p1,p2);
        	    break;
            case "nyear":
                obj.nextyear(p1,p2);
        	    break;
            case "pmonth":
                obj.prevMonth(p1,p2);
        	    break;
            case "nmonth":
                obj.nextMonth(p1,p2);
        	    break;
            case "sday":
                obj.chooseDay(p1,p2,p3);
        	    break;
        	case "close":
        	    obj.container.innerHTML = "";
        	    break;
        	case "today":
        	    obj.showToday();
        	    break;
            default:
            break;
            }     
        }  
    }

}
