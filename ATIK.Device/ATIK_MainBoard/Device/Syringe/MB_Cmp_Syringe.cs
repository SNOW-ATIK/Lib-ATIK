using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ATIK;

namespace ATIK.Device.ATIK_MainBoard
{
    public partial class MB_Cmp_Syringe : UserControl
    {
        private MB_Elem_Syringe MySyringe;
        public string LogicalName
        {
            get
            {
                if (MySyringe != null)
                {
                    return MySyringe.LogicalName;
                }
                return "";
            }
        }
        private bool CmpForControl = true;

        public delegate void SyringeConditionChanged(MB_Elem_Syringe elem, MB_SyringeFlow flow, MB_SyringeDirection dir, int speed, double vol_mL);
        public event SyringeConditionChanged SyringeConditionChangedEvent;

        public MB_Cmp_Syringe()
        {
            InitializeComponent();
        }

        public MB_Cmp_Syringe(MB_Elem_Syringe syringe, bool cmpForControl = true)
        {
            InitializeComponent();

            SetElem(syringe, cmpForControl);
        }

        public void SetElem(MB_Elem_Syringe syringe, bool cmpForControl = true)
        {
            MySyringe = syringe;
            CmpForControl = cmpForControl;

            lbl_Name.Text = MySyringe.LogicalName;
            if (MySyringe.ExtDirection_Enabled == false)
            {
                tbl_Direction.ColumnStyles[0].Width = 50;
                tbl_Direction.ColumnStyles[1].Width = 50;
                tbl_Direction.ColumnStyles[2].Width = 0;
            }

            if (cmpForControl == true)
            {
                tbl_MainLayout.RowStyles[tbl_MainLayout.RowCount - 2].Height = 4;
                tbl_MainLayout.RowStyles[tbl_MainLayout.RowCount - 1].Height = 42;

                tbl_InjVol.ColumnStyles[0].Width = 50;
                tbl_InjVol.ColumnStyles[1].Width = 50;
            }
            else
            {
                tbl_MainLayout.RowStyles[tbl_MainLayout.RowCount - 2].Height = 0;
                tbl_MainLayout.RowStyles[tbl_MainLayout.RowCount - 1].Height = 0;

                tbl_InjVol.ColumnStyles[0].Width = 100;
                tbl_InjVol.ColumnStyles[1].Width = 0;
            }
        }

        public void UpdateState()
        {
            if (CmpForControl == false)
            {
                throw new InvalidOperationException();
            }
            var PositionRtn = MySyringe.Get_Volume_mL();
            if (PositionRtn.IsValid == true)
            {
                lbl_CurrentPosition.Text = PositionRtn.Volume_mL.ToString("0.00");
                Application.DoEvents();
            }
        }

        public void Set_Unknown()
        {
            if (CmpForControl == true)
            {
                throw new InvalidOperationException();
            }

            chk_Pick.Checked = false;
            chk_Dispense.Checked = false;

            chk_In.Checked = false;
            chk_Out.Checked = false;
            chk_Ext.Checked = false;

            btn_SetSpeed.Text = "-";
            btn_SetInjectionVolume.Text = "-";

            chk_Pick.BackColor = Color.DarkGray;
            chk_Dispense.BackColor = Color.DarkGray;
            chk_In.BackColor = Color.DarkGray;
            chk_Out.BackColor = Color.DarkGray;
            chk_Ext.BackColor = Color.DarkGray;
            btn_SetSpeed.BackColor = Color.DarkGray;
            btn_SetInjectionVolume.BackColor = Color.DarkGray;
        }

