using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATIK
{
    public partial class UsrCtrl_LifeTime : UserControl
    {
        public bool IsSelected { get => chk_Select.Checked; }

        private LifeTimeObj MyInfo;
        private bool ModifyAvailable_Since = false;
        private bool ModifyAvailable_Current = false;
        private bool ModifyAvailable_Last = false;
        private List<IParam> GenParams = new List<IParam>();
        private List<IPrmCmp> CmpComps = new List<IPrmCmp>();
        private string TextToDraw_Remain = string.Empty;
        private bool IsTextColorBlack = false;

        private Language CurrentLanguage = Language.ENG;

        public UsrCtrl_LifeTime()
        {
            InitializeComponent();
        }

        public UsrCtrl_LifeTime(LifeTimeObj partInfo, bool modifySince, bool modifyCurrent, bool modifyLast, bool simplify = false)
        {
            InitializeComponent();

            MyInfo = partInfo;

            Init(modifySince, modifyCurrent, modifyLast, simplify);
        }

        public void Init(bool modifySince, bool modifyCurrent, bool modifyLast, bool simplify = false)
        {
            CmpCol_Enabled.Init(MyInfo.Gen_Enabled, "Enabled");
            GenParams.Add(MyInfo.Gen_Enabled);

            CmpCol_Type.Init(MyInfo.Gen_Type, "Type", Enum.GetValues(typeof(LifeTimeMngType)));
            GenParams.Add(MyInfo.Gen_Type);

            switch (MyInfo.Gen_Type.Value)
            {
                case LifeTimeMngType.Period:
                    CmpVal_Since.UseUserKeyPad = true;
                    CmpVal_Current.UseUserKeyPad = true;
                    CmpVal_Last.UseUserKeyPad = true;

                    CmpVal_Since.Init(MyInfo.Gen_Period_Since, "Since");

                    CmpVal_Current.Init(MyInfo.Gen_Period_Current, "Current");
                    //CmpVal_Current.ValueClickedEvent += CmpVal_Current_ValueClickedEvent;
                    CmpVal_Current.EnableModifying(true, false);

                    CmpVal_Last.Init(MyInfo.Gen_Period_Last, "Last");

                    CmpVal_Since.ValueClickedEvent += CmpVal_Since_ValueClickedEvent;
                    CmpVal_Last.ValueClickedEvent += CmpVal_Last_ValueClickedEvent;

                    GenParams.Add(MyInfo.Gen_Period_Since);
                    GenParams.Add(MyInfo.Gen_Period_Current);
                    GenParams.Add(MyInfo.Gen_Period_Last);
                    break;

                case LifeTimeMngType.Count:
                    CmpVal_Since.UseUserKeyPad = false;
                    CmpVal_Current.UseUserKeyPad = false;
                    CmpVal_Last.UseUserKeyPad = false;

                    CmpVal_Since.Init(MyInfo.Gen_Count_Since, "Since");

                    CmpVal_Current.Init(MyInfo.Gen_Count_Current, "Current");
                    //CmpVal_Current.ValueClickedEvent += CmpVal_Current_ValueClickedEvent;
                    CmpVal_Current.EnableModifying(true, false);

                    CmpVal_Last.Init(MyInfo.Gen_Count_Last, "Last");

                    CmpVal_Since.ValueChangedEvent += CmpVal_Since_ValueChangedEvent;
                    CmpVal_Last.ValueChangedEvent += CmpVal_Last_ValueChangedEvent;

                    GenParams.Add(MyInfo.Gen_Count_Since);
                    GenParams.Add(MyInfo.Gen_Count_Current);
                    GenParams.Add(MyInfo.Gen_Count_Last);
                    break;
            }

            ModifyAvailable_Since = modifySince;
            ModifyAvailable_Current = modifyCurrent;
            ModifyAvailable_Last = modifyLast;

            lbl_Name.Text = MyInfo.Gen_Name.Value;

            CmpComps.Add(CmpCol_Enabled);
            CmpComps.Add(CmpCol_Type);
            CmpComps.Add(CmpVal_Since);
            CmpComps.Add(CmpVal_Current);
            CmpComps.Add(CmpVal_Last);

            SetRate(MyInfo);

            if (simplify == true)
            {
                tableLayoutPanel1.ColumnStyles[0].Width = 0;
                tableLayoutPanel1.ColumnStyles[1].Width = 95;
                tableLayoutPanel1.ColumnStyles[2].Width = 0;
                tableLayoutPanel1.ColumnStyles[3].Width = 0;
                tableLayoutPanel1.ColumnStyles[4].Width = 0;
                tableLayoutPanel1.ColumnStyles[5].Width = 0;
                tableLayoutPanel1.ColumnStyles[6].Width = 0;
                tableLayoutPanel1.ColumnStyles[7].Width = 0;
                //tableLayoutPanel1.ColumnStyles[8].Width = 0;
                tableLayoutPanel1.ColumnStyles[9].Width = 0;

                chk_Select.Visible = false;
                CmpCol_Enabled.Visible = false;
                CmpCol_Type.Visible = false;
                CmpVal_Since.Visible = false;
                CmpVal_Current.Visible = false;
                CmpVal_Last.Visible = false;
                lbl_Status.Visible = false;
                btn_Set.Visible = false;

                this.MinimumSize = new Size(0, 30);
            }
        }

        public void ChangeLanguage(Language language)
        {
            if (CurrentLanguage == language)
            {
                return;
            }
            CurrentLanguage = language;

            CmpCol_Enabled.ChangeLanguage_Title(language, Texts.GetText(language, "Item_Enabled"));
            CmpCol_Type.ChangeLanguage_Title(language, Texts.GetText(language, "Item_Type"));
            CmpVal_Since.ChangeLanguage_Title(language, Texts.GetText(language, "Item_Since"));
            CmpVal_Current.ChangeLanguage_Title(language, Texts.GetText(language, "Item_Current"));
            CmpVal_Last.ChangeLanguage_Title(language, Texts.GetText(language, "Item_Last"));

            switch (language)
            {
                case Language.ENG:
                    btn_Set.Font = new Font("Consolas", 12f, FontStyle.Bold);
                    lbl_Remain.Font = new Font("Consolas", 12f, FontStyle.Bold);
                    lbl_Status.Font = new Font("Consolas", 12f, FontStyle.Bold);

                    TextToDraw_Remain = TextToDraw_Remain.Replace("일 남음", "day(s) left");
                    TextToDraw_Remain = TextToDraw_Remain.Replace("회 남음", "time(s) left");
                    TextToDraw_Remain = TextToDraw_Remain.Replace("일 지남", "day(s) over");
                    TextToDraw_Remain = TextToDraw_Remain.Replace("회 넘음", "time(s) over");
                    TextToDraw_Remain = TextToDraw_Remain.Replace("만료됨", "Expired");

                    //lbl_Remain.Text = lbl_Remain.Text.Replace("일 남음", "day(s) left");
                    //lbl_Remain.Text = lbl_Remain.Text.Replace("회 남음", "time(s) left");
                    //lbl_Remain.Text = lbl_Remain.Text.Replace("일 지남", "day(s) over");
                    //lbl_Remain.Text = lbl_Remain.Text.Replace("회 넘음", "time(s) over");
                    //lbl_Remain.Text = lbl_Remain.Text.Replace("만료됨", "Expired");
                    lbl_Remain.Text = TextToDraw_Remain.Replace("일 남음", "day(s) left");
                    lbl_Remain.Text = TextToDraw_Remain.Replace("회 남음", "time(s) left");
                    lbl_Remain.Text = TextToDraw_Remain.Replace("일 지남", "day(s) over");
                    lbl_Remain.Text = TextToDraw_Remain.Replace("회 넘음", "time(s) over");
                    lbl_Remain.Text = TextToDraw_Remain.Replace("만료됨", "Expired");
                    break;

                case Language.KOR:
                    btn_Set.Font = new Font("맑은 고딕", 11f, FontStyle.Bold);
                    lbl_Remain.Font = new Font("맑은 고딕", 11f, FontStyle.Bold);
                    lbl_Status.Font = new Font("맑은 고딕", 11f, FontStyle.Bold);

                    TextToDraw_Remain = TextToDraw_Remain.Replace("day(s) left", "일 남음");
                    TextToDraw_Remain = TextToDraw_Remain.Replace("time(s) left", "회 남음");
                    TextToDraw_Remain = TextToDraw_Remain.Replace("day(s) over", "일 지남");
                    TextToDraw_Remain = TextToDraw_Remain.Replace("time(s) over", "회 넘음");
                    TextToDraw_Remain = TextToDraw_Remain.Replace("Expired", "만료됨");

                    //lbl_Remain.Text = lbl_Remain.Text.Replace("day(s) left", "일 남음");
                    //lbl_Remain.Text = lbl_Remain.Text.Replace("time(s) left", "회 남음");
                    //lbl_Remain.Text = lbl_Remain.Text.Replace("day(s) over", "일 지남");
                    //lbl_Remain.Text = lbl_Remain.Text.Replace("time(s) over", "회 넘음");
                    //lbl_Remain.Text = lbl_Remain.Text.Replace("Expired", "만료됨");
                    lbl_Remain.Text = TextToDraw_Remain.Replace("day(s) left", "일 남음");
                    lbl_Remain.Text = TextToDraw_Remain.Replace("time(s) left", "회 남음");
                    lbl_Remain.Text = TextToDraw_Remain.Replace("day(s) over", "일 지남");
                    lbl_Remain.Text = TextToDraw_Remain.Replace("time(s) over", "회 넘음");
                    lbl_Remain.Text = TextToDraw_Remain.Replace("Expired", "만료됨");
                    break;
            }
            lbl_Status.Text = Texts.GetText(language, "Item_Status");
            btn_Set.Text = Texts.GetText(language, "Btn_Set");
        }

        public LifeTimeObj GetMaintInfo()
        {
            return MyInfo;
        }

        private void lbl_Remain_Paint(object sender, PaintEventArgs e)
        {
            if (MyInfo == null)
            {
                return;
            }

            Label lbl = (Label)sender;
            Size sizeOfText = TextRenderer.MeasureText(TextToDraw_Remain, lbl.Font);
            if (MyInfo.LifeTimeRate <= 0)
            {
                e.Graphics.FillRectangle(Brushes.Black, this.ClientRectangle);
                e.Graphics.DrawString(TextToDraw_Remain,
                                      lbl.Font,
                                      IsTextColorBlack == true ? Brushes.Black : Brushes.White,
                                      (lbl.Width - sizeOfText.Width) / 2,
                                      (lbl.Height - sizeOfText.Height) / 2);
            }
            else
            {
                int fillWidth_Gradient = (int)(lbl.ClientRectangle.Width * MyInfo.LifeTimeRate);
                int fillWidth_Normal = lbl.ClientRectangle.Width - fillWidth_Gradient;
                using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, fillWidth_Gradient, lbl.ClientRectangle.Height),
                                                                           lbl.BackColor,
                                                                           Color.White,
                                                                           0f))
                {
                    //e.Graphics.FillRegion(brush, new Region(new Rectangle(0, 0, fillWidth_Gradient, lbl.ClientRectangle.Height)));
                    e.Graphics.FillRectangle(brush, new Rectangle(0, 0, fillWidth_Gradient, lbl.ClientRectangle.Height));
                }
                if (fillWidth_Normal > 0)
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(fillWidth_Gradient, 0, fillWidth_Normal, lbl.ClientRectangle.Height),
                                                                               Color.White,
                                                                               Color.White,
                                                                               0f))
                    {
                        //e.Graphics.FillRegion(brush, new Region(new Rectangle(0, 0, fillWidth_Gradient, lbl.ClientRectangle.Height)));
                        e.Graphics.FillRectangle(brush, new Rectangle(fillWidth_Gradient, 0, fillWidth_Normal, lbl.ClientRectangle.Height));
                    }
                }

                switch (CurrentLanguage)
                {
                    case Language.ENG:
                        lbl_Remain.Font = new Font("Consolas", 12f, FontStyle.Bold);
                        TextToDraw_Remain = TextToDraw_Remain.Replace("일 남음", "day(s) left");
                        TextToDraw_Remain = TextToDraw_Remain.Replace("회 남음", "time(s) left");
                        TextToDraw_Remain = TextToDraw_Remain.Replace("일 지남", "day(s) over");
                        TextToDraw_Remain = TextToDraw_Remain.Replace("회 넘음", "time(s) over");
                        TextToDraw_Remain = TextToDraw_Remain.Replace("만료됨", "Expired");
                        break;

                    case Language.KOR:
                        lbl_Remain.Font = new Font("맑은 고딕", 11f, FontStyle.Bold);
                        TextToDraw_Remain = TextToDraw_Remain.Replace("day(s) left", "일 남음");
                        TextToDraw_Remain = TextToDraw_Remain.Replace("time(s) left", "회 남음");
                        TextToDraw_Remain = TextToDraw_Remain.Replace("day(s) over", "일 지남");
                        TextToDraw_Remain = TextToDraw_Remain.Replace("time(s) over", "회 넘음");
                        TextToDraw_Remain = TextToDraw_Remain.Replace("Expired", "만료됨");
                        break;
                }
                e.Graphics.DrawString(TextToDraw_Remain,
                                      lbl.Font,
                                      Brushes.Black,
                                      (lbl.Width - sizeOfText.Width) / 2,
                                      (lbl.Height - sizeOfText.Height) / 2);
            }
        }

        private void CmpVal_Since_ValueClickedEvent(object sender, object oldValue)
        {
            switch (MyInfo.Gen_Type.Value)
            {
                case LifeTimeMngType.Period:
                    string modifiyTarget = $"{MyInfo.Gen_Name.Value} - {CmpVal_Since.Prm_Name}";
                    Frm_ModifyDate frm = new Frm_ModifyDate(modifiyTarget, MyInfo.Gen_Period_Since);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        CmpVal_Since.Prm_Value = frm.NewValue;
                    }
                    break;
            }
        }

        private void CmpVal_Last_ValueClickedEvent(object sender, object oldValue)
        {
            switch (MyInfo.Gen_Type.Value)
            {
                case LifeTimeMngType.Period:
                    string modifiyTarget = $"{MyInfo.Gen_Name.Value} - {CmpVal_Last.Prm_Name}";
                    Frm_ModifyDate frm = new Frm_ModifyDate(modifiyTarget, MyInfo.Gen_Period_Last);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        CmpVal_Last.Prm_Value = frm.NewValue;
                    }
                    break;
            }
        }

        private void CmpVal_Since_ValueChangedEvent(object sender, object oldValue, object newValue)
        {
            switch (MyInfo.Gen_Type.Value)
            {
                case LifeTimeMngType.Count:
                    if ((int)newValue < 0)
                    {
                        MsgFrm_NotifyOnly msg = new MsgFrm_NotifyOnly(Texts.GetText(CurrentLanguage, "InvalidValue"));
                        msg.ShowDialog();

                        CmpVal_Since.Prm_Value = oldValue;
                    }
                    break;
            }
        }

        private void CmpVal_Last_ValueChangedEvent(object sender, object oldValue, object newValue)
        {
            switch (MyInfo.Gen_Type.Value)
            {
                case LifeTimeMngType.Count:
                    if ((int)newValue < 0)
                    {
                        MsgFrm_NotifyOnly msg = new MsgFrm_NotifyOnly(Texts.GetText(CurrentLanguage, "InvalidValue"));
                        msg.ShowDialog();

                        CmpVal_Last.Prm_Value = oldValue;
                    }
                    break;
            }
        }


        public delegate void UIInvoke_SetRate(LifeTimeObj maintInfo);
        private void SetRate(LifeTimeObj maintInfo)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new UIInvoke_SetRate(SetRate), maintInfo);
                return;
            }

            bool subjEnabled = (bool)CmpCol_Enabled.Prm_Value;
            if (subjEnabled == false)
            {
                lbl_Remain.Text = "(N/A)";
                lbl_Remain.Enabled = false;
                return;
            }
            lbl_Remain.Enabled = true;

            int percentage = (int)(maintInfo.LifeTimeRate * 100);
            string remainingDetail = string.Empty;
            string tail = string.Empty;
            int cntLeft = 0;
            switch (maintInfo.Gen_Type.Value)
            {
                case LifeTimeMngType.Period:
                    DateTime dtCurrent = DateTime.Parse(maintInfo.Gen_Period_Current.Value);
                    DateTime dtLast = DateTime.Parse(maintInfo.Gen_Period_Last.Value);
                    cntLeft = (int)((dtCurrent - dtLast).TotalDays);
                    if (cntLeft > 0)
                    {
                        tail = Texts.GetText(CurrentLanguage, "Tail_PeriodOver");
                    }
                    else if (cntLeft < 0)
                    {
                        tail = Texts.GetText(CurrentLanguage, "Tail_PeriodLeft");
                    }
                    break;

                case LifeTimeMngType.Count:
                    int nCurrent = maintInfo.Gen_Count_Current.Value;
                    int nLast = maintInfo.Gen_Count_Last.Value;
                    cntLeft = nCurrent - nLast;
                    if (cntLeft > 0)
                    {
                        tail = Texts.GetText(CurrentLanguage, "Tail_CountOver");
                    }
                    else if (cntLeft < 0)
                    {
                        tail = Texts.GetText(CurrentLanguage, "Tail_CountOver");
                    }
                    break;
            }
            remainingDetail = $"{Math.Abs(cntLeft)}{tail}";

            if (maintInfo.LifeTimeRate < 0)
            {
                TextToDraw_Remain = $"{remainingDetail}";
                lbl_Remain.BackColor = Color.Black;
                IsTextColorBlack = false;
            }
            else if (maintInfo.LifeTimeRate == 0)
            {
                TextToDraw_Remain = $"Expired";
                lbl_Remain.BackColor = Color.Black;
                IsTextColorBlack = false;
            }
            else
            {
                TextToDraw_Remain = $"{percentage}%: {remainingDetail}";
                if (percentage < 5)
                {
                    lbl_Remain.BackColor = Color.FromKnownColor(KnownColor.Black);
                    IsTextColorBlack = true;
                }
                else if (percentage < 25)
                {
                    lbl_Remain.BackColor = Color.FromKnownColor(KnownColor.Crimson);
                    IsTextColorBlack = true;
                }
                else if (percentage < 50)
                {
                    lbl_Remain.BackColor = Color.FromKnownColor(KnownColor.Gold);
                    IsTextColorBlack = true;
                }
                else
                {
                    lbl_Remain.BackColor = Color.FromKnownColor(KnownColor.MediumSeaGreen);
                    IsTextColorBlack = true;
                }
            }
            lbl_Remain.Invalidate();
            //lbl_Remain.Text = remainingDetail;
        }

        private void btn_Set_Click(object sender, EventArgs e)
        {
            bool valid = true;
            switch (MyInfo.Gen_Type.Value)
            {
                case LifeTimeMngType.Period:
                    DateTime dtSince = DateTime.Parse(MyInfo.Gen_Period_Since.Value);
                    DateTime dtCurrent = DateTime.Parse(MyInfo.Gen_Period_Current.Value);
                    DateTime dtLast = DateTime.Parse(MyInfo.Gen_Period_Last.Value);

                    if (dtSince > dtCurrent || dtCurrent > dtLast || dtSince >= dtLast)
                    {
                        valid = false;
                    }
                    break;

                case LifeTimeMngType.Count:
                    int nSince = MyInfo.Gen_Count_Since.Value;
                    int nCurrent = MyInfo.Gen_Count_Current.Value;
                    int nLast = MyInfo.Gen_Count_Last.Value;

                    if (nSince > nCurrent || nCurrent > nLast || nSince >= nLast)
                    {
                        valid = false;
                    }
                    break;
            }

            if (valid == true || MyInfo.Gen_Enabled.Value == false)
            {
                GenParams.ForEach(prm => prm.Save(true));
                CmpComps.ForEach(cmp => cmp.UpdateNamePlate());

                SetRate(MyInfo);
            }
            else
            {
                MsgFrm_NotifyOnly msgFrm = new MsgFrm_NotifyOnly(Texts.GetText(CurrentLanguage, "InvalidValue"));
                msgFrm.ShowDialog();
            }
        }

        public void UpdateStatus()
        {
            SetRate(MyInfo);
        }

        public void CheckAuthority(UserAuthority authority)
        {
            bool subjEnabled = (bool)CmpCol_Enabled.Prm_Value;
            switch (authority)
            {
                case UserAuthority.Admin:
                    CmpCol_Type.EnableModifying(subjEnabled, true);
                    CmpVal_Since.EnableModifying(subjEnabled, true);
                    CmpVal_Current.EnableModifying(subjEnabled, false);
                    CmpVal_Last.EnableModifying(subjEnabled, true);
                    chk_Select.Visible = true;
                    CmpCol_Enabled.Visible = true;
                    CmpCol_Type.Visible = true;
                    btn_Set.Visible = true;
                    tableLayoutPanel1.ColumnStyles[0].Width = 54;
                    tableLayoutPanel1.ColumnStyles[2].Width = 80;
                    tableLayoutPanel1.ColumnStyles[3].Width = 85;
                    tableLayoutPanel1.ColumnStyles[9].Width = 54;
                    break;

                case UserAuthority.Engineer:
                case UserAuthority.User:
                    CmpCol_Type.EnableModifying(subjEnabled, false);
                    CmpVal_Since.EnableModifying(subjEnabled, false);
                    CmpVal_Current.EnableModifying(subjEnabled, false);
                    CmpVal_Last.EnableModifying(subjEnabled, false);
                    chk_Select.Visible = false;
                    CmpCol_Enabled.Visible = false;
                    CmpCol_Type.Visible = false;
                    btn_Set.Visible = false;
                    tableLayoutPanel1.ColumnStyles[0].Width = 0;
                    tableLayoutPanel1.ColumnStyles[2].Width = 0;
                    tableLayoutPanel1.ColumnStyles[3].Width = 0;
                    tableLayoutPanel1.ColumnStyles[9].Width = 0;
                    break;
            }
        }

        public void SelectThis(bool select)
        {
            chk_Select.Checked = select;
        }

        private void CmpCol_Enabled_SelectedUserItemChangedEvent(object sender, object changedValue)
        {
            bool bChanged = (bool)changedValue;

            CmpCol_Type.EnableParameter(bChanged);
            CmpVal_Since.EnableParameter(bChanged);
            CmpVal_Current.EnableParameter(bChanged);
            CmpVal_Last.EnableParameter(bChanged);
            lbl_Status.BackColor = bChanged == true ? Color.LemonChiffon : Color.LightGray;
            lbl_Remain.Visible = bChanged;
            //btn_Set.Enabled = bChanged;
        }

        private void CmpCol_Type_SelectedUserItemChangedEvent(object sender, object changedValue)
        {
            LifeTimeMngType type = (LifeTimeMngType)changedValue;
            MsgFrm_AskYesNo msgFrm_Ask = new MsgFrm_AskYesNo(Texts.GetText(CurrentLanguage, "Msg_ChangeType"));
            msgFrm_Ask.ShowDialog();
            if (msgFrm_Ask.DialogResult == DialogResult.Yes)
            {
                MyInfo.Gen_Type.Set_Value(type);
                switch (type)
                {
                    case LifeTimeMngType.Period:
                        CmpVal_Since.ValueChangedEvent -= CmpVal_Since_ValueChangedEvent;
                        CmpVal_Last.ValueChangedEvent -= CmpVal_Last_ValueChangedEvent;

                        CmpVal_Since.Init(MyInfo.Gen_Period_Since, "Since");

                        CmpVal_Current.Init(MyInfo.Gen_Period_Current, "Current");
                        CmpVal_Current.EnableModifying(true, false);

                        CmpVal_Last.Init(MyInfo.Gen_Period_Last, "Last");

                        CmpVal_Since.ValueClickedEvent += CmpVal_Since_ValueClickedEvent;
                        CmpVal_Last.ValueClickedEvent += CmpVal_Last_ValueClickedEvent;

                        CmpVal_Since.UseUserKeyPad = true;
                        CmpVal_Current.UseUserKeyPad = true;
                        CmpVal_Last.UseUserKeyPad = true;

                        string sToday = DateTime.Now.ToString("yyyy-MM-dd");
                        MyInfo.Gen_Period_Since.Set_Value(sToday);
                        MyInfo.Gen_Period_Current.Set_Value(sToday);
                        MyInfo.Gen_Period_Last.Set_Value(sToday);

                        GenParams.Remove(MyInfo.Gen_Count_Since);
                        GenParams.Remove(MyInfo.Gen_Count_Current);
                        GenParams.Remove(MyInfo.Gen_Count_Last);

                        GenParams.Add(MyInfo.Gen_Period_Since);
                        GenParams.Add(MyInfo.Gen_Period_Current);
                        GenParams.Add(MyInfo.Gen_Period_Last);
                        break;

                    case LifeTimeMngType.Count:
                        CmpVal_Since.ValueClickedEvent -= CmpVal_Since_ValueClickedEvent;
                        CmpVal_Last.ValueClickedEvent -= CmpVal_Last_ValueClickedEvent;

                        CmpVal_Since.Init(MyInfo.Gen_Count_Since, "Since");

                        CmpVal_Current.Init(MyInfo.Gen_Count_Current, "Current");
                        CmpVal_Current.EnableModifying(true, false);

                        CmpVal_Last.Init(MyInfo.Gen_Count_Last, "Last");

                        CmpVal_Since.ValueChangedEvent += CmpVal_Since_ValueChangedEvent;
                        CmpVal_Last.ValueChangedEvent += CmpVal_Last_ValueChangedEvent;

                        CmpVal_Since.UseUserKeyPad = false;
                        CmpVal_Current.UseUserKeyPad = false;
                        CmpVal_Last.UseUserKeyPad = false;

                        MyInfo.Gen_Count_Since.Set_Value(0);
                        MyInfo.Gen_Count_Current.Set_Value(0);
                        MyInfo.Gen_Count_Last.Set_Value(0);

                        GenParams.Remove(MyInfo.Gen_Period_Since);
                        GenParams.Remove(MyInfo.Gen_Period_Current);
                        GenParams.Remove(MyInfo.Gen_Period_Last);

                        GenParams.Add(MyInfo.Gen_Count_Since);
                        GenParams.Add(MyInfo.Gen_Count_Current);
                        GenParams.Add(MyInfo.Gen_Count_Last);
                        break;
                }

                GenParams.ForEach(prm => prm.Save(true));
                CmpComps.ForEach(cmp => cmp.UpdateNamePlate());

                SetRate(MyInfo);
            }
            else
            {
                CmpCol_Type.SelectedUserItemChangedEvent -= CmpCol_Type_SelectedUserItemChangedEvent;
                CmpCol_Type.Prm_Value = type == LifeTimeMngType.Period ? LifeTimeMngType.Count : LifeTimeMngType.Period;
                CmpCol_Type.SelectedUserItemChangedEvent += CmpCol_Type_SelectedUserItemChangedEvent;
            }
        }

        private void chk_Select_CheckedChanged(object sender, EventArgs e)
        {
           // chk_Select.BackColor = chk_Select.Checked == true ? Color.MediumSeaGreen : Color.White;
        }

        private void lbl_Name_DoubleClick(object sender, EventArgs e)
        {
            // TBD
            //if (GlbVar.Authority != UserAuthority.Admin)
            //{
            //    return;
            //}

            //MsgFrm_AskYesNo msg = new MsgFrm_AskYesNo(string.Format(SigmaLanguage.Message.AskResetMaint, lbl_Name.Text));
            //if (msg.ShowDialog() == DialogResult.Yes)
            //{
            //    switch (MyInfo.Gen_Type.Value)
            //    {
            //        case LifeTimeMngType.Period:
            //            string sToday = DateTime.Now.ToString("yyyy-MM-dd");
            //            MyInfo.Gen_Period_Since.Set_Value(sToday, true);
            //            MyInfo.Gen_Period_Current.Set_Value(sToday, true);
            //            MyInfo.Gen_Period_Last.Set_Value(sToday, true);
            //            break;

            //        case LifeTimeMngType.Count:
            //            MyInfo.Gen_Count_Since.Set_Value(0, true);
            //            MyInfo.Gen_Count_Current.Set_Value(0, true);
            //            MyInfo.Gen_Count_Last.Set_Value(0, true);
            //            break;
            //    }
            //    UpdateStatus();
            //}
        }

        public class Texts
        {
            public static string GetText(Language language, string itemName)
            {
                string rtn = "NotDefined";
                switch (language)
                {
                    case Language.ENG:
                        switch (itemName)
                        {
                            case "Item_Enabled":
                                rtn = "Enabled";
                                break;

                            case "Item_Type":
                                rtn = "Type";
                                break;

                            case "Item_Since":
                                rtn = "Since";
                                break;

                            case "Item_Current":
                                rtn = "Current";
                                break;

                            case "Item_Last":
                                rtn = "Last";
                                break;

                            case "Item_Status":
                                rtn = "Status";
                                break;

                            case "Btn_Set":
                                rtn = "SET";
                                break;

                            case "Msg_InvalidValue":
                                rtn = "Invalid Value";
                                break;

                            case "Tail_PeriodOver":
                                rtn = "day(s) over";
                                break;

                            case "Tail_PeriodLeft":
                                rtn = "day(s) left";
                                break;

                            case "Tail_CountOver":
                                rtn = "time(s) over";
                                break;

                            case "Tail_CountLeft":
                                rtn = "time(s) left";
                                break;

                            case "Msg_ChangeType":
                                rtn = "LifeTime info will be cleard.\r\nDo you want to continue?";
                                break;
                        }
                        break;

                    case Language.KOR:
                        switch (itemName)
                        {
                            case "Item_Enabled":
                                rtn = "Enabled";
                                break;

                            case "Item_Type":
                                rtn = "Type";
                                break;

                            case "Item_Since":
                                rtn = "Since";
                                break;

                            case "Item_Current":
                                rtn = "Current";
                                break;

                            case "Item_Last":
                                rtn = "Last";
                                break;

                            case "Item_Status":
                                rtn = "Status";
                                break;

                            case "Btn_Set":
                                rtn = "SET";
                                break;

                            case "Msg_InvalidValue":
                                rtn = "Invalid Value";
                                break;

                            case "Tail_PeriodOver":
                                rtn = "일 지남";
                                break;

                            case "Tail_PeriodLeft":
                                rtn = "일 남음";
                                break;

                            case "Tail_CountOver":
                                rtn = "회 지남";
                                break;

                            case "Tail_CountLeft":
                                rtn = "회 남음";
                                break;

                            case "Msg_ChangeType":
                                rtn = "모든 기록이 초기화 됩니다.\r\n계속하겠습니까?";
                                break;
                        }
                        break;
                }
                return rtn;
            }
        }
    }
}
