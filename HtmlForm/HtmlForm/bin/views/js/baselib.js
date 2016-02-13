/************************************************************************/
/*                          冒泡提示                              
/**********************************************************************/
function Tip(ctrl,msg)
{
	this.i = 100;
	this.div = document.createElement("div");
    //this.div.style.backgroundImage = "url('c:/a.jpg')";
    //this.div.style.backgroundRepeat = "repeated"
    //this.div.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='c:/a.jpg', sizingMethod='scale')";
    
	this.tip = function()
	{
	    document.body.appendChild(this.div);
	    this.div.style.position =  "absolute"  
        this.div.style.borderStyle = "dotted";
        this.div.style.borderWidth = "1px";
        this.div.style.borderColor = "red";
        this.div.style.color = "red";
	    this.div.style.filter = "alpha(opacity=100)";
	    this.div.style.left= ctrl.offsetLeft + 10;
	    this.div.style.top =ctrl.offsetTop;
	    alert(ctrl.clientTop);
        this.i = 100;
	  
		this.div.innerText = msg;
		var len = msg.length * 200 + 1000;
		var inobj = new this.inner(this);
		setTimeout(inobj.callback,len);
	}
	this.go = function()
	{
		var t = new Interval(this.callback,this);
		t.start(50);
	}
	this.inner = function (obj) 
	{
	    this.callback = function()
	    {
	        obj.go();
	    }
	}
	/*
	 *	线程函数，this为window
	 */
	this.callback = function(t,me)
	{
		if(me.i <=0)
		{
		    t.stop();
            //alert(document.body.childNodes.length);
		    document.body.removeChild(me.div);
		    //alert(document.body.childNodes.length);
		}
		me.i -= 5;
		//IE
		var flt = "alpha(opacity=" + me.i + ")";
		me.div.style.filter = flt;
		
	}
}
/************************************************************************/
/*                                                                      */
/************************************************************************/
function Interval(fn,ctner)
{
	this.timer ;
	this.start = function(itv)
	{
		var i = new this.inner(this,ctner);
		this.timer = setInterval(i.callback,itv);
	}
	this.run = function()
	{
			fn(this,ctner);
	}
	this.stop = function()
	{
		clearInterval(this.timer);
	}
	this .inner = function(t)
	{	
		this.callback = function()
		{
			t.run();
		}
	}
}
Array.prototype.remove = function(s) {   
    for (var i = 0; i < this.length; i++) {   
        if (s == this[i])   
            this.splice(i, 1);   
    }   
}   



/************************************************************************/
/* Map 操作函数                                                                  
/************************************************************************/
function MapBase()
{
    /**  
     * 放入一个键值对  
     * @param {String} key  
     * @param {Object} value  
     */  
    this.add = function(key, value) {   
        if(this.data[key] == null){   
            this.keys.push(key);   
        }   
        this.data[key] = value;   
    };   
       
    /**  
     * 获取某键对应的值  
     * @param {String} key  
     * @return {Object} value  
     */  
    this.get = function(key) {   
        return this.data[key];   
    };   
       
    /**  
     * 删除一个键值对  
     * @param {String} key  
     */  
    this.remove = function(key) {   
        this.keys.remove(key);   
        this.data[key] = null;   
    };   
       
    /**  
     * 判断Map是否为空  
     */  
    this.isEmpty = function() {   
        return this.keys.length == 0;   
    };   
    this.isExist = function(key)
	{
	    return this.data[key] != null;
	}
    /**  
     * 获取键值对数量  
     */  
    this.size = function(){   
        return this.keys.length;   
    };   
       
    /**  
     * 重写toString   适应 C# - json 格式
     */  
    this.toString = function(){   
        var s = "[";
        for(var i=0;i<this.keys.length;i++)
		{   
			if( i != 0)
				s += ",";
			var k = this.keys[i];   
			s += "{\"Key\":\"" + k + "\",\"Value\":";
			if(typeof(this.data[k]) == "object")
			{
			    s += this.data[k];
			}else
			{
			    var s1 = this.data[k].replace(/\\/g,"\\\\").replace(/\"/g,"\\\"");
			    //var v = escape(s1);
			    s += "\"" + s1 + "\""; 
			}
		
            s += "}";   
        }   
        s+="]";   
        return s;   
    };   
    
    /**  
     * 从对象中转换，该对象结构和 C# Dictionary Json对应 
     */  
	this.fromDJson = function(djson)
	 {
		for(var i=0;i<djson.length;i++)
		 {
			var pair = djson[i];
			this.add(pair.Key,pair.Value);
		}
	}
	this.see = function()
	{
	    var s ="";
	    for(var i=0;i<this.keys.length;i++)
		{   
			if( i != 0)
				s += ",";
			var k = this.keys[i];   
            s += k + "=";
            s += this.data[k];   
        }     
        return s;   
	}
}
/************************************************************************/
/* Map 数据结构                                                                    
/************************************************************************/
function Map() {   
    /** 存放键的数组(遍历用到) */  
    this.keys = new Array();   
    /** 存放数据 */  
    this.data = new Object();       
} ;
Map.prototype = new MapBase();