        public void Enable_Setting()
        {
            if (chk_Pick.Checked == false)
            {
                chk_Pick.BackColor = Color.White;
            }
            if (chk_Dispense.Checked == false)
            {
                chk_Dispense.BackColor = Color.White;
            }

            if (chk_In.Checked == false)
            {
                chk_In.BackColor = Color.White;
            }
            if (chk_Out.Checked == false)
            {
                chk_Out.BackColor = Color.White;
            }
            if (chk_Ext.Checked == false)
            {
                chk_Ext.BackColor = Color.White;
            }

            if (btn_SetSpeed.Text == "-")
            {
                btn_SetSpeed.BackColor = Color.White;
            }
            if (btn_SetInjectionVolume.Text == "-")
            {
                btn_SetInjectionVolume.BackColor = Color.White;
            }

            int nFlow = tbl_Flow.Controls.OfType<CheckBox>().Count(chk => chk.Checked == true);
            int nDir = tbl_Direction.Controls.OfType<CheckBox>().Count(chk => chk.Checked == true);
            bool bSpeed = btn_SetSpeed.Text != "-";
            bool bVol = btn_SetInjectionVolume.Text != "-";
            if (nFlow == 0 && nDir == 0 && bSpeed == false && bVol == false)
            {
                chk_Pick.BackColor = Color.DarkGray;
                chk_Dispense.BackColor = Color.DarkGray;
                chk_In.BackColor = Color.DarkGray;
                chk_Out.BackColor = Color.DarkGray;
                chk_Ext.BackColor = Color.DarkGray;
                btn_SetSpeed.BackColor = Color.DarkGray;
                btn_SetInjectionVolume.BackColor = Color.DarkGray;
            }
        }

        public void Set_State(MB_SyringeFlow flow, MB_SyringeDirection dir, int speed, double injVol_mL)
        {
            switch (flow)
            {
                case MB_SyringeFlow.Pick:
                    chk_Pick.Checked = true;
                    break;

                case MB_SyringeFlow.Dispense:
                    chk_Dispense.Checked = true;
                    break;

                case MB_SyringeFlow.None:
                    chk_Pick.Checked = false;
                    chk_Dispense.Checked = false;
                    break;
            }

            switch (dir)
            {
                case MB_SyringeDirection.In:
                    chk_In.Checked = true;
                    break;

                case MB_SyringeDirection.Out:
                    chk_Out.Checked = true;
                    break;

                case MB_SyringeDirection.Ext:
                    chk_Ext.Checked = true;
                    break;

                case MB_SyringeDirection.None:
                    chk_In.Checked = false;
                    chk_Out.Checked = false;
                    chk_Ext.Checked = false;
                    break;
            }


            if (CmpForControl == true)
            {
                btn_SetSpeed.Text = speed.ToString();
                btn_SetInjectionVolume.Text = injVol_mL.ToString("0.00");
            }
            else
            {
                int nFlow = tbl_Flow.Controls.OfType<CheckBox>().Count(chk => chk.Checked == true);
                if (nFlow == 0)
                {
                    chk_Pick.BackColor = Color.DarkGray;
                    chk_Dispense.BackColor = Color.DarkGray;
                }
                int nDir = tbl_Direction.Controls.OfType<CheckBox>().Count(chk => chk.Checked == true);
                if (nDir == 0)
                {
                    chk_In.BackColor = Color.DarkGray;
                    chk_Out.BackColor = Color.DarkGray;
                    chk_Ext.BackColor = Color.DarkGray;
                }
                btn_SetSpeed.Text = speed > 0 ? speed.ToString() : "-";
                btn_SetSpeed.BackColor = speed > 0 ? Color.MediumSeaGreen : Color.DarkGray;
                btn_SetInjectionVolume.Text = injVol_mL > 0? injVol_mL.ToString("0.00") : "-";
                btn_SetInjectionVolume.BackColor = injVol_mL > 0 ? Color.MediumSeaGreen : Color.DarkGray;
            }
        }

