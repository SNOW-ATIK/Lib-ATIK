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
    public partial class KeyPadButton : UserControl
    {
        public string KeyText_Normal { get; set; }
        public string KeyText_Shift { get; set; }
        public string KeyText_Current { get => KeyButton.Text; }

        public Font KeyFont { get => KeyButton.Font; set => KeyButton.Font = value; }
        public bool KeyEnabled { get => KeyButton.Enabled; set => KeyButton.Enabled = value; }
        
        public bool KeyForFunction { get; set; }
        public bool IsSpecialSymbol_Normal { get; set; }
        public bool IsSpecialSymbol_Shift { get; set; }

        public bool BehaviorAsCheckBox { get; set; }
        public bool CheckedState { get; set; }

        public delegate void KeyClicked(object sender, EventArgs e);
        public event KeyClicked KeyClickedEvent;

        public delegate void CheckedChanged(object sender, EventArgs e);
        public event CheckedChanged CheckedChangedEvent;

        public KeyPadButton()
        {
            InitializeComponent();

            KeyButton.Click += KeyButton_Click;
            KeyButton.EnabledChanged += KeyButton_EnabledChanged;
        }

        public void PerformClick()
        {
            KeyButton.PerformClick();
        }

        private void KeyPadButton_Load(object sender, EventArgs e)
        {
            KeyButton.Text = KeyText_Normal;
        }

        public void ShowShiftedKey(bool shift)
        {
            KeyButton.Text = shift == true ? KeyText_Shift : KeyText_Normal;
        }

        private void KeyButton_EnabledChanged(object sender, EventArgs e)
        {
            KeyButton.BackColor = KeyButton.Enabled == true ? Color.White : Color.DarkGray;
        }

        private void KeyButton_Click(object sender, EventArgs e)
        {
            if (BehaviorAsCheckBox == true)
            {
                CheckedState = !CheckedState;
                KeyButton.BackColor = CheckedState == true ? Color.DeepSkyBlue : Color.White;
                CheckedChangedEvent?.Invoke(this, e);
            }
            else
            {
                KeyClickedEvent?.Invoke(this, e);
            }
        }
    }
}
