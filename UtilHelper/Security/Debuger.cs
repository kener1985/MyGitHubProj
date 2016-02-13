using System.Text;
using System.Security.Cryptography;
namespace UtilHelper.Security
{
    class Debuger
    {
        internal static void ShowLegalSize(KeySizes[] ks)
        {
            StringBuilder sTmp = new StringBuilder(128);
            foreach (KeySizes item in ks)
            {
                sTmp.Append("Max:").AppendLine(item.MaxSize.ToString());
                sTmp.Append("Skip:").AppendLine(item.SkipSize.ToString());
                sTmp.Append("Min:").Append(item.MinSize.ToString());
                System.Windows.Forms.MessageBox.Show(sTmp.ToString());
            }
        }
    }
}