        private void SelectedFlowChanged(object sender, EventArgs e)
        {
            CheckBox clicked = (CheckBox)sender;
            if (clicked.Checked == true)
            {
                if (clicked == chk_Pick)
                {
                    chk_Dispense.Checked = !chk_Pick.Checked;
                }
                else
                {
                    chk_Pick.Checked = !chk_Dispense.Checked;
                }
            }

            if (CmpForControl == false)
            {
                Enable_Setting();

                NotifyCondition();
            }
        }

        private void SelectedDirectionChanged(object sender, EventArgs e)
        {
            CheckBox clicked = (CheckBox)sender;
            if (clicked.Checked == true)
            {
                if (clicked == chk_In)
                {
                    chk_Out.Checked = !clicked.Checked;
                    chk_Ext.Checked = !clicked.Checked;
                }
                else if (clicked == chk_Out)
                {
                    chk_In.Checked = !clicked.Checked;
                    chk_Ext.Checked = !clicked.Checked;
                }
                else //if (clicked == chk_Ext)
                {
                    chk_In.Checked = !clicked.Checked;
                    chk_Out.Checked = !clicked.Checked;
                }
            }

            if (CmpForControl == false)
            {
                Enable_Setting();

                NotifyCondition();
            }
        }

        private void btn_SetSpeed_Click(object sender, EventArgs e)
        {
            int old = CmpForControl == true ? int.Parse(btn_SetSpeed.Text) : 0;
            Frm_NumPad numPad = new Frm_NumPad("Syringe Speed", old);
            if (numPad.ShowDialog() == DialogResult.OK)
            {
                int setVal = (int)numPad.NewValue;

                if (setVal <= 0 || setVal > MySyringe.MaxSpeed)
                {
                    string msg = "";
                    if (CmpForControl == true)
                    {
                        msg = "Invalid Speed";
                    }
                    else
                    {
                        msg = $"{MySyringe.LogicalName} won't move in this step.";

                        btn_SetSpeed.Text = "-";
                        btn_SetSpeed.BackColor = Color.DarkGray;
                    }

                    MsgFrm_NotifyOnly msgFrm = new MsgFrm_NotifyOnly(msg);
                    msgFrm.ShowDialog();

                    Enable_Setting();

                    NotifyCondition();

                    return;
                }

                btn_SetSpeed.Text = ((int)numPad.NewValue).ToString();
                if (CmpForControl == false)
                {
                    Enable_Setting();
                    btn_SetSpeed.BackColor = Color.MediumSeaGreen;

                    NotifyCondition();
                }
            }
        }

        private void btn_SetInjectionVolume_Click(object sender, EventArgs e)
        {
            double old = CmpForControl == true ? double.Parse(btn_SetInjectionVolume.Text) : 0;
            Frm_NumPad numPad = new Frm_NumPad("Injection Volume", old);
            if (numPad.ShowDialog() == DialogResult.OK)
            {
                double setVal = (double)numPad.NewValue;
                double maxVol_Unit = MySyringe.MaxVolume_mL;
                
                if (setVal <= 0 || setVal > maxVol_Unit)
                {
                    string msg = "";
                    if (CmpForControl == true)
                    {
                        msg = "Invalid Volume";
                    }
                    else
                    {
                        msg = $"{MySyringe.LogicalName} won't move in this step.";

                        btn_SetInjectionVolume.Text = "-";
                        btn_SetSpeed.BackColor = Color.DarkGray;
                    }
                    
                    MsgFrm_NotifyOnly msgFrm = new MsgFrm_NotifyOnly(msg);
                    msgFrm.ShowDialog();

                    Enable_Setting();

                    NotifyCondition();

                    return;
                }
                btn_SetInjectionVolume.Text = ((double)numPad.NewValue).ToString();
                if (CmpForControl == false)
                {
                    Enable_Setting();
                    btn_SetInjectionVolume.BackColor = Color.MediumSeaGreen;

                    NotifyCondition();
                }
            }
        }

