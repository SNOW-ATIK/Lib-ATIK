using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATIK.Device.IO
{
    public partial class Cmp_DIO : UserControl
    {
        [Category("ATIK Properties")]
        [DisplayName("BackColor for Parameter Name")]
        public Color Color_Name
        {
            get => lbl_IOName.BackColor;
            set => lbl_IOName.BackColor = value;
        }

        [Category("ATIK Properties")]
        [DisplayName("Splitter Distance")]
        public int SplitterDistance
        {
            get => splitContainer1.SplitterDistance;
            set
            {
                int desired = value;
                if (this.Width - desired < 52)
                {
                    splitContainer1.SplitterDistance = this.Width - 52;
                }
                else
                {
                    splitContainer1.SplitterDistance = desired;
                }
            }
        }

        [Category("ATIK Properties")]
        [DisplayName("Enable UserInput")]
        public bool EnableUserInput
        {
            get;
            set;
        }

        public object ValueObject { get => ValueEditor.Tag; }

        private Element_IO MyElem;
        public IOType MyType
        {
            get
            {
                if (MyElem != null)
                {
                    return MyElem.Type;
                }
                return IOType.Unknown;
            }
        }
        private Control ValueEditor = new Control();

        public Cmp_DIO()
        {
            InitializeComponent();
        }

        public Cmp_DIO(Element_IO elem, string displayName = "")
        {
            InitializeComponent();

            Init(elem, displayName);
        }

        public void Init(Element_IO elem, string displayName = "")
        {
            MyElem = elem;

            if (displayName != "")
            {
                lbl_IOName.Text = displayName;
            }
            else
            {
                lbl_IOName.Text = MyElem.Name;
            }

            Init_Editor();
        }

        private void Init_Editor()
        {
            switch (MyElem.Type)
            {
                case IOType.DIN:
                case IOType.DOUT:
                    Button btn = new Button();
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Enabled = (MyElem.Type == IOType.DOUT);

                    ValueEditor = btn;
                    break;

                case IOType.AIN:
                case IOType.AOUT:
                    Label lbl = new Label();
                    lbl.AutoSize = false;
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    lbl.BackColor = Color.White;
                    //lbl.Enabled = (MyElem.Type == IOType.AOUT);
                    lbl.Enabled = true;
                    splitContainer1.SplitterDistance = 75;

                    ValueEditor = lbl;
                    break;
            }
            //ValueEditor.Dock = DockStyle.Fill;
            ValueEditor.Dock = DockStyle.None;
            ValueEditor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            ValueEditor.Location = new Point(0, 0);
            ValueEditor.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
            ValueEditor.Font = new Font("Consolas", 12f, FontStyle.Bold);            

            Binding binding = new Binding("Tag", MyElem, "ValueObject", true, DataSourceUpdateMode.OnPropertyChanged);
            binding.BindingComplete += Binding_BindingComplete;
            BindCompleteDel += UpdateComponentState;
            ValueEditor.DataBindings.Add(binding);            
            ValueEditor.Click += ValueEditor_Click;

            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(ValueEditor);
        }

        private delegate void BindingCompleteDelegate(BindingCompleteEventArgs e);
        private BindingCompleteDelegate BindCompleteDel;
        private void Binding_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (ValueEditor.InvokeRequired == true)
            {
                ValueEditor.Invoke(BindCompleteDel, e);
                return;
            }
            UpdateComponentState(e);
        }

        private void UpdateComponentState(BindingCompleteEventArgs e)
        {
            if (MyElem.ValueObject == null)
            {
                ValueEditor.BackColor = Color.FromKnownColor(KnownColor.LightGray);
                return;
            }
            ValueEditor.BackColor = Color.White;
            switch (MyElem.Type)
            {
                case IOType.DIN:
                case IOType.DOUT:
                    if ((bool)MyElem.ValueObject == true)
                    {
                        ValueEditor.BackColor = Color.FromKnownColor(KnownColor.MediumSeaGreen);
                        ValueEditor.Text = "ON";
                    }
                    else
                    {
                        ValueEditor.BackColor = Color.FromKnownColor(KnownColor.Crimson);
                        ValueEditor.Text = "OFF";
                    }
                    break;

                case IOType.AIN:
                case IOType.AOUT:
                    ValueEditor.Text = $"{(double)MyElem.ValueObject:0.000}";
                    break;
            }
        }

        private void ValueEditor_Click(object sender, EventArgs e)
        {
            if (MyElem.Type != IOType.DOUT && MyElem.Type != IOType.AOUT)
            {
                return;
            }
            if (EnableUserInput == false)
            {
                MsgFrm_NotifyOnly msgFrm = new MsgFrm_NotifyOnly("User input is disabled.");
                msgFrm.ShowDialog();
                return;
            }
            switch (MyElem.Type)
            {
                case IOType.DOUT:
                    (bool getSuccess, bool state) = MyElem.Get_DigitalOut();
                    if (getSuccess == true)
                    {
                        MyElem.Set_DigitalOut(!state, true);
                    }
                    break;

                case IOType.AOUT:
                    // Show KeyPad, Write New Value
                    Frm_NumPad keypad = new Frm_NumPad(MyElem.Name, MyElem.Get_AnalogOut().Value);
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        MyElem.Set_AnalogOut((double)keypad.NewValue);
                    }
                    break;

                default:
                    break;
            }
            MyElem.UpdateValueObject();
        }

        private void Cmp_IO_SizeChanged(object sender, EventArgs e)
        {
            if (this.Height < 50)
            {
                splitContainer1.SplitterDistance = 48;
            }
            else
            {
                splitContainer1.SplitterDistance = this.Height - 2;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
