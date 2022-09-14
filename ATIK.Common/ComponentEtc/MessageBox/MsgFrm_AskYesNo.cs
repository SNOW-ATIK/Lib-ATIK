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
    public partial class MsgFrm_AskYesNo : Form
    {
        //private SDDio.DioTCB mBuzzer;
        private bool bBuzzerOn = false;

        public MsgFrm_AskYesNo(string msg, bool bEnbBuzzer = true)
        {
            bBuzzerOn = bEnbBuzzer;

            InitializeComponent();
            
            this.TopMost = true;

            if (bBuzzerOn == true)
            {
                //DRV_TowerLamp.BUZZER_WARN_ON();
            }

            string fontname = string.Empty;
            float fontsize = 0f;

            // Default Font (ENG)
            fontname = "Consolas";
            fontsize = 18f;

            btn_Yes.Font = new Font(fontname, 36f, FontStyle.Bold);
            btn_Yes.Text = "YES";
            btn_No.Font = new Font(fontname, 36f, FontStyle.Bold);
            btn_No.Text = "NO";

            //switch (SD_OPERATION.SELECTED_LANGUAGE)
            //{
            //    case SupportLanguage.ENG:
            //        fontname = "Consolas";
            //        fontsize = 18f;

            //        btn_Yes.Font = new Font(fontname, 36f, FontStyle.Bold);
            //        btn_Yes.Text = "YES";
            //        btn_No.Font = new Font(fontname, 36f, FontStyle.Bold);
            //        btn_No.Text = "NO";
            //        break;

            //    case SupportLanguage.KOR:
            //        fontname = "Malgun Gothic";
            //        fontsize = 16.5f;

            //        btn_Yes.Font = new Font(fontname, 28f, FontStyle.Bold);
            //        btn_Yes.Text = "예";
            //        btn_No.Font = new Font(fontname, 28f, FontStyle.Bold);
            //        btn_No.Text = "아니오";
            //        break;

            //    case SupportLanguage.CHN:
            //        fontname = "Consolas";
            //        fontsize = 32f;

            //        btn_Yes.Font = new Font(fontname, 36f, FontStyle.Bold);
            //        btn_Yes.Text = "YES";
            //        btn_No.Font = new Font(fontname, 36f, FontStyle.Bold);
            //        btn_No.Text = "NO";
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
            lbl_Msg.Font = fnt;
            lbl_Msg.Text = msg;

            
        }

        private void btn_Yes_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;            
            this.Close();
        }

        private void FRM_Notify_YesNo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bBuzzerOn == true)
            {
                //DRV_TowerLamp.BUZZER_WARN_OFF();
            }
        }
    }
}
