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
    public partial class UsrCtrl_TimeNow : UserControl
    {
        public UsrCtrl_TimeNow()
        {
            InitializeComponent();
        }

        private void tmr_TimeNow_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            lbl_TimeNow.Text = $"{now:yyyy-MM-dd}\r\n{now:HH:mm:ss}";
        }
    }
}