/************************************************************************/
/* MapVector 数据结构                                                                    
/************************************************************************/
function MapVector()
{
	this._Array = new Array();
	this.add = function(map)
	{
		this._Array.push(map);
	};
    
	this.remove = function(idx)
	{
		if(this._Array.length == 0)
			return null;
		var obj = this._Array[idx];
		this._Array.splice(idx,1);
		return obj;
	}
	this.isEmpty = function()
	{
		return this._Array.length == 0;
	}
	this.length = function()
	{
		return this._Array.length;
	};

	this.at = function(idx)
	{
		return this._Array[idx];
	};
	
    this.size = function ()
    {
        return this._Array.length;
    }
    
	this.toString=function(){
		var s = "[";
		for(var i=0;i<this._Array.length;i++)
		{
			var item =  this._Array[i];
			if((item instanceof Map) == false)
				throw Error("Map only");
			if(i != 0)
			    s += ',';
			s += item.toString();
		}
		s += "]";

		return s;
	};

	this.fromDJson = function(djson)
	{
		for(var i=0;i<djson.length;i++)
		{
			var item = djson[i];
			var map = new Map();
			map.fromDJson(item);
			this._Array.push(map);
		}
	}
	this.see = function ()
	{
	    var s = "";
		for(var i=0;i<this._Array.length;i++)
		{
			var item =  this._Array[i];
			if((item instanceof Map) == false)
				throw Error("Map only");
			s += item.see() + "\r\n";
		}

		return s;
	}
}
/************************************************************************/
/* Ini 数据结构                                                                    
/************************************************************************/

function Ini()
{
	/** 存放键的数组(遍历用到) */  
    this.keys = new Array();   
    /** 存放数据 */  
    this.data = new Array();   

	this.fromDJson = function(djson)
	{
		for(var i=0;i<djson.length;i++)
		{
			var mv = new MapVector();
			var item = djson[i];
			mv.fromDJson(item.Value);
			this.add(item.Key,mv);
		}
	}
	
	this.see = function ()
	{
	   var s = "";
		for(var i=0;i<this.keys.length;i++)
		{
			//MapVector 
			var key = this.keys[i];
			var mvItem = this.data[key];
			if((mvItem instanceof MapVector) == false)
				throw Error("MapVector only");
			s += '[' + this.keys[i] + "]\r\n" ;
			s += mvItem.see() ;
		}
		return s;
	}
}
Ini.prototype = new MapBase();

/************************************************************************/
/*   设置页面属性                                                                   
/************************************************************************/
Function.prototype.toString = function()
	{
		return "Unsupport!";
	}
	//window.alert = function()
	//{}
	//禁止页面按键 F5 和后退，后退键除输入框不屏蔽 
	document.onkeydown=function()
	{        
		var k = window.event.keyCode;
		var name = window.event.srcElement.tagName.toLowerCase();
       var go = true;

            switch(k)
            {
            	case 8 :
            	{
            		if(name == "input")
            		{
            			var type = window.event.srcElement.type.toLowerCase();
            			if(type != "text" && type !="password")
            			    go = false;
            		}else	if(name != "textarea")
            		{
            				go = false;
            		}
            		break;
            	}
            	case 116:
            		go = false;
            		break;
            }

            if(go == false)
            window.event.keyCode = 0;
            window.event.returnValue = go;
 }

