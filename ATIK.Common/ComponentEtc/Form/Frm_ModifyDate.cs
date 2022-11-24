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
    public partial class Frm_ModifyDate : Form
    {
        private bool ModifyTime = false;

        public string OldValue { get; private set; }
        public string NewValue { get; private set; }
        public Frm_ModifyDate(string name, GenericParam<string> genDate, bool modifyTime = false)
        {
            InitializeComponent();

            OldValue = genDate.Value;
            DateTime date = DateTime.Parse(genDate.Value);
            Init(name, date, modifyTime);
        }

        public Frm_ModifyDate(string name, string strDate, bool modifyTime = false)
        {
            InitializeComponent();

            OldValue = strDate;
            DateTime date = DateTime.Parse(strDate);
            Init(name, date, modifyTime);
        }

        public Frm_ModifyDate(string name, DateTime dateTime, bool modifyTime = false)
        {
            InitializeComponent();

            string strDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            OldValue = strDate;
            DateTime date = DateTime.Parse(strDate);
            Init(name, date, modifyTime);
        }

        private void Init(string name, DateTime dateTime, bool modifyTime)
        {
            ModifyTime = modifyTime;
            tableLayoutPanel4.Visible = modifyTime;
            if (modifyTime == false)
            {
                tableLayoutPanel1.RowStyles[3].Height = 0;
                tableLayoutPanel1.RowStyles[4].Height = 0;

                this.Height -= 65;
            }

            lbl_Name.Text = name;

            NewValue = OldValue;

            CmpVal_Year.Init_WithOutGenPrm("Year", dateTime.Year);
            CmpVal_Year.ValueChangedEvent += CmpVal_Year_ValueChangedEvent;
            CmpVal_Month.Init_WithOutGenPrm("Month", dateTime.Month);
            CmpVal_Month.ValueChangedEvent += CmpVal_Month_ValueChangedEvent;
            CmpVal_Day.Init_WithOutGenPrm("Day", dateTime.Day);
            CmpVal_Day.ValueChangedEvent += CmpVal_Day_ValueChangedEvent;

            if (modifyTime == true)
            {
                CmpVal_Hour.Init_WithOutGenPrm("Hour", dateTime.Hour);
                CmpVal_Hour.ValueChangedEvent += CmpVal_Hour_ValueChangedEvent;
                CmpVal_Minute.Init_WithOutGenPrm("Minute", dateTime.Minute);
                CmpVal_Minute.ValueChangedEvent += CmpVal_Minute_ValueChangedEvent;
                CmpVal_Second.Init_WithOutGenPrm("Second", dateTime.Second);
                CmpVal_Second.ValueChangedEvent += CmpVal_Second_ValueChangedEvent;
            }
            else
            {
                CmpVal_Hour.Init_WithOutGenPrm("Hour", 0);
                CmpVal_Minute.Init_WithOutGenPrm("Minute", 0);
                CmpVal_Second.Init_WithOutGenPrm("Second", 0);
            }

            ChangeLanguage();
        }

        private void ChangeLanguage()
        {
            // TBD
            //switch (SigmaLanguage.CurrentLanguage)
            //{
            //    case Language.ENG:
            //        lbl_Name.Font = new Font("Consolas", 14f, FontStyle.Bold);
            //        btn_Apply.Font = new Font("Consolas", 16f, FontStyle.Bold);        
            //        btn_Cancel.Font = new Font("Consolas", 16f, FontStyle.Bold);
            //        break;

            //    case Language.KOR:
            //        lbl_Name.Font = new Font("맑은 고딕", 13f, FontStyle.Bold);
            //        btn_Apply.Font = new Font("맑은 고딕", 15f, FontStyle.Bold);
            //        btn_Cancel.Font = new Font("맑은 고딕", 15f, FontStyle.Bold);
            //        break;
            //}

            //lbl_Name.Text = SigmaLanguage.View_Main.Page_Maint.Item_ModifyDate;
            //btn_Apply.Text = SigmaLanguage.View_Main.Page_Maint.Btn_Apply;
            //btn_Cancel.Text = SigmaLanguage.View_Main.Page_Maint.Btn_Cancel;

            //string sLang = SigmaLanguage.CurrentLanguage.ToString();
            //CmpVal_Year.ChangeLanguage_Title(sLang, SigmaLanguage.View_Main.Page_Maint.DateTime_Year);
            //CmpVal_Month.ChangeLanguage_Title(sLang, SigmaLanguage.View_Main.Page_Maint.DateTime_Month);
            //CmpVal_Day.ChangeLanguage_Title(sLang, SigmaLanguage.View_Main.Page_Maint.DateTime_Day);
            //CmpVal_Hour.ChangeLanguage_Title(sLang, SigmaLanguage.View_Main.Page_Maint.DateTime_Hour);
            //CmpVal_Minute.ChangeLanguage_Title(sLang, SigmaLanguage.View_Main.Page_Maint.DateTime_Minute);
            //CmpVal_Second.ChangeLanguage_Title(sLang, SigmaLanguage.View_Main.Page_Maint.DateTime_Second);
        }

        private void CmpVal_Year_ValueChangedEvent(object sender, object oldValue, object newValue)
        {
            if (CheckValidityOfDate(Convert.ToInt32(newValue), Convert.ToInt32(CmpVal_Month.Prm_Value), Convert.ToInt32(CmpVal_Day.Prm_Value),
                                    Convert.ToInt32(CmpVal_Hour.Prm_Value), Convert.ToInt32(CmpVal_Minute.Prm_Value), Convert.ToInt32(CmpVal_Second.Prm_Value)) == false)
            {
                CmpVal_Year.Prm_Value = oldValue;
            }
        }

        private void CmpVal_Month_ValueChangedEvent(object sender, object oldValue, object newValue)
        {
            if (CheckValidityOfDate(Convert.ToInt32(CmpVal_Year.Prm_Value), Convert.ToInt32(newValue), Convert.ToInt32(CmpVal_Day.Prm_Value),
                Convert.ToInt32(CmpVal_Hour.Prm_Value), Convert.ToInt32(CmpVal_Minute.Prm_Value), Convert.ToInt32(CmpVal_Second.Prm_Value)) == false)
            {
                CmpVal_Month.Prm_Value = oldValue;
            }
        }

        private void CmpVal_Day_ValueChangedEvent(object sender, object oldValue, object newValue)
        {
            if (CheckValidityOfDate(Convert.ToInt32(CmpVal_Year.Prm_Value), Convert.ToInt32(CmpVal_Month.Prm_Value), Convert.ToInt32(newValue),
                Convert.ToInt32(CmpVal_Hour.Prm_Value), Convert.ToInt32(CmpVal_Minute.Prm_Value), Convert.ToInt32(CmpVal_Second.Prm_Value)) == false)
            {
                CmpVal_Day.Prm_Value = oldValue;
            }
        }

        private void CmpVal_Hour_ValueChangedEvent(object sender, object oldValue, object newValue)
        {
            if (CheckValidityOfDate(Convert.ToInt32(CmpVal_Year.Prm_Value), Convert.ToInt32(CmpVal_Month.Prm_Value), Convert.ToInt32(CmpVal_Day.Prm_Value),
                Convert.ToInt32(newValue), Convert.ToInt32(CmpVal_Minute.Prm_Value), Convert.ToInt32(CmpVal_Second.Prm_Value)) == false)
            {
                CmpVal_Hour.Prm_Value = oldValue;
            }
        }

        private void CmpVal_Minute_ValueChangedEvent(object sender, object oldValue, object newValue)
        {
            if (CheckValidityOfDate(Convert.ToInt32(CmpVal_Year.Prm_Value), Convert.ToInt32(CmpVal_Month.Prm_Value), Convert.ToInt32(CmpVal_Day.Prm_Value),
                Convert.ToInt32(CmpVal_Hour.Prm_Value), Convert.ToInt32(newValue), Convert.ToInt32(CmpVal_Second.Prm_Value)) == false)
            {
                CmpVal_Minute.Prm_Value = oldValue;
            }
        }

        private void CmpVal_Second_ValueChangedEvent(object sender, object oldValue, object newValue)
        {
            if (CheckValidityOfDate(Convert.ToInt32(CmpVal_Year.Prm_Value), Convert.ToInt32(CmpVal_Month.Prm_Value), Convert.ToInt32(CmpVal_Day.Prm_Value),
                Convert.ToInt32(CmpVal_Hour.Prm_Value), Convert.ToInt32(CmpVal_Minute.Prm_Value), Convert.ToInt32(newValue)) == false)
            {
                CmpVal_Second.Prm_Value = oldValue;
            }
        }

        private bool CheckValidityOfDate(int year, int month, int day, int hour = 0, int minute = 0, int second = 0, bool showMsg = true)
        {
            if (DateTime.TryParse($"{year}-{month}-{day} {hour}:{minute}:{second}", out DateTime temp) == false)
            {
                if (showMsg == true)
                {
                    MsgFrm_NotifyOnly msgFrm = new MsgFrm_NotifyOnly("Invalid Date");
                    msgFrm.ShowDialog();
                }
                return false;
            }
            return true;
        }

        private void btn_Today_Click(object sender, EventArgs e)
        {
            CmpVal_Year.Prm_Value = DateTime.Now.Year;
            CmpVal_Month.Prm_Value = DateTime.Now.Month;
            CmpVal_Day.Prm_Value = DateTime.Now.Day;
            CmpVal_Hour.Prm_Value = DateTime.Now.Hour;
            CmpVal_Minute.Prm_Value = DateTime.Now.Minute;
            CmpVal_Second.Prm_Value = DateTime.Now.Second;
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            string year = int.Parse((string)CmpVal_Year.Prm_Value).ToString("0000");
            string month = int.Parse((string)CmpVal_Month.Prm_Value).ToString("00");
            string day = int.Parse((string)CmpVal_Day.Prm_Value).ToString("00");
            if (ModifyTime == true)
            {
                string hour = int.Parse((string)CmpVal_Hour.Prm_Value).ToString("00");
                string minute = int.Parse((string)CmpVal_Minute.Prm_Value).ToString("00");
                string second = int.Parse((string)CmpVal_Second.Prm_Value).ToString("00");
                NewValue = $"{year}-{month}-{day} {hour}:{minute}:{second}";
            }
            else
            {
                NewValue = $"{year}-{month}-{day}";
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_AddYear_Click(object sender, EventArgs e)
        {
            CmpVal_Year.Prm_Value = int.Parse((string)CmpVal_Year.Prm_Value) + 1;
        }

        private void btn_AddMonth_Click(object sender, EventArgs e)
        {
            int month = int.Parse((string)CmpVal_Month.Prm_Value) + 1;
            if (month > 12)
            {
                btn_AddYear.PerformClick();
                CmpVal_Month.Prm_Value = 1;
            }
            else
            {
                CmpVal_Month.Prm_Value = month;
            }
        }

        private void btn_AddDay_Click(object sender, EventArgs e)
        {
            int day = int.Parse((string)CmpVal_Day.Prm_Value) + 1;
            if (CheckValidityOfDate(int.Parse((string)CmpVal_Year.Prm_Value), int.Parse((string)CmpVal_Month.Prm_Value), day, 0, 0, 0, false) == true)
            {
                CmpVal_Day.Prm_Value = day;
            }
            else
            {
                CmpVal_Day.Prm_Value = 1;
                btn_AddMonth.PerformClick();
            }
        }
    }
}
