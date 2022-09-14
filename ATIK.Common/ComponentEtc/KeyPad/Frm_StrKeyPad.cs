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
    public partial class Frm_StrKeyPad : Form
    {
        public string OldValue { get; private set; }
        public string NewValue { get; private set; }

        private bool Flag_SpecialKeysEnabled = false;
        private bool Flag_Password = false;

        private List<KeyPadButton> AllKeys;
        private List<KeyPadButton> ShiftKeys;

        public Frm_StrKeyPad(string valName, string oldValue, bool enbSpecialKeys = false, bool password = false)
        {
            InitializeComponent();

            this.SetTopLevel(true);

            lbl_Name.Text = valName;
            txt_Value.Text = OldValue;
            Flag_SpecialKeysEnabled = enbSpecialKeys;
            Flag_Password = password;

            OldValue = oldValue;            
        }

        private void Key_CheckedChangedEvent(object sender, EventArgs e)
        {
            KeyPadButton keyBtn = (KeyPadButton)sender;
            if (keyBtn.KeyText_Current == "Shift")
            {
                if (ShiftKeys[0].CheckedState != ShiftKeys[1].CheckedState)
                {
                    AllKeys.ForEach(key =>
                    {
                        if (Flag_SpecialKeysEnabled == true)
                        {
                            key.ShowShiftedKey(keyBtn.CheckedState);
                        }
                        else
                        {
                            if (key.IsSpecialSymbol_Shift == true)
                            {
                                key.ShowShiftedKey(false);
                            }
                            else
                            {
                                key.ShowShiftedKey(keyBtn.CheckedState);
                            }
                        }
                    });

                    if (keyBtn == ShiftKeys[0])
                    {
                        ShiftKeys[1].PerformClick();
                    }
                    else // if (keyBtn == shiftKeys[1])
                    {
                        ShiftKeys[0].PerformClick();
                    }
                }
                return;
            }
            else if (keyBtn.KeyText_Current == "Caps Lock")
            {
                AllKeys.ForEach(key =>
                {
                    key.ShowShiftedKey(keyBtn.CheckedState);
                });
                return;
            }
        }

        private void Key_KeyClickedEvent(object sender, EventArgs e)
        {
            KeyPadButton keyBtn = (KeyPadButton)sender;

            if (keyBtn.KeyForFunction == true)
            {
            }
            else
            {
                if (keyBtn.KeyText_Current == "←")
                {
                    if (txt_Value.Text != "")
                    {
                        txt_Value.Text = txt_Value.Text.Substring(0, txt_Value.Text.Length - 1);
                    }
                }
                else if (keyBtn.KeyText_Current == "␣")
                {
                    if (txt_Value.Text != "")
                    {
                        txt_Value.Text += " ";
                    }
                }
                else
                {
                    txt_Value.Text += keyBtn.KeyText_Current;
                }

                if (txt_Value.Text.Trim() == "")
                {
                    txt_Value.Text = "";
                }
            }

            if (ShiftKeys[0].CheckedState == true)
            {
                ShiftKeys[0].PerformClick();
            }            
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Value.Text = "";
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            NewValue = txt_Value.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            NewValue = OldValue;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Frm_StrKeyPad_Load(object sender, EventArgs e)
        {
            AllKeys = Handle_UI.GetAllControlsRecursive(tableLayoutPanel3).OfType<KeyPadButton>().ToList();
            AllKeys.ForEach(key =>
            {
                if (key.BehaviorAsCheckBox == true)
                {
                    key.CheckedChangedEvent += Key_CheckedChangedEvent;
                }
                else
                {
                    key.KeyClickedEvent += Key_KeyClickedEvent;
                }
            });
            ShiftKeys = AllKeys.Where(key => key.KeyText_Current == "Shift").ToList();
        }
    }
}
