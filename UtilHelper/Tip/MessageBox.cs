using System;
using System.Windows.Forms;
namespace UtilHelper.Tip
{
    public class MB
    {
        private string _Caption;
        public MB()
        {
            _Caption = String.Empty;
        }
        public MB(string caption)
        {
            _Caption = caption;
        }
        #region Infomation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="msg">用object是为了方便传入的对象为非String，不用总是调ToString方法.</param>
        public void Info(string caption, object msg)
        {
            MessageBox.Show(msg.ToString(), caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void Info(object msg)
        {
            Info(_Caption, msg);
        }
        
        #endregion
        #region Warm
        public DialogResult Warm(string caption, object msg)
        {
            return MessageBox.Show(msg.ToString(), caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public DialogResult Warm(object msg)
        {
            return Warm(_Caption, msg);
        }
        #endregion
        #region Error
        public void Error(string caption, object msg)
        {
            MessageBox.Show(msg.ToString(), caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void Error(object msg)
        {
            Error(_Caption, msg);
        }
        #endregion
        #region Question
        public DialogResult Question(string caption, object msg)
        {
            return MessageBox.Show(msg.ToString(), 
                caption,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }
        public DialogResult Question(object msg)
        {
            return Question(_Caption, msg);
        }
        #endregion
        #region Stop
        public void Stop(string caption, object msg)
        {
            MessageBox.Show(msg.ToString(), caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        public void Stop(object msg)
        {
            Info(_Caption, msg);
        }
        #endregion
    }
}
