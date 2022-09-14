using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ATIK
{
    public partial class Frm_NumPad : Form
    {
        public object OldValue { get; private set; }
        public object NewValue { get; private set; }

        private Type PrmValueType;
        private bool Flag_Password = false;
        private string Hidden_Password = "";

        public Frm_NumPad(string prmName, object oldValue, bool password = false)
        {
            Flag_Password = password;

            InitializeComponent();

            this.SetTopLevel(true);

            lbl_PrmName.Text = prmName;
            if (Flag_Password == true)
            {
                lbl_Old.Text = "";
                lbl_New.Text = "";
                btn_Dot.Enabled = false;
                btn_Plus.Enabled = false;
                btn_Minus.Enabled = false;
                PrmValueType = typeof(int);
            }
            else
            {
                OldValue = oldValue;
                lbl_Old.Text = OldValue.ToString();

                PrmValueType = OldValue.GetType();
                NewValue = OldValue;
                lbl_New.Text = NewValue.ToString();

                if (PrmValueType == typeof(double) || PrmValueType == typeof(float))
                {
                    btn_Dot.Enabled = true;
                }
                else
                {
                    btn_Dot.Enabled = false;
                }
            }
        }

        private void BuildNumber(string number)
        {
            if (lbl_New.Text.EndsWith(".") == true)
            {
                lbl_New.Text += number;
            }
            else
            {
                string targetText = "";
                if (Flag_Password == true)
                {
                    targetText = Hidden_Password;
                }
                else
                {
                    targetText = lbl_New.Text;
                }

                if (double.TryParse(targetText, out double dCheckZero) == true)
                {
                    if (Flag_Password == true)
                    {
                        if (dCheckZero == 0)
                        {
                            lbl_New.Text = "";
                            Hidden_Password = number;
                        }
                        else
                        {
                            lbl_New.Text += "#";
                            Hidden_Password += number;
                        }
                    }
                    else
                    {
                        if (lbl_New.Text.Contains(".") == true)
                        {
                            lbl_New.Text += number;
                        }
                        else
                        {
                            if (dCheckZero == 0)
                            {
                                lbl_New.Text = number;
                            }
                            else
                            {
                                lbl_New.Text += number;
                            }
                        }
                    }
                }
                else
                {
                    if (Flag_Password == true)
                    {
                        lbl_New.Text += "#";
                        Hidden_Password += number;
                    }
                }
            }
        }

        private void AddDot()
        {
            if (lbl_New.Text.Contains(".") == true)
            {
                return;
            }
            else
            {
                lbl_New.Text += ".";
            }
        }

        private void AddSign(bool positive)
        {
            lbl_New.Text = lbl_New.Text.Replace("+", "");
            lbl_New.Text = lbl_New.Text.Replace("-", "");
            if (positive == false)
            {
                lbl_New.Text = "-" + lbl_New.Text;
            }
        }

        private void Frm_KeyPad_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad0:
                case Keys.D0:
                    btn_No_0.PerformClick();
                    break;

                case Keys.D1:
                    btn_No_1.PerformClick();
                    break;

                case Keys.D2:
                    btn_No_2.PerformClick();
                    break;

                case Keys.D3:
                    btn_No_3.PerformClick();
                    break;

                case Keys.D4:
                    btn_No_4.PerformClick();
                    break;

                case Keys.D5:
                    btn_No_5.PerformClick();
                    break;

                case Keys.D6:
                    btn_No_6.PerformClick();
                    break;

                case Keys.D7:
                    btn_No_7.PerformClick();
                    break;

                case Keys.D8:
                    btn_No_8.PerformClick();
                    break;

                case Keys.D9:
                    btn_No_9.PerformClick();
                    break;

                case Keys.Decimal:
                case Keys.OemPeriod:
                    btn_Dot.PerformClick();
                    break;

                case Keys.Add:
                    btn_Plus.PerformClick();
                    break;

                case Keys.Oemplus:
                    if (e.Shift == true)
                    {
                        btn_Plus.PerformClick();
                    }
                    break;

                case Keys.Subtract:
                case Keys.OemMinus:
                    btn_Minus.PerformClick();
                    break;

                case Keys.Delete:
                    btn_Clear.PerformClick();
                    break;

                case Keys.Back:
                    btn_BackSpace.PerformClick();
                    break;

                case Keys.Escape:
                    btn_Close.PerformClick();
                    break;

                case Keys.Enter:
                    btn_Apply.PerformClick();
                    break;
            }
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            string targetText = "";
            if (Flag_Password == true)
            {
                targetText = Hidden_Password;
            }
            else
            {
                targetText = lbl_New.Text;
            }
            if (string.IsNullOrEmpty(targetText) == true)
            {
                // #. Invalid => ShowMsgBox
                return;
            }
            bool validApply = false;

            // Parse New Value
            bool parseSuccess = false;
            var tryParseMethod = PrmValueType.GetMethod("TryParse",
                                              BindingFlags.Static | BindingFlags.Public,
                                              null,
                                              new Type[] { typeof(string), PrmValueType.MakeByRefType() },
                                              null);
            object[] invokeParams = new object[] { targetText, null };
            parseSuccess = (bool)tryParseMethod.Invoke(null, invokeParams);
            if (parseSuccess == true)
            {
                NewValue = invokeParams[1];
                validApply = true;
            }

            if (validApply == true)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // #. Invalid => ShowMsgBox
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Number_Click(object sender, EventArgs e)
        {
            BuildNumber(((Button)sender).Text);
        }

        private void btn_Dot_Click(object sender, EventArgs e)
        {
            AddDot();
        }

        private void btn_Plus_Click(object sender, EventArgs e)
        {
            AddSign(true);
        }

        private void btn_Minus_Click(object sender, EventArgs e)
        {
            AddSign(false);
        }

        private void btn_BackSpace_Click(object sender, EventArgs e)
        {
            string targetText = "";
            if (lbl_New.Text.Length < 1)
            {
                return;
            }

            if (Flag_Password == true)
            {
                targetText = Hidden_Password;
            }
            else
            {
                targetText = lbl_New.Text;
            }
            if (double.TryParse(targetText, out double dCheckZero) == true && dCheckZero == 0)
            {
                if (Flag_Password == true)
                {
                    lbl_New.Text = "";
                    Hidden_Password = "";
                }
                else
                {
                    lbl_New.Text = "0";
                }
            }
            else
            {
                if (Flag_Password == true)
                {
                    lbl_New.Text = lbl_New.Text.Substring(0, lbl_New.Text.Length - 1);
                    Hidden_Password = Hidden_Password.Substring(0, Hidden_Password.Length- 1);
                }
                else
                {
                    lbl_New.Text = lbl_New.Text.Substring(0, lbl_New.Text.Length - 1);
                }

                if (lbl_New.Text.Length == 0 || lbl_New.Text == "-")
                {
                    if (Flag_Password == true)
                    {
                        lbl_New.Text = "";
                        Hidden_Password = "";
                    }
                    else
                    {
                        lbl_New.Text = "0";
                    }
                }
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            if (Flag_Password == true)
            {
                lbl_New.Text = "";
                Hidden_Password = "";
            }
            else
            {
                lbl_New.Text = "0";
            }
        }

        private void lbl_PrmName_MouseHover(object sender, EventArgs e)
        {
            Handle_UI.FormMove_DragDrop.Object_MouseHover(this);
        }

        private void lbl_PrmName_MouseLeave(object sender, EventArgs e)
        {
            Handle_UI.FormMove_DragDrop.Object_MouseLeave(this);
        }

        private void lbl_PrmName_MouseDown(object sender, MouseEventArgs e)
        {
            Handle_UI.FormMove_DragDrop.Object_MouseDown(e);
        }

        private void lbl_PrmName_MouseMove(object sender, MouseEventArgs e)
        {
            Handle_UI.FormMove_DragDrop.Object_MouseMove(this, e);
        }

        private void lbl_PrmName_DoubleClick(object sender, EventArgs e)
        {
            Handle_UI.FormMove_DragDrop.Object_DoubleClickedToCenter(this);
        }
    }
}
