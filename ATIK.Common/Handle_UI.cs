using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ATIK
{
    public class Handle_UI
    {
        public static List<Control> GetAllControlsRecursive(Control containerControl)
        {
            List<Control> controlList = new List<Control>();

            foreach (Control ctl in containerControl.Controls)
            {
                controlList.Add(ctl);

                if (ctl.Controls.Count > 0)
                {
                    controlList.AddRange(GetAllControlsRecursive(ctl)); // 재귀 
                }
            }
            return controlList;
        }

        public static class FormMove_DragDrop
        {
            // Window Move by Drag&Drop 
            private static Point mousePoint;
            public static void Object_MouseDown(MouseEventArgs e)
            {
                mousePoint = new Point(e.X, e.Y);
            }

            public static void Object_MouseHover(Control control)
            {
                control.Cursor = Cursors.NoMove2D;
            }

            public static void Object_MouseLeave(Control control)
            {
                control.Cursor = Cursors.Arrow;
            }

            public static void Object_MouseMove(Control control, MouseEventArgs e)
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    control.Location = new Point(control.Left - (mousePoint.X - e.X),
                        control.Top - (mousePoint.Y - e.Y));
                }
            }

            public static void Object_DoubleClickedToCenter(Control control)
            {
                Size screenSize = Screen.PrimaryScreen.Bounds.Size;
                control.Location = new Point(screenSize.Width / 2 - control.Size.Width / 2, screenSize.Height / 2 - control.Size.Height / 2);
            }
        }

        public static void ComboBoxItem_AlignCenter(object sender, DrawItemEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                // Always draw the background
                e.DrawBackground();
                // Drawing one of the items?
                if (e.Index >= 0)
                {
                    // Set the string alignment.  Choices are Center, Near and Far
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    // Set the Brush to ComboBox ForeColor to maintain any ComboBox color settings
                    // Assumes Brush is solid
                    Brush brush = new SolidBrush(cbx.ForeColor);
                    // If drawing highlighted selection, change brush
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;
                    // Draw the string
                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }
    }
}
