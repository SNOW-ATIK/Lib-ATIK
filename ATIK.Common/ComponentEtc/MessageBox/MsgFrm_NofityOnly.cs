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
    public partial class MsgFrm_NotifyOnly : Form
    {
        private bool bBuzzerOn = false;

        public MsgFrm_NotifyOnly(string msg, bool bEnbBuzzer = true, bool bEnbButton = true, bool loc_Center = true)
        {
            InitializeComponent();

            ChangeLanguage();

            this.TopMost = true;

            if (loc_Center == false)
            {
                this.StartPosition = FormStartPosition.Manual;                
                this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, Screen.PrimaryScreen.Bounds.Height - this.Height);
            }

            bBuzzerOn = bEnbBuzzer;
            btn_Msg.Enabled = bEnbButton;

            if (bBuzzerOn == true)
            {
                //DRV_TowerLamp.BUZZER_WARN_ON();
            }

            string fontname = string.Empty;
            float fontsize = 0f;

            // Default Font (ENG)
            fontname = "Consolas";
            fontsize = 18f;

            btn_Msg.Font = new Font(fontname, 36f, FontStyle.Bold);

            //switch (SD_OPERATION.SELECTED_LANGUAGE)
            //{
            //    case SupportLanguage.ENG:
            //        fontname = "Consolas";
            //        fontsize = 18f;
            //        break;

            //    case SupportLanguage.KOR:
            //        fontname = "Malgun Gothic";
            //        fontsize = 16.5f;
            //        break;

            //    case SupportLanguage.CHN:
            //        fontname = "Consolas";
            //        fontsize = 20f;
            //        break;

            //    case SupportLanguage.VNM:
            //        break;
            //}

            Font fnt;
            if (msg.Contains("\n") == true)
            {
                fnt = new System.Drawing.Font(fontname, fontsize * 0.8f, FontStyle.Bold);
            }
            else
            {
                fnt = new System.Drawing.Font(fontname, fontsize, FontStyle.Bold);
            }
            btn_Msg.Font = fnt;
            btn_Msg.Text = msg;
        }

        Timer AutoCloseTimer = new Timer();
        public void Show(int autoClose_ms)
        {
            if (autoClose_ms > 0)
            {
                AutoCloseTimer.Interval = autoClose_ms;
                AutoCloseTimer.Tick += Tmr_Tick;
                AutoCloseTimer.Start();
            }
            this.Show();
        }

        private void Tmr_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        public void UpdateMsg(string msg)
        {
            btn_Msg.Text = msg;
        }

        private void ChangeLanguage()
        {
            string fontname = "";
            float fontSize = btn_Msg.Font.Size;

            this.btn_Msg.Font = new System.Drawing.Font(fontname, fontSize, System.Drawing.FontStyle.Bold);
        }

        private void FRM_Notify_MsgOnly_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bBuzzerOn == true)
            {
                //DRV_TowerLamp.BUZZER_WARN_OFF();
            }
        }

        private void btn_Msg_Click(object sender, EventArgs e)
        {
            AutoCloseTimer.Tick -= Tmr_Tick;
            AutoCloseTimer.Stop();

            this.Close();
        }
    }
}
