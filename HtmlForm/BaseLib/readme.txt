配合GlobalVar使用
GlobalVar.AppPath:Browser初始化时，会将该路径设为主程序路径
Browser.Navigate接受正常http或file等协议，内部自定义协议为receipt
receipt协议：如receipt://action/command/?key=value，其中参数中invoke和action为关键字

如为post，用PostData函数(BaseFunc.js，请求会转到action字段指定的Action－post中进行中转，最终通过action和cmd字段确定最终处理Action数据放于data字段中
如果为form发送，表单中的action值对receipt无效(Browser内部原因)，因此action值可以为空，或action="?action=default&cmd=command"

PostData可发送json数据，并通过EasyUIDataTable和EasyUIDataSet进行反序列化

数据记录放于rows中，total可选，记录当前条数，该数据格式配合EasyUI用
{rows:[{record},{record}...],total:n}