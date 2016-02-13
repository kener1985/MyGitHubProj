using System;
using System.Windows.Forms;
using System.Collections;
using HVH_Ken_Modules;
namespace HVH_Ken
{
    public partial class AdvSrcFrm :  CustomIconForm
    {
        private int m_iRememberIndex;//记住上次操作的搜索引擎
        public AdvSrcFrm()
        {
            InitializeComponent();
           
        }

       
        private void TryRun()
        {
            string strSrcUrl = GlobalVar.Instanse.DEFAULT_SEARCH_ENGINE_URL;
            string sKey = tbKeyWord.Text.Trim();
            if (!sKey.Equals(String.Empty))
            {
                if (cbxUrl.SelectedIndex != 0)
                    strSrcUrl = GlobalVar.Instanse.SrcEngInfo[cbxUrl.Text].ToString();
                
                    try
                    {
                        ProgFinder.StartSerachEngine(sKey, strSrcUrl);
                        tbKeyWord.AutoCompleteCustomSource.Add(sKey);
                        this.Hide();
                    }
                    catch (Exception)
                    {
                        tbKeyWord.Focus();
                        tbKeyWord.SelectAll();
                        toolTip.SetToolTip(tbKeyWord, "找不到合适的搜索引擎 : [ " + strSrcUrl + " ]\r\n或关键字占位符为空");
                        return;
                    }
               
            }

        }



        private void AdvSrc_Activated(object sender, EventArgs e)
        {
            tbKeyWord.Focus();
            tbKeyWord.SelectAll();
            //每次激活界面时都应该更新搜索引擎，因为用户有可能删除当前搜索引擎
            RefreshEnginName();
            if (m_iRememberIndex < cbxUrl.Items.Count)
                cbxUrl.SelectedIndex = m_iRememberIndex;
            else
                cbxUrl.SelectedIndex = 0;
        }

        //
        /// <summary>
        /// 刷新搜索引擎名.
        /// </summary>
        private void RefreshEnginName()
        {
            cbxUrl.Items.Clear();
            cbxUrl.Items.Add("默认");
            if (GlobalVar.Instanse.SrcEngInfo != null)
            {
                foreach (DictionaryEntry entry in GlobalVar.Instanse.SrcEngInfo)
                    cbxUrl.Items.Add(entry.Key);
            }
        }

        private void cbxUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_iRememberIndex = cbxUrl.SelectedIndex;
        }

        private void tbKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)//按下Enter键
            {
                TryRun();
            }
            else if (e.KeyData == Keys.Escape)//按下Esc键
            {
                this.Hide();
            }
        }

        private void AdvSrcFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            this.Hide();
            e.Cancel = true;
        }
}

}