        private void NotifyCondition()
        {
            if (SyringeConditionChangedEvent == null)
            {
                return;
            }
            bool bFlow = chk_Pick.Checked || chk_Dispense.Checked;
            bool bDir = chk_In.Checked || chk_Out.Checked || chk_Ext.Checked;
            bool bSpeed = int.TryParse(btn_SetSpeed.Text, out int speed) == true && speed > 0;
            bool bVol = double.TryParse(btn_SetInjectionVolume.Text, out double vol_mL) == true && vol_mL > 0;
            if (bFlow == true && bDir == true && bSpeed == true && bVol == true)
            {
                MB_SyringeFlow flow = chk_Pick.Checked == true ? MB_SyringeFlow.Pick : MB_SyringeFlow.Dispense;
                MB_SyringeDirection dir = MB_SyringeDirection.In;
                if (chk_In.Checked == true)
                {
                    dir = MB_SyringeDirection.In;
                }
                else if (chk_Out.Checked == true)
                {
                    dir = MB_SyringeDirection.Out;
                }
                else if (chk_Ext.Checked == true)
                {
                    dir = MB_SyringeDirection.Ext;
                }

                SyringeConditionChangedEvent(MySyringe, flow, dir, speed, vol_mL);
            }
            else if (bFlow == false && bDir == false && bSpeed == false && bVol == false)
            {
                SyringeConditionChangedEvent(MySyringe, MB_SyringeFlow.None, MB_SyringeDirection.None, 0, 0);
            }
            else
            { 
            }
        }

        private void btn_Run_Click(object sender, EventArgs e)
        {
            if (chk_Pick.Checked == false && chk_Dispense.Checked == false)
            {
                MsgFrm_NotifyOnly msg = new MsgFrm_NotifyOnly("Select Flow");
                msg.ShowDialog();
                return;
            }
            if (chk_In.Checked == false && chk_Out.Checked == false && chk_Ext.Checked == false)
            {
                MsgFrm_NotifyOnly msg = new MsgFrm_NotifyOnly("Select Direction");
                msg.ShowDialog();
                return;
            }
            if (int.TryParse(btn_SetSpeed.Text, out int speed) == true)
            {
                if (speed <= 0 || speed > MySyringe.MaxSpeed)
                {
                    MsgFrm_NotifyOnly msg = new MsgFrm_NotifyOnly("Invalid Speed");
                    msg.ShowDialog();
                    return;
                }
            }
            else
            {
                MsgFrm_NotifyOnly msg = new MsgFrm_NotifyOnly("Invalid Speed");
                msg.ShowDialog();
                return;
            }
            if (double.TryParse(btn_SetInjectionVolume.Text, out double volume) == true)
            {
                
                if (volume <= 0 || volume > MySyringe.MaxVolume_mL)
                {
                    MsgFrm_NotifyOnly msg = new MsgFrm_NotifyOnly("Invalid Volume");
                    msg.ShowDialog();
                    return;
                }
            }
            else
            {
                MsgFrm_NotifyOnly msg = new MsgFrm_NotifyOnly("Invalid Volume");
                msg.ShowDialog();
                return;
            }

            MB_SyringeFlow flow = chk_Pick.Checked == true ? MB_SyringeFlow.Pick : MB_SyringeFlow.Dispense;
            MB_SyringeDirection dir = MB_SyringeDirection.In;
            if (chk_Out.Checked == true)
            {
                dir = MB_SyringeDirection.Out;
            }
            else if (chk_Ext.Checked == true)
            {
                dir = MB_SyringeDirection.Ext;
            }

            MySyringe.Run_mL(flow, dir, volume, speed);

            //btn_SetSpeed.Text = "0";
            btn_SetInjectionVolume.Text = "0";
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            var rtn = MySyringe.Get_Volume_mL();
            if (rtn.IsValid == true)
            {
                MySyringe.Run_mL(MB_SyringeFlow.Dispense, MB_SyringeDirection.Out, rtn.Volume_mL, 15);
            }
        }
    }
}
