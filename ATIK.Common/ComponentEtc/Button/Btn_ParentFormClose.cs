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
    public partial class Btn_ParentFormClose : UserControl
    {
        public Btn_ParentFormClose()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ParentForm.Close();
        }
    }
}
