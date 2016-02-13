using System;
using System.Collections.Generic;
using System.Text;

namespace HVH_Ken_Modules
{
    public class MessagePool
    {
        private static MessagePool m_instance = new MessagePool();
        private Queue<MessageObj> m_msgs ;
        private System.Threading.AutoResetEvent m_event;
        private System.Threading.Mutex m_mutex;
        public static MessagePool GetInstanse()
        {
            return m_instance;
        }
        private MessagePool()
        {
            m_msgs = new Queue<MessageObj>();
            m_event = new System.Threading.AutoResetEvent(false);
            m_mutex = new System.Threading.Mutex();
        }

        //public void SetEmptyState()
        //{
        //    m_event.Reset();
        //    m_mutex.ReleaseMutex();
        //}
       
        public MessageObj PopMessage()
        {
            //下面两行不能调换位置，否则可能会死锁
            //GlobalVar.Instanse.LogInfo("PopMessage：检查消息队列是否有信号");
            m_event.WaitOne();
            //GlobalVar.Instanse.LogInfo("PopMessage：尝试进入待消息队列");
            m_mutex.WaitOne();
            //GlobalVar.Instanse.LogInfo("PopMessage：已进入待消息队列");
            MessageObj msg = null;
            if (m_msgs.Count != 0)
            {
                msg = m_msgs.Dequeue();
            }
            else
            {
                //通知消息队列已空
                m_event.Reset();
            }
            m_mutex.ReleaseMutex();
            //GlobalVar.Instanse.LogInfo("PopMessage：退出消息队列");
            return msg;
        }
        public void PushMessage(MessageObj msg)
        {
            //GlobalVar.Instanse.LogInfo("PushMessage：尝试进入待消息队列");
            m_mutex.WaitOne();
            //GlobalVar.Instanse.LogInfo("PushMessage：已进入待消息队列");

            m_msgs.Enqueue(msg);
            //GlobalVar.Instanse.LogInfo("PushMessage：消息队列设置为有信息状态");
            m_event.Set();
            m_mutex.ReleaseMutex();
            //GlobalVar.Instanse.LogInfo("PushMessage：退出消息队列");
        }
    }
    /// <summary>
    /// 消息池对象
    /// </summary>
    public class MessageObj
    {
        private string m_name;
        private object m_param;

        public MessageObj(string name, object param)
        {
            this.m_name = name;
            this.m_param = param;
        }
        public string Name
        {
            get { return m_name; }
        }
        public object Param
        {
            get { return m_param; }
        }

    }
}
