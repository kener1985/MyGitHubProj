using System.Windows.Forms;

namespace HVH_Ken
{
    public partial class AboutInfo :  CustomIconForm
    {
        public AboutInfo()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, System.EventArgs e)
        {
            HVH_Ken_Modules.ProgFinder.FindToRun("mailto:kener1985@gmail.com?subject=意见反馈&body=此致\r\n\t敬礼!",true);
            {
                this.BackColor = HVH_Ken_Modules.GlobalVar.Instanse.StyleColor;
            }
        }
    }
}
