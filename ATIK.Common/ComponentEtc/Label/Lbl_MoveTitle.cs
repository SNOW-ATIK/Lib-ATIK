using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATIK
{
    public partial class Lbl_MoveTitle : UserControl
    {
        public string Title { get => lbl_Title.Text; set => lbl_Title.Text = value; }
        public Font TitleFont { get => lbl_Title.Font; set => lbl_Title.Font = value; }
        public Color TitleBackColor { get => lbl_Title.BackColor; set => lbl_Title.BackColor = value; }
        public Color TitleForeColor { get => lbl_Title.ForeColor; set => lbl_Title.ForeColor = value; }

        public Lbl_MoveTitle()
        {
            InitializeComponent();
        }

        private void lbl_Title_MouseHover(object sender, EventArgs e)
        {
            Handle_UI.FormMove_DragDrop.Object_MouseHover(this);
        }

        private void lbl_Title_MouseLeave(object sender, EventArgs e)
        {
            Handle_UI.FormMove_DragDrop.Object_MouseLeave(this);
        }

        private void lbl_Title_MouseDown(object sender, MouseEventArgs e)
        {
            Handle_UI.FormMove_DragDrop.Object_MouseDown(e);
        }

        private void lbl_Title_MouseMove(object sender, MouseEventArgs e)
        {
            Handle_UI.FormMove_DragDrop.Object_MouseMove(ParentForm, e);
        }

        private void lbl_Title_DoubleClick(object sender, EventArgs e)
        {
            Handle_UI.FormMove_DragDrop.Object_DoubleClickedToCenter(ParentForm);
        }
    }
}
