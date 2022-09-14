using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ATIK
{
    [DefaultEvent("SelectedUserItemChangedEvent")]
    public partial class PrmCmp_Collection : UserControl, IPrmCmp
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
            get => cmb_Values.BackColor;
            set => cmb_Values.BackColor = value;
        }

        [Category("ATIK Properties")]
        [DisplayName("Parameter Name")]
        public string Prm_Name
        {
            get => lbl_PrmName.Text;
            set => lbl_PrmName.Text = value;
        }

        [Category("ATIK Properties")]
        [DisplayName("Parameter Value")]
        public object Prm_Value
        {
            get => cmb_Values.SelectedIndex > -1? cmb_Values.SelectedItem : null;
            set
            {
                if (value == null)
                {
                    return;
                }
                if (cmb_Values.Items.Contains(value) == true)
                {
                    cmb_Values.SelectedItem = value;
                }
                else
                {
                    cmb_Values.SelectedIndex = -1;
                }
            }
        }

        private PrmCmp.PrmType _Prm_Type = PrmCmp.PrmType.Boolean;
        [Category("ATIK Properties")]
        [DisplayName("Parameter Type")]
        public PrmCmp.PrmType Prm_Type
        {
            get => _Prm_Type;
            set
            {
                if (_Prm_Type != value)
                {
                    if (value != PrmCmp.PrmType.Boolean && value != PrmCmp.PrmType.UserCollection)
                    {
                        throw new Exception("Consider Other PrmCmp Type.");
                    }

                    _Prm_Type = value;
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
                        lbl_PrmName.TextAlign = ContentAlignment.MiddleCenter;
                        cmb_Values.Location = new Point(-1, 0);
                        break;

                    case Orientation.Vertical:
                        this.MinimumSize = new Size(PrmCmp.MIN_SPLITDISTANCE, 
                                                    PrmCmp.MIN_PRM_NAME_HEIGHT + splitContainer1.SplitterWidth);
                        this.MaximumSize = new Size(1000, 2 * PrmCmp.MIN_PRM_NAME_HEIGHT + splitContainer1.SplitterWidth);
                        splitContainer1.Panel1MinSize = PrmCmp.MIN_SPLITDISTANCE;
                        splitContainer1.Panel2MinSize = PrmCmp.MIN_SPLITDISTANCE;
                        lbl_PrmName.TextAlign = ContentAlignment.MiddleLeft;
                        cmb_Values.Location = new Point(0, -1);
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

        public delegate void SelectedUserItemChangedEventHandler(object sender, object changedValue);
        [Browsable(true)]
        public event SelectedUserItemChangedEventHandler SelectedUserItemChangedEvent;

        public IParam GenParam { get; set; }
        private Array Collection;

        public PrmCmp_Collection()
        {
            InitializeComponent();

            splitContainer1.Size = this.Size;
        }

        public bool Init(object genPrm, string prmName, Array collection = null, object initValue = null)
        {
            cmb_Values.SelectedIndexChanged -= Cmb_Values_SelectedIndexChanged;
            cmb_Values.DataBindings.Clear();

            GenParam = (IParam)genPrm;

            if (GenParam == null)
            {
                if (collection == null)
                {
                    List<bool> lst_Bool = new List<bool>();
                    lst_Bool.Add(false);
                    lst_Bool.Add(true);
                    Collection = lst_Bool.ToArray();
                }
                else
                {
                    Collection = collection;
                }
            }
            else
            {
                if (GenParam.GetType() == typeof(bool))
                {
                    if (collection == null)
                    {
                        List<bool> lst_Bool = new List<bool>();
                        lst_Bool.Add(false);
                        lst_Bool.Add(true);
                        Collection = lst_Bool.ToArray();
                    }
                    else
                    {
                        Collection = collection;
                    }
                }
                else
                {
                    Collection = collection;
                }
            }

            cmb_Values.DataSource = Collection;

            if (genPrm != null)
            {
                Binding itemBind = new Binding("SelectedItem", GenParam, "ValueObject", true, DataSourceUpdateMode.OnPropertyChanged);
                cmb_Values.DataBindings.Add(itemBind);
            }

            Prm_Name = prmName;
            if (initValue != null)
            {
                Prm_Value = initValue;
            }
            else
            {
                if (GenParam != null)
                {
                    Prm_Value = GenParam.ValueObject;
                }
            }

            cmb_Values.SelectedIndexChanged += Cmb_Values_SelectedIndexChanged;

            //SelectedUserItemChangedEvent?.Invoke(this, cmb_Values.SelectedItem);

            return true;
        }

        public void Init_WithOutGenPrm(string prmName, Array collection, object initValue = null)
        {
            Prm_Name = prmName;
            Collection = collection;

            cmb_Values.SelectedIndexChanged -= Cmb_Values_SelectedIndexChanged;

            cmb_Values.DataSource = Collection;
            cmb_Values.SelectedIndexChanged += Cmb_Values_SelectedIndexChanged;
            if (initValue != null)
            {
                Prm_Value = initValue;
            }
            SelectedUserItemChangedEvent?.Invoke(this, cmb_Values.SelectedItem);
        }

        public void ChangeLanguage_Title(string language, string title)
        {
            if (language == "ENG")
            {
                lbl_PrmName.Font = new Font("Consolas", 12f, FontStyle.Bold);
            }
            else if (language == "KOR")
            {
                lbl_PrmName.Font = new Font("맑은 고딕", 11f, FontStyle.Bold);
            }
            Prm_Name = title;
        }

        public void EnableParameter(bool enb)
        {
            this.Enabled = enb;

            lbl_PrmName.BackColor = this.Enabled == true ? Color.FromKnownColor(KnownColor.LemonChiffon) : Color.FromKnownColor(KnownColor.LightGray);
            cmb_Values.BackColor = this.Enabled == true ? Color.FromKnownColor(KnownColor.Window) : Color.FromKnownColor(KnownColor.LightGray);
        }

        public void EnableModifying(bool paramEnabled, bool modifyEnabled)
        {
            this.Enabled = paramEnabled;
            lbl_PrmName.BackColor = this.Enabled == true ? Color.FromKnownColor(KnownColor.LemonChiffon) : Color.FromKnownColor(KnownColor.LightGray);

            cmb_Values.Enabled = modifyEnabled;
            cmb_Values.BackColor = cmb_Values.Enabled == true ? Color.FromKnownColor(KnownColor.Window) : Color.FromKnownColor(KnownColor.LightGray);
        }

        public List<object> Get_Collection()
        {
            if (Collection != null && Collection.Length > 0)
            {
                List<object> rtn = new List<object>();
                for (int i = 0; i < Collection.Length; i++)
                {
                    rtn.Add(Collection.GetValue(i));
                }
                return rtn;
            }
            return null;
        }

        private void Cmb_Values_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;

            // Always draw the background
            e.DrawBackground();
            // Drawing one of the items?
            if (e.Index >= 0)
            {
                // Set the string alignment.  Choices are Center, Near and Far
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                // Set the Brush to ComboBox ForeColor to maintain any ComboBox color settings
                // Assumes Brush is solid
                Brush brush = new SolidBrush(cbx.ForeColor);
                // If drawing highlighted selection, change brush
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    brush = SystemBrushes.HighlightText;
                }

                // Draw the string
                e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
            }
        }

        private void Cmb_Values_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GenParam != null)
            {
                GenParam.Set_ValueObject(cmb_Values.SelectedItem, false);
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
            SelectedUserItemChangedEvent?.Invoke(this, cmb_Values.SelectedItem);
        }

        private void PrmCmp_SizeChanged(object sender, EventArgs e)
        {
            // UI Designer에서 Size 조정할때 Label이 찌그러지는 게 보기 싫어서,
            // SplitContainer의 Dock을 풀고 PrmCmp의 크기가 조정될 때마다 수동으로 Size를 할당함.
            splitContainer1.Size = this.Size;
            if (Orientation == Orientation.Horizontal)
            {
                cmb_Values.Width = this.Size.Width;
            }
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
        }
    }
}
