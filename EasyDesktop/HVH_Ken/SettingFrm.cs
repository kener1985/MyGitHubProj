using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections.Specialized;
using HVH_Ken_Modules;
using UtilHelper.Converter;
namespace HVH_Ken
{
    public partial class SettingFrm : CustomIconForm
    {

        /// <summary>
        /// 更改前的关键字标识
        /// </summary>
        private string m_sKeyWordOri;
        //private bool m_bClChg;
        public SettingFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化.
        /// </summary>
        private void SettingForm_Load(object sender, EventArgs e)
        {
            m_sKeyWordOri = GlobalVar.Instanse.KEY_WORD_FLAG;
            ShowHotKeyInfo();
            tbKWFlag.Text = GlobalVar.Instanse.KEY_WORD_FLAG;
            tbUrl.Text = GlobalVar.Instanse.DEFAULT_SEARCH_ENGINE_URL;
            //pdWarmLmt.Maximum = Int32.MaxValue;
            //m_bClChg = false;
            try
            {
                menuStrip.BackColor = HVH_Ken_Modules.GlobalVar.Instanse.StyleColor;
            }
            catch (Exception)
            { 
            }
            //MessageBox.Show(Environment.CurrentDirectory);
        }

        private void ShowHotKeyInfo()
        {
            if (GlobalVar.Instanse.HOT_KEY_MODIFIER == 1)
                tbHkAct.Text = "Alt + " + ((char)GlobalVar.Instanse.HOT_KEY_ACTIVE).ToString();
            else if (GlobalVar.Instanse.HOT_KEY_MODIFIER == 2)
                tbHkAct.Text = "Ctrl + " + ((char)GlobalVar.Instanse.HOT_KEY_ACTIVE).ToString();
            if (GlobalVar.Instanse.HOT_KEY_SHOWINFO != 0)
                tbHkInf.Text = ((char)GlobalVar.Instanse.HOT_KEY_SHOWINFO).ToString();
        }
        /// <summary>
        /// 根据快捷名称查找程序，如果找得到，则运行，否则返回false.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        /// <returns></returns>
        private void AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutInfo().ShowDialog();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fullpath = GlobalVar.PROGRAM_ROOT_PATH + @"\help.txt";
            if (!File.Exists(fullpath))
            {
                GlobalVar.Tip.Info("帮助文件不存在！");
                return;
            }
            try
            {
                ProgFinder.FindToRun(fullpath,true);
            }
            catch (Exception ex)
            {
                GlobalVar.Tip.Error(ex.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            uint iatv = (uint)(tbHkAct.Text == string.Empty ? 0 : (int)tbHkAct.Text[tbHkAct.Text.Length - 1]);
            uint isif = (uint)(tbHkInf.Text == string.Empty ? 0 : (int)tbHkInf.Text[0]);

            //检验数据        
            //判断热键是否冲突
            if (iatv == isif)
            {
                GlobalVar.Tip.Error("热键冲突");
                return;
            }

            string sKWFlag = tbKWFlag.Text.Trim();
            string sUrl = tbUrl.Text.Trim();
            //判断关键字标志的复杂度
            if(!sKWFlag.Equals(string.Empty) || !sUrl.Equals(string.Empty))
                if (!CheckKWFlag(sKWFlag))
                    return;
            //写入数据
            GlobalVar.Instanse.HOT_KEY_ACTIVE = iatv;
            //GlobalVar.Instanse.HOT_KEY_CHANGE = ichg;
            GlobalVar.Instanse.HOT_KEY_SHOWINFO = isif;


            GlobalVar.Instanse.HOT_KEY_MODIFIER = 0;
            if (tbHkAct.Text != string.Empty)
            {
                if (tbHkAct.Text.Contains("Alt"))
                    GlobalVar.Instanse.HOT_KEY_MODIFIER = 1;
                else
                    GlobalVar.Instanse.HOT_KEY_MODIFIER = 2;

            }

            GlobalVar.Instanse.DEFAULT_SEARCH_ENGINE_URL = sUrl;
            GlobalVar.Instanse.KEY_WORD_FLAG = sKWFlag;

            //如果关键字标识有变，刷新所有配置的URL
            if (!GlobalVar.Instanse.KEY_WORD_FLAG.Equals(m_sKeyWordOri))
            {
                ReplaceKeyWord(m_sKeyWordOri, GlobalVar.Instanse.KEY_WORD_FLAG);
                m_sKeyWordOri = GlobalVar.Instanse.KEY_WORD_FLAG;
            }
            GlobalVar.Instanse.NeedToSaveCfg = true;
            //if(m_bClChg)
            //    GlobalVar.Instanse.StyleColor = colorDialog.Color;
            
            GlobalVar.Tip.Info("保存成功");
            Close();
        }

        private bool CheckKWFlag(string sKey)
        {
            bool bRtn = true;
            char[] cSigns = new char[] { '`', '~', '!', '#', '$', '^', '&', '*', '(', ')', '-', '_', '+', '|' };
            string sSigns = new string(cSigns);
            int iHints = 0;//特殊符号的命中数

            if (sKey.Length < 6)
                bRtn = false;
            else
            {
                //检查特殊符号的命中数
                char cTmp = ' ';
                foreach (char c in cSigns)
                {
                    if (sKey.IndexOf(c) != -1)
                    {
                        if (iHints == 0)
                        {
                            cTmp = c;
                            iHints = 1;
                        }

                        if (c != cTmp)
                        {
                            iHints++;
                            if (iHints >= 2)
                            {
                                return true;
                            }
                        }
                    }
                }
                if (iHints < 2)
                    bRtn = false;
            }
            if (bRtn == false)
                GlobalVar.Tip.Info("关键标识复杂度过低，请检查是否符合以下格式\r\n" +
                                "1、至少6个字符\r\n" +
                                "2、至少含有 " + sSigns + " 中的任两个");
            return bRtn;
        }
        private void ReplaceKeyWord(string sOld, string sNew)
        {
            if (sOld == null || sOld.Equals(String.Empty))
                return;
            StringCollection sc = new StringCollection();
            foreach (string sKey in GlobalVar.Instanse.SrcEngInfo.Keys)
            {
                sc.Add(sKey);
            }
            GlobalVar.Instanse.DEFAULT_SEARCH_ENGINE_URL = GlobalVar.Instanse.DEFAULT_SEARCH_ENGINE_URL.Replace(sOld, sNew);
            tbUrl.Text = tbUrl.Text.Replace(sOld, sNew);
            foreach (string sKey in sc)
            {
                GlobalVar.Instanse.SrcEngInfo[sKey] = GlobalVar.Instanse.SrcEngInfo[sKey].ToString().Replace(sOld, sNew);
            }
        }
        
        private void btnEngEdt_Click(object sender, EventArgs e)
        {
            SrcEngEdt frm = new SrcEngEdt();
            frm.ShowInTaskbar = false;
            frm.ShowDialog();
        }

        private void SettingFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalVar.Instanse.IsSetFrmShown = false;
        }

        private void tbHkChg_TextChanged(object sender, EventArgs e)
        {
            if (tbHkAct.Text != string.Empty)
            {
                int iPos = tbHkAct.Text.IndexOf('+');
                if (iPos != -1)
                {
                    lbSinfMod.Text = tbHkAct.Text.Substring(0, iPos + 1);
                }
                else
                {
                    lbSinfMod.Text = string.Empty;
                }

            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            

        }

        private void btnClearHints_Click(object sender, EventArgs e)
        {
            if (GlobalVar.Tip.Question("确定清零吗?") != DialogResult.Yes)
                return;
            GlobalVar.Helper.ExcuteForUnique("update [programs] set hints=0");
        }
    }
}
