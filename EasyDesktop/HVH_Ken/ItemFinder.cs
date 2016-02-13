using System;
using System.Windows.Forms;
namespace HVH_Ken
{
    public partial class ItemFinder : CustomIconForm
    {
        private HVH_Ken_Modules.ItemPosite itemPos;
        private ListView m_Lv;
        public ItemFinder(ListView lv)
        {
            itemPos = new HVH_Ken_Modules.ItemPosite(lv);
            m_Lv = lv;
            lv.MultiSelect = false;
            InitializeComponent();
        }
        public void SetFieldToSearch(string[] value)
        {
             this.itemPos.FieldToSearch = value; 
        }
        /// <summary>
        /// 下一条
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void btnNext_Click(object sender, EventArgs e)
        {
            string[] Fields = CollectFields();;

            if (Fields == null)
                return;

            itemPos.FieldToSearch = Fields;
            itemPos.FindNext(tbCdt.Text.Trim());
        }
        private string[] CollectFields()
        { 
            CheckBox[] cbs = {cbTitle,cbPath,cbCmd,cbIsAR,cbIsTask};
            int iChkCount = CountChecked();

            if (iChkCount == 0)
                return null;

            string[] Fields = new string[iChkCount];
            int i = 0;
            foreach(CheckBox cb in cbs)
            {   
                if(cb.Checked)
                {
                    Fields[i++] = cb.Tag.ToString();
                }
                
            }
            return Fields;
        }
        private int CountChecked()
        {
            int iChkCount = 0;
            CheckBox[] cbs = { cbTitle, cbPath, cbCmd, cbIsAR, cbIsTask };
            foreach (CheckBox cb in cbs)
            {
                if (cb.Checked)
                    iChkCount++;
            }
            
            return iChkCount;
        }
        private void ItemFinder_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            m_Lv.MultiSelect = true;
            e.Cancel = true;
        }

        private void tbCdt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string[] Fields = CollectFields(); ;

                if (Fields == null)
                    return;

                itemPos.FieldToSearch = Fields;
                itemPos.Find(tbCdt.Text.Trim(),0);
            }
        }
        public new void Activate()
        {
            base.Activate();
            tbCdt.Focus();
            tbCdt.SelectAll();
        }

        private void tbCdt_TextChanged(object sender, EventArgs e)
        {
            itemPos.Reset();
        }

        private void lbAdv_Click(object sender, EventArgs e)
        {
            int iChgHeight;
            gbAdv.Visible = !gbAdv.Visible;
            iChgHeight = (int)gbAdv.Tag;
            iChgHeight = gbAdv.Visible ? iChgHeight : -iChgHeight;
            this.Height = this.Height + iChgHeight;
        }

        private void ItemFinder_Load(object sender, EventArgs e)
        {
            gbAdv.Tag = gbAdv.Height;
            lbAdv_Click(null, null);
        }
    }


    
}
