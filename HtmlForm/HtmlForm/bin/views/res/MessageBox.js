var Common = {
                getEvent: function() { //ie/ff             
                    if (document.all) {
                        return window.event;
                    }
                    func = getEvent.caller;
                    while (func != null) {
                        var arg0 = func.arguments[0];
                        if (arg0) {
                            if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof(arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {
                                return arg0;
                            }
                        }
                        func = func.caller;
                    }
                    return null;
                },
                getMousePos: function(ev) {
                    if (!ev) {
                        ev = this.getEvent();
                    }
                    if (ev.pageX || ev.pageY) {
                        return {
                            x: ev.pageX,
                            y: ev.pageY
                        };
                    }
                    if (document.documentElement && document.documentElement.scrollTop) {
                        return {
                            x: ev.clientX + document.documentElement.scrollLeft - document.documentElement.clientLeft,
                            y: ev.clientY + document.documentElement.scrollTop - document.documentElement.clientTop
                        };
                    } else if (document.body) {
                        return {
                            x: ev.clientX + document.body.scrollLeft - document.body.clientLeft,
                            y: ev.clientY + document.body.scrollTop - document.body.clientTop
                        };
                    }
                },
                getItself: function(id) {
                    return "string" == typeof id ? document.getElementById(id) : id;
                },
                getViewportSize: {
                    w: (window.innerWidth) ? window.innerWidth: (document.documentElement && document.documentElement.clientWidth) ? document.documentElement.clientWidth: document.body.offsetWidth,
                    h: (window.innerHeight) ? window.innerHeight: (document.documentElement && document.documentElement.clientHeight) ? document.documentElement.clientHeight: document.body.offsetHeight
                },
                isIE: document.all ? true: false,
                setOuterHtml: function(obj, html) {
                    var Objrange = document.createRange();
                    obj.innerHTML = html;
                    Objrange.selectNodeContents(obj);
                    var frag = Objrange.extractContents();
                    obj.parentNode.insertBefore(frag, obj);
                    obj.parentNode.removeChild(obj);
                }
            } ///------------------------------------------------------------------------------------------------------       
            var Class = {
                create: function() {
                    return function() {
                        this.init.apply(this, arguments);
                    }
                }
            }
            var Drag = Class.create();
            Drag.prototype = {
                init: function(dragDiv, Options) {
                    //设置点击是否透明，默认不透明     
                    //titleBar = Common.getItself(titleBar);
                    dragDiv = Common.getItself(dragDiv);
                    this.dragArea = {
                        maxLeft: 0,
                        maxRight: Common.getViewportSize.w - dragDiv.offsetWidth - 2,
                        maxTop: 0,
                        maxBottom: Common.getViewportSize.h - dragDiv.offsetHeight - 2
                    };
                    if (Options) {
                        this.opacity = Options.opacity ? (isNaN(parseInt(Options.opacity)) ? 100 : parseInt(Options.opacity)) : 100;
                        this.keepOrigin = Options.keepOrigin ? ((Options.keepOrigin == true || Options.keepOrigin == false) ? Options.keepOrigin: false) : false;
                        if (this.keepOrigin) {
                            this.opacity = 50;
                        }
                        if (Options.area) {
						
                            if (Options.area.left && !isNaN(parseInt(Options.area.left))) {
                                this.dragArea.maxLeft = Options.area.left
                            };
                            if (Options.area.right && !isNaN(parseInt(Options.area.right))) {
                                this.dragArea.maxRight = Options.area.right
                            };
                            if (Options.area.top && !isNaN(parseInt(Options.area.top))) {
                                this.dragArea.maxTop = Options.area.top
                            };
                            if (Options.area.bottom && !isNaN(parseInt(Options.area.bottom))) {
                                this.dragArea.maxBottom = Options.area.bottom
                            };
                        }
                    } else {
                        this.opacity = 100,
                        this.keepOrigin = false;
                    }
                    this.originDragDiv = null;
                    this.tmpX = 0;
                    this.tmpY = 0;
                    this.moveable = false;
                    var dragObj = this;
//
                    dragDiv.onmousedown = function(e) {
                        var ev = e || window.event || Common.getEvent();
                        //只允许通过鼠标左键进行拖拽,IE鼠标左键为1 FireFox为0          
                        if (Common.isIE && ev.button == 1 || !Common.isIE && ev.button == 0) {} else {
                            return false;
                        }

                        if (dragObj.keepOrigin) {
                            dragObj.originDragDiv = document.createElement("div");
                            dragObj.originDragDiv.style.cssText = dragDiv.style.cssText;
                            dragObj.originDragDiv.style.width = dragDiv.offsetWidth;
                            dragObj.originDragDiv.style.height = dragDiv.offsetHeight;
                            dragObj.originDragDiv.innerHTML = dragDiv.innerHTML;
                            dragDiv.parentNode.appendChild(dragObj.originDragDiv);
                        }
                        dragObj.moveable = true;
                        dragDiv.style.zIndex = dragObj.GetZindex() + 1;
                        var downPos = Common.getMousePos(ev);
                        dragObj.tmpX = downPos.x - dragDiv.offsetLeft;
                        dragObj.tmpY = downPos.y - dragDiv.offsetTop;
                       //
						dragDiv.style.cursor = "move";
                        if (Common.isIE) {
                            dragDiv.setCapture();
                        } else {
                            window.captureEvents(Event.MOUSEMOVE);
                        }
                        dragObj.SetOpacity(dragDiv, dragObj.opacity); //FireFox 去除容器内拖拽图片问题        
                        if (ev.preventDefault) {
                            ev.preventDefault();
                            ev.stopPropagation();
                        }
                        document.onmousemove = function(e) {
                            if (dragObj.moveable) {
                                var ev = e || window.event || Common.getEvent();
                                //IE 去除容器内拖拽图片问题                          
                                if (document.all) //IE                           
                                {
                                    ev.returnValue = false;
                                }
                                var movePos = Common.getMousePos(ev);
                                dragDiv.style.left = Math.max(Math.min(movePos.x - dragObj.tmpX, dragObj.dragArea.maxRight), dragObj.dragArea.maxLeft) + "px";
                                dragDiv.style.top = Math.max(Math.min(movePos.y - dragObj.tmpY, dragObj.dragArea.maxBottom), dragObj.dragArea.maxTop) + "px";
                            }
                        };
                        document.onmouseup = function() {
                            if (dragObj.keepOrigin) {
                                if (Common.isIE) {
                                    dragObj.originDragDiv.outerHTML = "";
                                } else {
                                    Common.setOuterHtml(dragObj.originDragDiv, "");
                                }
                            }
                            if (dragObj.moveable) {
                                if (Common.isIE) {
                                    dragDiv.releaseCapture();
                                } else {
                                    window.releaseEvents(Event.MOUSEMOVE);
                                }
                                dragObj.SetOpacity(dragDiv, 100);
								//
                                dragDiv.style.cursor = "default";
                                dragObj.moveable = false;
                                dragObj.tmpX = 0;
                                dragObj.tmpY = 0;
                            }
                        };
                    }
                },
                SetOpacity: function(dragDiv, n) {
                    if (Common.isIE) {
                        dragDiv.filters.alpha.opacity = n;
                    } else {
                        dragDiv.style.opacity = n / 100;
                    }
                },
                GetZindex: function() {
                    var maxZindex = 0;
                    var divs = document.getElementsByTagName("div");
                    for (z = 0; z < divs.length; z++) {
                        maxZindex = Math.max(maxZindex, divs[z].style.zIndex);
                    }
					//alert(maxZindex);
                    return maxZindex;
                }
            }
		
function ShowMsg  (title,msg)
			{
			LockPage();
				__divDrag = document.createElement("div");
				var left = document.documentElement.scrollLeft + (document.documentElement.clientWidth - 200)/2;
				var top = document.documentElement.scrollTop + (document.documentElement.clientHeight - 150)/2;
				__divDrag.style.position="absolute";
				__divDrag.style.backgroundcolor="#FFFFFF";
				__divDrag.style.border="solid 1px #849BCA";
				__divDrag.style.width="200px";
				__divDrag.style.height="150px";
				__divDrag.style.zIndex = "2"; 
				__divDrag.style.left= left + "px";
				__divDrag.style.top= top + "px";
				__divDrag.style.filter="alpha(opacity=100)";

				
				var tbl = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%;height:100%;border-collapse:collapse; \">";
                tbl +="<tr style=\"height:10%; text-align:left; background-color:#547BC9;color:White; padding:3px;\">";
                tbl +="    <th align=\"left\" unselectable=\"on\">";
                tbl +=       title ;
                tbl +="    </th><th align='right'><INPUT TYPE=\"button\" VALUE=\"X\" onclick='__DisposeDragObjResource()'></th></tr>";
				tbl += "<tr style=\"height:90%;padding:3px;background-color:#ccccff;overflow:scroll;\" valign=\"middle\">";
                  tbl +=  "<td colspan=2 align='center'>"+ msg+"</td></tr></table>";

				  __divDrag.innerHTML = tbl;
				 
				document.body.appendChild(__divDrag);
				  new Drag(__divDrag, {
                    opacity: 100,
                    keepOrigin: true
                });	
				}
function __DisposeDragObjResource()
{
	document.body.removeChild(__divDrag);
	document.body.removeChild(__divMask);
}
function LockPage()
	{
		var w = document.body.scrollWidth;
		var h = document.body.scrollHeight;
		w=Math.max(document.body.scrollWidth,document.documentElement.scrollWidth); 
		h=Math.max(document.body.scrollHeight,document.documentElement.scrollHeight); 

		__divMask= document.createElement("div");
		__divMask.id = "mask";
		__divMask.style.filter = "alpha(opacity=30)";
		__divMask.style.background="#33393C"; 
		__divMask.style.width = w + "px";
		__divMask.style.height = h  + "px";
		__divMask.style.position="absolute"; 
		__divMask.style.top = "0px";
		__divMask.style.left = "0px";
		__divMask.style.zIndex = "1"; 
		__divMask.style.display = "";

		document.body.appendChild(__divMask);
	}