
namespace HVH_Ken
{
    /// <summary>
    /// 按下回车时，去掉'嘟'的声音
    /// </summary>
    internal partial class TextBoxWithoutSound : System.Windows.Forms.TextBox
    {
        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            //回车、Esc、Ctrl + 回车
            if (e.KeyChar == (char)13 || e.KeyChar == (char)27 || e.KeyChar == (char)10)
                e.Handled = true;
            base.OnKeyPress(e);
        }
    }
}
