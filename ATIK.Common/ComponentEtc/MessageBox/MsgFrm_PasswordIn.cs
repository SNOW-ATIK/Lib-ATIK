using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATIK
{
    public partial class MsgFrm_PasswordIn : Form
    {
        //private SDDio.DioTCB mBuzzer;
        private bool bBuzzerOn = false;
        public string Password_KeyIn
        {
            get;
            private set;
        }

        private bool NumPad = false;

        public MsgFrm_PasswordIn(string msg, bool numPad = false, bool bEnbBuzzer = true)
        {
            NumPad = numPad;
            bBuzzerOn = bEnbBuzzer;

            InitializeComponent();

            ChangeLanguage();

            this.TopMost = true;

            if (bBuzzerOn == true)
            {
                //DRV_TowerLamp.BUZZER_WARN_ON();
            }
            if (msg.Contains("\r\n") == true)
            {
                Font fnt = new System.Drawing.Font("Consolas", lbl_Msg.Font.Size * 0.8f);
                lbl_Msg.Font = fnt;
            }
            lbl_Msg.Text = msg;
            txt_Password.Focus();
        }

        private void ChangeLanguage()
        {
            //string fontname = "";
            //float fontSize = lbl_Msg.Font.Size;

            //this.lbl_Msg.Font = new System.Drawing.Font(fontname, fontSize, System.Drawing.FontStyle.Bold);
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Password_KeyIn = txt_Password.Text.Trim();
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Password_KeyIn = string.Empty;
            this.Close();
        }

        private void FRM_Notify_YesNo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bBuzzerOn == true)
            {
                //DRV_TowerLamp.BUZZER_WARN_OFF();
            }
        }

        private void txt_Password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Password_KeyIn = txt_Password.Text.Trim();
                this.Close();
            }
        }

        private void txt_Password_Click(object sender, EventArgs e)
        {
            if (NumPad == true)
            {
                Frm_NumPad keyPad = new Frm_NumPad("Password", (int)0, true);
                keyPad.TopMost = true;
                if (keyPad.ShowDialog() == DialogResult.OK)
                {
                    txt_Password.Text = Convert.ToString(keyPad.NewValue);
                }
            }
        }
    }
}
