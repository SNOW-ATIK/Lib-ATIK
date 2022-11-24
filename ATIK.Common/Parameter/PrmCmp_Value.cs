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
    public partial class PrmCmp_Value : UserControl, IPrmCmp
    {
        [Category("ATIK Properties")]
        [DisplayName("BackColor for Parameter Name")]
        public Color Color_Name
        {
            get => lbl_PrmName.BackColor;
            set => lbl_PrmName.BackColor = value;
        }

        [Category("ATIK Properties")]
        [DisplayName("BackColor for Parameter Value")]
        public Color Color_Value
        {
            get => ValueEditor.BackColor;
            set => ValueEditor.BackColor = value;
        }

        [Category("ATIK Properties")]
        [DisplayName("Parameter Name")]
        public string Prm_Name
        {
            get => lbl_PrmName.Text;
            set => lbl_PrmName.Text = value;
        }

        private string TmpText = string.Empty;
        [Category("ATIK Properties")]
        [DisplayName("Parameter Value")]
        public object Prm_Value
        {
            get
            {
                if (GenParam != null)
                {
                    return GenParam.ValueObject;
                }
                else
                {
                    return TmpText;
                }
            }
            set
            {
                SetValueObject(value, false);
            }
        }

        [Category("ATIK Properties")]
        [DisplayName("Value Text Align")]
        public ContentAlignment ValueTextAlign
        {
            get
            {
                if (ValueEditor != null && ValueEditor.GetType() == typeof(Label))
                {
                    Label lbl = (Label)ValueEditor;
                    return lbl.TextAlign;
                }
                throw new Exception("Can not set Alignment.");
            }
            set
            {
                if (ValueEditor != null && ValueEditor.GetType() == typeof(Label))
                {
                    Label lbl = (Label)ValueEditor;
                    lbl.TextAlign = value;
                }
            }
        }

        [Category("ATIK Properties")]
        [DisplayName("Name Text Align")]
        public ContentAlignment NameTextAlign { get => lbl_PrmName.TextAlign; set => lbl_PrmName.TextAlign = value; }

        public delegate void UIInvoke_SetValueObject(object value, bool saveDirect);
        private void SetValueObject(object value, bool saveDirect)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new UIInvoke_SetValueObject(SetValueObject), value, saveDirect);
                return;
            }

            if (GenParam != null)
            {
                GenParam.Set_ValueObject(value, saveDirect);
                if (this.Enabled == true)
                {
                    if (GenParam.ValueObject.Equals(GenParam.ValueObject_Original) == true)
                    {
                        lbl_PrmName.BackColor = Color.LemonChiffon;
                    }
                    else
                    {
                        lbl_PrmName.BackColor = Color.DarkOrange;
                    }
                }
            }

            if (value != null)
            {
                if (value.GetType() == typeof(double))
                {
                    ValueEditor.Text = ((double)value).ToString("0.###");
                }
                else
                {
                    ValueEditor.Text = value?.ToString();
                }
            }
            TmpText = value?.ToString();
        }

        private PrmCmp.PrmType _Prm_Type = PrmCmp.PrmType.Integer;
        [Category("ATIK Properties")]
        [DisplayName("Parameter Type")]
        public PrmCmp.PrmType Prm_Type
        {
            get => _Prm_Type;
            set
            {
                if (value != PrmCmp.PrmType.Integer && value != PrmCmp.PrmType.Double && value != PrmCmp.PrmType.String)
                {
                    throw new Exception("Consider Other PrmCmp Type.");
                }
                if (_Prm_Type != value)
                {
                    _Prm_Type = value;
                    if (_Prm_Type == PrmCmp.PrmType.Integer || _Prm_Type == PrmCmp.PrmType.Double)
                    {
                        string tmpText = ValueEditor.Text;
                        Font tmpFont = ValueEditor.Font;
                        ValueEditor.Dispose();
                        splitContainer1.Panel2.Controls.Clear();

                        ValueEditor = new Label();
                        Label lbl = (Label)ValueEditor;
                        lbl.BorderStyle = BorderStyle.FixedSingle;
                        switch (Orientation)
                        {
                            case Orientation.Horizontal:
                                lbl.TextAlign = ContentAlignment.MiddleCenter;
                                break;

                            case Orientation.Vertical:
                                lbl.TextAlign = ContentAlignment.MiddleRight;
                                break;
                        }
                        // 2022.08.29. Delete
                        //if (UseKeyPadUI == true)
                        //{
                        //    ValueEditor = new TextBox();
                        //}
                        //else
                        //{
                        //    ValueEditor = new Label();
                        //    Label lbl = (Label)ValueEditor;
                        //    lbl.BorderStyle = BorderStyle.FixedSingle;
                        //    switch (Orientation)
                        //    {
                        //        case Orientation.Horizontal:
                        //            lbl.TextAlign = ContentAlignment.MiddleCenter;
                        //            break;

                        //        case Orientation.Vertical:
                        //            lbl.TextAlign = ContentAlignment.MiddleRight;
                        //            break;
                        //    }
                        //}

                        if (string.IsNullOrEmpty(tmpText) == true || double.TryParse(tmpText, out double outValue) == false)
                        {
                            ValueEditor.Text = "0";
                        }
                        else
                        {
                            if (outValue.GetType() == typeof(double))
                            {
                                ValueEditor.Text = outValue.ToString("0.###");
                            }
                            else
                            {
                                ValueEditor.Text = outValue.ToString();
                            }
                        }
                    }
                    //ValueEditor.Margin = new Padding(0, 0, 0, 0);
                    ValueEditor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    ValueEditor.BackColor = Color.FromKnownColor(KnownColor.White);
                    ValueEditor.Font = new Font("Consolas", 12f, FontStyle.Bold);
                    switch (Orientation)
                    {
                        case Orientation.Horizontal:
                            ValueEditor.Location = new Point(-1, 0);
                            break;

                        case Orientation.Vertical:
                            ValueEditor.Location = new Point(-1, -1);
                            break;
                    }
                    ValueEditor.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
                    //ValueEditor.Dock = DockStyle.Fill;
                    splitContainer1.Panel2.Controls.Add(ValueEditor);
                }
            }
        }

        private bool _UseKeyPadUI = false;
        [Category("ATIK Properties")]
        [DisplayName("Enable KeyPad UI")]
        public bool UseKeyPadUI 
        {
            get => _UseKeyPadUI;
            set
            {
                if (_UseKeyPadUI != value && (Prm_Type == PrmCmp.PrmType.Integer || Prm_Type == PrmCmp.PrmType.Double || Prm_Type == PrmCmp.PrmType.String))
                {
                    _UseKeyPadUI = value;
                    this.Enabled = false;

                    string tmpText = string.Empty;
                    Font tmpFont = new Font("Consolas", 12f, FontStyle.Bold);
                    if (ValueEditor != null)
                    {
                        tmpText = ValueEditor.Text;
                        tmpFont = ValueEditor.Font;
                        ValueEditor.Dispose();
                    }

                    splitContainer1.Panel2.Controls.Clear();

                    if (value == true)
                    {
                        ValueEditor = new Label();
                        Label lbl = (Label)ValueEditor;
                        lbl.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
                        lbl.BorderStyle = BorderStyle.FixedSingle;
                        if (Orientation == Orientation.Horizontal)
                        {
                            lbl.TextAlign = ContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            lbl.TextAlign = ContentAlignment.MiddleRight;
                        }
                        ValueEditor.Click += ValueEditor_Click;
                    }
                    else
                    {
                        ValueEditor = new TextBox();
                        TextBox txt = (TextBox)ValueEditor;
                        txt.Size = new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
                        if (Orientation == Orientation.Horizontal)
                        {
                            txt.TextAlign = HorizontalAlignment.Center;
                        }
                        else
                        {
                            txt.TextAlign = HorizontalAlignment.Right;
                        }
                    }
                    ValueEditor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    ValueEditor.Font = tmpFont;

                    if (string.IsNullOrEmpty(tmpText) == true || double.TryParse(tmpText, out double outValue) == false)
                    {
                        ValueEditor.Text = "0";
                    }
                    else
                    {
                        if (outValue.GetType() == typeof(double))
                        {
                            ValueEditor.Text = outValue.ToString("0.###");
                        }
                        else
                        {
                            ValueEditor.Text = value.ToString();
                        }
                    }

                    if (GenParam != null)
                    {
                        Binding txtBind = new Binding("Text", GenParam, "ValueObject", _UseKeyPadUI, DataSourceUpdateMode.OnPropertyChanged);
                        ValueEditor.DataBindings.Add(txtBind);
                        GenParam.Set_BindingControl(this);
                    }

                    ValueEditor.BackColor = Color.FromKnownColor(KnownColor.White);
                    switch (splitContainer1.Orientation)
                    {
                        case Orientation.Horizontal:
                            lbl_PrmName.Location = new Point(-1, 0);
                            lbl_PrmName.Size = new Size(splitContainer1.Panel1.Width + 1, splitContainer1.Panel1.Height);
                            ValueEditor.Location = new Point(-1, 0);
                            break;

                        case Orientation.Vertical:
                            lbl_PrmName.Location = new Point(-1, -1);
                            //lbl_PrmName.Size = new Size(splitContainer1.Panel1.Width + 1, splitContainer1.Panel1.Height);
                            //ValueEditor.Location = new Point(-1, -1);
                            lbl_PrmName.Size = new Size(splitContainer1.Panel1.Width + 1, splitContainer1.Panel1.Height);
                            ValueEditor.Location = new Point(0, -1);
                            break;
                    }
                    splitContainer1.Panel2.Controls.Add(ValueEditor);

                    this.Enabled = true;
                }                
            }
        }

        [Category("ATIK Properties")]
        [DisplayName("Splitter Orientation")]
        public Orientation Orientation 
        {
            get => splitContainer1.Orientation;
            set
            {
                splitContainer1.Panel1MinSize = 1;
                splitContainer1.Panel2MinSize = 1;
                splitContainer1.Orientation = value;

                switch (splitContainer1.Orientation)
                {
                    case Orientation.Horizontal:
                        this.MinimumSize = new Size(PrmCmp.MIN_SPLITDISTANCE, PrmCmp.MIN_PRM_NAME_HEIGHT + PrmCmp.MIN_PRM_VALUE_HEIGHT + splitContainer1.SplitterWidth);
                        this.MaximumSize = new Size(1000, 2 * (PrmCmp.MIN_PRM_NAME_HEIGHT + PrmCmp.MIN_PRM_VALUE_HEIGHT) + splitContainer1.SplitterWidth);
                        splitContainer1.Panel1MinSize = PrmCmp.MIN_PRM_NAME_HEIGHT;
                        splitContainer1.Panel2MinSize = PrmCmp.MIN_PRM_VALUE_HEIGHT;
                        if (ValueEditor.GetType() == typeof(Label))
                        {
                            Label lbl = (Label)ValueEditor;
                            lbl.TextAlign = ContentAlignment.MiddleCenter;
                        }
                        else if (ValueEditor.GetType() == typeof(TextBox))
                        {
                        }
                        lbl_PrmName.TextAlign = ContentAlignment.MiddleCenter;
                        lbl_PrmName.Size = new Size(splitContainer1.Panel1.Width + 1, splitContainer1.Panel1.Height);
                        ValueEditor.Location = new Point(-1, 0);
                        break;

                    case Orientation.Vertical:
                        //this.MinimumSize = new Size(PrmCmp.MIN_PRM_NAME_WIDTH + PrmCmp.MIN_PRM_VALUE_WIDTH + splitContainer1.SplitterWidth, 
                        //                            PrmCmp.MIN_PRM_NAME_HEIGHT + splitContainer1.SplitterWidth);
                        this.MinimumSize = new Size(PrmCmp.MIN_SPLITDISTANCE,
                                                    PrmCmp.MIN_PRM_NAME_HEIGHT + splitContainer1.SplitterWidth);
                        this.MaximumSize = new Size(1000, 4 * PrmCmp.MIN_PRM_NAME_HEIGHT + splitContainer1.SplitterWidth);
                        splitContainer1.Panel1MinSize = PrmCmp.MIN_SPLITDISTANCE;
                        splitContainer1.Panel2MinSize = PrmCmp.MIN_SPLITDISTANCE;
                        if (ValueEditor.GetType() == typeof(Label))
                        {
                            Label lbl = (Label)ValueEditor;
                            lbl.TextAlign = ContentAlignment.MiddleCenter;
                        }
                        else if (ValueEditor.GetType() == typeof(TextBox))
                        {
                        }
                        lbl_PrmName.TextAlign = ContentAlignment.MiddleLeft;
                        lbl_PrmName.Location = new Point(-1, -1);
                        lbl_PrmName.Size = new Size(splitContainer1.Panel1.Width + 1, splitContainer1.Panel1.Height);
                        ValueEditor.Location = new Point(0, -1);
                        break;
                }

                CheckSize();
            }
        }

        [Category("ATIK Properties")]
        [DisplayName("Splitter Distance")]
        public int SplitterDistance
        {
            get => splitContainer1.SplitterDistance;
            set
            {
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        if (value < PrmCmp.MIN_PRM_NAME_HEIGHT)
                        {
                            value = PrmCmp.MIN_PRM_NAME_HEIGHT;
                        }
                        break;

                    case Orientation.Vertical:
                        if (value < PrmCmp.MIN_SPLITDISTANCE)
                        {
                            value = PrmCmp.MIN_SPLITDISTANCE;
                        }
                        break;
                }
                splitContainer1.SplitterDistance = value;
            }                
        }

        public delegate void ValueChangedEventHandler(object sender, object oldValue, object newValue);
        public event ValueChangedEventHandler ValueChangedEvent;

        public bool UseUserKeyPad { get; set; }
        public delegate void ValueClicked(object sender, object oldValue);
        public event ValueClicked ValueClickedEvent;

        public IParam GenParam { get; set; }
        private Control ValueEditor = new Control();

        private bool _EnableModifyingValue = true;
        public bool EnableModifyingValue { get => _EnableModifyingValue; private set => _EnableModifyingValue = value; }

        public PrmCmp_Value()
        {
            InitializeComponent();

            ValueEditor = lbl_PrmValueString;

            splitContainer1.Size = this.Size;
        }

        public void Init(object genPrm, string prmName, object initValue = null)
        {
            ValueEditor.DataBindings.Clear();

            GenParam = (IParam)genPrm;

            ValueEditor.DataBindings.Clear();

            Binding txtBind = new Binding("Text", GenParam, "ValueObject", UseKeyPadUI, DataSourceUpdateMode.OnPropertyChanged);
            ValueEditor.DataBindings.Add(txtBind);
            GenParam.Set_BindingControl(this);

            Prm_Name = prmName;
            if (initValue != null)
            {
                Prm_Value = initValue;
            }
            else
            {
                Prm_Value = GenParam.ValueObject;
            }
        }

        public void Init_WithOutGenPrm(string prmName, object initValue = null)
        {
            Prm_Name = prmName;
            if (initValue != null)
            {
                Prm_Value = initValue;
            }
        }

        public void ChangeLanguage_Title(Language language, string title)
        {
            switch (language)
            {
                case Language.ENG:
                    lbl_PrmName.Font = new Font("Consolas", 12f, FontStyle.Bold);
                    break;

                case Language.KOR:
                    lbl_PrmName.Font = new Font("맑은 고딕", 11f, FontStyle.Bold);
                    break;
            }
            Prm_Name = title;
        }

        public void EnableParameter(bool enb)
        {
            this.Enabled = enb;
            lbl_PrmName.BackColor = this.Enabled == true ? Color.FromKnownColor(KnownColor.LemonChiffon) : Color.FromKnownColor(KnownColor.DarkGray);
            ValueEditor.BackColor = this.Enabled == true && EnableModifyingValue == true ? Color.FromKnownColor(KnownColor.White) : Color.FromKnownColor(KnownColor.LightGray);
        }

        public void EnableModifying(bool paramEnabled, bool modifyEnabled)
        {
            if (paramEnabled == false && modifyEnabled == false)
            {
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
            }
            lbl_PrmName.BackColor = this.Enabled == true ? Color.FromKnownColor(KnownColor.LemonChiffon) : Color.FromKnownColor(KnownColor.DarkGray);

            EnableModifyingValue = modifyEnabled;
            ValueEditor.Enabled = EnableModifyingValue;
            ValueEditor.BackColor = paramEnabled == true && modifyEnabled == true ? Color.FromKnownColor(KnownColor.White) : Color.FromKnownColor(KnownColor.LightGray);
        }

        private void ValueEditor_Click(object sender, EventArgs e)
        {
            if (UseKeyPadUI == false)
            {
                return;
            }
            if (GenParam != null)
            {
                if (UseUserKeyPad == true)
                {                    
                    ValueClickedEvent?.Invoke(this, GenParam.ValueObject);
                    UpdateNamePlate();
                }
                else
                {
                    Frm_NumPad keyPad = new Frm_NumPad(Prm_Name, GenParam.ValueObject);
                    if (keyPad.ShowDialog() == DialogResult.OK)
                    {
                        GenParam.Set_ValueObject(keyPad.NewValue, false);

                        if (GenParam.ValueObject.Equals(GenParam.ValueObject_Original) == true)
                        {
                            lbl_PrmName.BackColor = Color.LemonChiffon;
                        }
                        else
                        {
                            lbl_PrmName.BackColor = Color.DarkOrange;
                            ValueChangedEvent?.Invoke(this, keyPad.OldValue, keyPad.NewValue);
                        }
                        //if (keyPad.OldValue != keyPad.NewValue)
                        //{
                        //    ValueChangedEvent?.Invoke(keyPad.OldValue, keyPad.NewValue);
                        //}
                    }
                }
            }
            else
            {
                if (UseUserKeyPad == true)
                {
                    // TBD. Form으로부터 입력받은 값을 대리자를 통해 리턴 받아서 Prm_Value 갱신 필요.
                    // (현재는 대리자에서 처리해야함. 고칠 데가 많아서 보류 중)
                    ValueClickedEvent?.Invoke(this, Prm_Value);
                    UpdateNamePlate();
                }
                else
                {
                    Frm_NumPad keyPad = new Frm_NumPad(Prm_Name, Convert.ToDouble(Prm_Value));
                    if (keyPad.ShowDialog() == DialogResult.OK)
                    {
                        ValueEditor.Text = ((double)keyPad.NewValue).ToString("0.###");
                        if (keyPad.OldValue != keyPad.NewValue)
                        {
                            Prm_Value = keyPad.NewValue;
                            ValueChangedEvent?.Invoke(this, keyPad.OldValue, keyPad.NewValue);
                        }
                    }
                }
            }
        }

        private void PrmCmp_SizeChanged(object sender, EventArgs e)
        {
            // UI Designer에서 Size 조정할때 Label이 찌그러지는 게 보기 싫어서,
            // SplitContainer의 Dock을 풀고 PrmCmp의 크기가 조정될 때마다 수동으로 Size를 할당함.
            splitContainer1.Size = this.Size;
        }

        private void CheckSize()
        {
            switch (splitContainer1.Orientation)
            {
                case Orientation.Horizontal:
                    splitContainer1.SplitterDistance = (this.Height - splitContainer1.SplitterWidth) / 2;
                    break;
            }
        }

        public void UpdateNamePlate()
        {
            if (this.Enabled == false)
            {
                return;
            }
            if (GenParam != null)
            {
                if (GenParam.ValueObject.Equals(GenParam.ValueObject_Original) == true)
                {
                    lbl_PrmName.BackColor = Color.LemonChiffon;
                }
                else
                {
                    lbl_PrmName.BackColor = Color.DarkOrange;
                }
            }
        }

        public void Restore()
        {
            if (GenParam != null)
            {
                GenParam.Set_ValueObject(GenParam.ValueObject_Original, false);
            }
            UpdateNamePlate();
        }

        private void PrmCmp_Value_EnabledChanged(object sender, EventArgs e)
        {
            // TBD. Change Background Color
        }
    }
}
