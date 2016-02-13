using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HVH_Ken_Modules;
using System.Drawing;

namespace HVH_Ken
{
    class MyListView : ListView 
    {
        public MyListView()
        {
            DoubleBuffered = true;
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            base.OnDrawColumnHeader(e);
            e.DrawBackground();
            e.DrawText();
            
            //e.BackColor = System.Drawing.Color.AliceBlue;
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            base.OnDrawSubItem(e);
            if (e.ItemState == 0)
            {
                return;
            }
            if ((e.ItemState & ListViewItemStates.Selected) == ListViewItemStates.Selected)
                e.SubItem.BackColor = System.Drawing.Color.LightSkyBlue;
            else
            {
                ConfigData data = e.Item.Tag as ConfigData;
                if (data != null && (data.HasTaskitem || data.IsAutRun))
                {
                    e.SubItem.BackColor = System.Drawing.Color.Pink;
                }
                else if (e.ItemIndex % 2 != 0)
                    e.SubItem.BackColor = HVH_Ken_Modules.GlobalVar.Instanse.StyleColor;
                else
                    e.SubItem.BackColor = this.BackColor;
            }
            e.DrawBackground();
            
            using (StringFormat sf = new StringFormat())
            {
                HorizontalAlignment align = Columns[e.ColumnIndex].TextAlign;
                if (align == HorizontalAlignment.Center)
                    sf.Alignment = StringAlignment.Center;
                else if (align == HorizontalAlignment.Right)
                    sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(e.SubItem.Text, Font, new SolidBrush(ForeColor), e.Bounds, sf);
            }
           
            e.DrawFocusRectangle(e.Bounds);

            if ((e.ItemState & ListViewItemStates.Focused) == ListViewItemStates.Focused)
            {
                if (this.FullRowSelect == false)
                {
                    if (e.ColumnIndex == 0)
                    {
                        e.DrawFocusRectangle(e.Bounds);
                    }
                }
                else
                    e.DrawFocusRectangle(e.Bounds);

            }
        }
    }
}
