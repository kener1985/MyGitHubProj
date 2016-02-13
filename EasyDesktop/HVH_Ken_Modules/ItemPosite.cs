using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HVH_Ken_Modules
{
    /// <summary>
    /// 
    /// </summary>
    public class ItemPosite : IDisposable
    {
        private int m_iCurIndex;
        private System.Windows.Forms.ListView m_lvObj;
        private string [] m_szFields;
        public ItemPosite(System.Windows.Forms.ListView lv)
        {
            m_lvObj = lv;
            m_iCurIndex = 0;
        }
        public string[] FieldToSearch
        {
            set { this.m_szFields = value; }
        }
        public void FindNext(string sKey)
        {
            int startIndex = m_iCurIndex;
            if (startIndex >= m_lvObj.Items.Count)
            {
                startIndex = 0;
                m_iCurIndex = -1;
            }
            Find(sKey, startIndex);
            m_iCurIndex++;
        }
        /// <summary>
        /// Searches the item.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="bIsPre">if set to <c>true</c> [b is pre].是否向前查找</param>
        public void Find(string sKey, int startIndex)
        {
            if (m_lvObj.Items.Count == 0)
                return;
            if (startIndex >= m_lvObj.Items.Count || startIndex < 0)
                startIndex = 0;
            int bIndex = CustomSearch(sKey, startIndex);
            if (bIndex != -1)
            {
                System.Windows.Forms.ListViewItem item = m_lvObj.Items[bIndex];
                item.Selected = true;
                item.Focused = true;
                m_lvObj.Select();
                m_lvObj.EnsureVisible(item.Index);
                m_iCurIndex = item.Index;
                m_lvObj.Focus();
            }
            else
            {
                m_iCurIndex = -1;
            }
        }

        /// <summary>
        /// Searches the by title.
        /// </summary>
        /// <param name="strTitle">The STR title.</param>
        /// <param name="bStart">初始位置.</param>
        /// <returns>-1:没有对应项;关键字模糊匹配的对应项</returns>
        public int CustomSearch(string strTitle, int iStart)
        {
            //for (int iPos =iStart;iPos < m_lvObj.Items.Count;iPos++)
            Type tData = typeof(ConfigData);
            for (int iPos = iStart; iPos < m_lvObj.Items.Count; iPos++)
            {
                System.Windows.Forms.ListViewItem item = m_lvObj.Items[iPos];
                foreach (string sField in m_szFields)
                {
                    if (sField == null || sField.Equals(String.Empty))
                        continue;
                    //iRtn = item.SubItems[iCol].Text.ToLower().IndexOf(strTitle.ToLower());
                    ConfigData data = item.Tag as ConfigData;
                    
                    FieldInfo fi = tData.GetField(sField,BindingFlags.Instance|BindingFlags.NonPublic);
                    if (fi == null || data == null)
                        continue;
                    object value = fi.GetValue(data);
                    bool bHit = false;
                    bHit |= (fi.FieldType == typeof(bool) && value.ToString().Equals("True"));
                    //value.ToString()
                    bHit |= (fi.FieldType == typeof(string) && strTitle.ToString() != string.Empty && 
                            value.ToString().ToLower().IndexOf(strTitle.ToLower()) != -1);
                    bHit |= (fi.FieldType == typeof(TaskItem) && value != null);
                    if ( bHit == true )
                    {
                        return iPos;
                    }
                   
                }
            }
            return -1;
        }
        public void Reset()
        {
            m_iCurIndex = 0;
        }
        public void Dispose()
        {
            m_lvObj.MultiSelect = true;
        }
    }
}
