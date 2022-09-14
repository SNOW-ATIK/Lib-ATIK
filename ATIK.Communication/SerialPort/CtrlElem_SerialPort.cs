using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATIK.Communication.SerialPort
{
    public partial class CtrlElem_SerialPort : UserControl
    {
        private const int ROW_HEIGHT_VER = 28;
        private const int ROW_HEIGHT_HOR = 54;

        private bool _ShowDataBits = true;
        [Category("ATIK Properties")]
        [DisplayName("Display DataBits")]
        public bool ShowDataBits
        {
            get
            {
                return _ShowDataBits;
            }
            set
            {
                _ShowDataBits = value;
                RelocateTimeOutComp();
            }
        }

        private bool _ShowStopBits = true;
        [Category("ATIK Properties")]
        [DisplayName("Display StopBits")]
        public bool ShowStopBits
        {
            get
            {
                return _ShowStopBits;
            }
            set
            {
                _ShowStopBits = value;
                RelocateTimeOutComp();
            }
        }

        private bool _ShowParity = true;
        [Category("ATIK Properties")]
        [DisplayName("Display Parity")]
        public bool ShowParity
        {
            get
            {
                return _ShowParity;
            }
            set
            {
                _ShowParity = value;
                RelocateTimeOutComp();
            }
        }

        private bool _ShowReadTimeOut = true;
        [Category("ATIK Properties")]
        [DisplayName("Display Read TimeOut")]
        public bool ShowReadTimeOut
        {
            get
            {
                return _ShowReadTimeOut;
            }
            set
            {
                _ShowReadTimeOut = value;
                RelocateTimeOutComp();
            }
        }

        private bool _ShowWriteTimeOut = true;
        [Category("ATIK Properties")]
        [DisplayName("Display Write TimeOut")]
        public bool ShowWriteTimeOut
        {
            get
            {
                return _ShowWriteTimeOut;
            }
            set
            {
                _ShowWriteTimeOut = value;
                RelocateTimeOutComp();
            }
        }

        private bool _ShowTerminator = true;
        [Category("ATIK Properties")]
        [DisplayName("Display Terminator")]
        public bool ShowTerminator
        {
            get
            {
                return _ShowTerminator;
            }
            set
            {
                _ShowTerminator = value;
                RelocateTimeOutComp();
            }
        }

        private Orientation _SplitterOrientation = Orientation.Vertical;
        [Category("ATIK Properties")]
        [DisplayName("Splitter Orientation")]
        public Orientation SplitterOrientation
        {
            get
            {
                return _SplitterOrientation;
            }
            set
            {
                _SplitterOrientation = value;
                RelocateAll();
            }
        }

        private int _SplitDistance = 100;
        [Category("ATIK Properties")]
        [DisplayName("Split Distance")]
        public int SplitDistance
        {
            get
            {
                return _SplitDistance;
            }
            set
            {
                if (SplitterOrientation == Orientation.Vertical)
                {
                    _SplitDistance = value;
                    tableLayoutPanel2.Controls.OfType<IPrmCmp>().ToList().ForEach(cmp => cmp.SplitterDistance = _SplitDistance);
                }
            }
        }

        private Element_SerialPort MyElem;
        private bool EnableUserInput = false;

        public CtrlElem_SerialPort()
        {
            InitializeComponent();
        }

        public void ChangeLanguage_Title(string language, string title)
        {
            if (language == "ENG")
            {
                lbl_Name.Font = new Font("Consolas", 12f, FontStyle.Bold);
            }
            else if (language == "KOR")
            {
                lbl_Name.Font = new Font("맑은 고딕", 11f, FontStyle.Bold);
            }
            lbl_Name.Text = title;
        }

        public void Init(Element_SerialPort elem, string name = "", bool enableUserInput = false)
        {
            MyElem = elem;
            lbl_Name.Text = name;
            EnableUserInput = enableUserInput;

            string[] ports = Element_SerialPort.GetPortList();
            CmpCol_PortName.Init_WithOutGenPrm("PortName", ports, elem.ComSetting.PortName);

            int[] baudrates = new int[] { 9600, 19200, 38400, 57600, 115200 };
            CmpCol_BaudRate.Init_WithOutGenPrm("BaudRate", baudrates, elem.ComSetting.BaudRate);

            int[] databits = new int[] { 5, 6, 7, 8 };
            CmpCol_DataBits.Init_WithOutGenPrm("DataBits", databits, elem.ComSetting.DataBits);

            Array stopbits = Enum.GetValues(typeof(System.IO.Ports.StopBits));
            CmpCol_StopBits.Init_WithOutGenPrm("StopBits", stopbits, elem.ComSetting.StopBits);

            Array parity = Enum.GetValues(typeof(System.IO.Ports.Parity));
            CmpCol_Parity.Init_WithOutGenPrm("Parity", parity, elem.ComSetting.Parity);

            if (MyElem.IsOpened == true)
            {
                btn_Open.BackColor = Color.MediumSeaGreen;
                //btn_Open.Text = "CLOSE";

                tableLayoutPanel2.Controls.OfType<UserControl>().ToList().ForEach(cmp => cmp.Enabled = false);
            }
            else
            {
                btn_Open.BackColor = Color.Crimson;
                //btn_Open.Text = "OPEN";

                tableLayoutPanel2.Controls.OfType<UserControl>().ToList().ForEach(cmp => cmp.Enabled = true);
            }

            btn_Open.Enabled = enableUserInput;
        }

        private void RelocateTimeOutComp()
        {
            CmpCol_DataBits.Visible = ShowDataBits;
            CmpCol_StopBits.Visible = ShowStopBits;
            CmpCol_Parity.Visible = ShowParity;
            CmpVal_ReadTimeOut.Visible = ShowReadTimeOut;
            CmpVal_WriteTimeOut.Visible = ShowWriteTimeOut;
            CmpVal_Terminator.Visible = ShowTerminator;

            tableLayoutPanel2.RowStyles[2].Height = ShowDataBits == true ? (SplitterOrientation == Orientation.Vertical ? ROW_HEIGHT_VER : ROW_HEIGHT_HOR) : 0;
            tableLayoutPanel2.RowStyles[3].Height = ShowStopBits == true ? (SplitterOrientation == Orientation.Vertical ? ROW_HEIGHT_VER : ROW_HEIGHT_HOR) : 0;
            tableLayoutPanel2.RowStyles[4].Height = ShowParity == true ? (SplitterOrientation == Orientation.Vertical ? ROW_HEIGHT_VER : ROW_HEIGHT_HOR) : 0;
            tableLayoutPanel2.RowStyles[5].Height = ShowReadTimeOut == true? (SplitterOrientation == Orientation.Vertical ? ROW_HEIGHT_VER : ROW_HEIGHT_HOR) : 0;
            tableLayoutPanel2.RowStyles[6].Height = ShowWriteTimeOut == true ? (SplitterOrientation == Orientation.Vertical ? ROW_HEIGHT_VER : ROW_HEIGHT_HOR) : 0;
            tableLayoutPanel2.RowStyles[7].Height = ShowTerminator == true ? (SplitterOrientation == Orientation.Vertical ? ROW_HEIGHT_VER : ROW_HEIGHT_HOR) : 0;
        }

        private void RelocateAll()
        {
            tableLayoutPanel2.Controls.OfType<IPrmCmp>().ToList().ForEach(cmp => cmp.Orientation = _SplitterOrientation);
            for (int i = 0; i < 8; i++)
            {
                tableLayoutPanel2.RowStyles[i].Height = _SplitterOrientation == Orientation.Vertical ? ROW_HEIGHT_VER : ROW_HEIGHT_HOR;
            }

            if (_SplitterOrientation == Orientation.Vertical)
            {
                _SplitDistance = 100;
                tableLayoutPanel2.Controls.OfType<IPrmCmp>().ToList().ForEach(cmp => cmp.SplitterDistance = _SplitDistance);
            }

            RelocateTimeOutComp();
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            if (EnableUserInput == false)
            {
                return;
            }

            if (MyElem.IsOpened == true)
            {
                MyElem.Close();

                btn_Open.BackColor = Color.Crimson;
                //btn_Open.Text = "OPEN";

                tableLayoutPanel2.Controls.OfType<UserControl>().ToList().ForEach(cmp => cmp.Enabled = true);
            }
            else
            {
                try
                {
                    MyElem.Open();
                }
                catch
                {
                    MsgFrm_NotifyOnly msgFrm = new MsgFrm_NotifyOnly($"Failed to open SerialPort. (PortName={MyElem.ComSetting.PortName}, BaudRate={MyElem.ComSetting.BaudRate})");
                    msgFrm.ShowDialog();
                }

                if (MyElem.IsOpened == true)
                {
                    btn_Open.BackColor = Color.MediumSeaGreen;
                    //btn_Open.Text = "CLOSE";

                    tableLayoutPanel2.Controls.OfType<UserControl>().ToList().ForEach(cmp => cmp.Enabled = false);
                }
                else
                {
                    btn_Open.BackColor = Color.Crimson;
                    //btn_Open.Text = "OPEN";

                    tableLayoutPanel2.Controls.OfType<UserControl>().ToList().ForEach(cmp => cmp.Enabled = true);
                }
            }
        }
    }
}
