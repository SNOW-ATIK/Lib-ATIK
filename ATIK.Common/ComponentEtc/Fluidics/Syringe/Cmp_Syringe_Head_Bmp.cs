using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATIK.Common.ComponentEtc.Fluidics
{
    public partial class Cmp_Syringe_Head_Bmp : UserControl
    {
        private Syringe_Head _HeadType = Syringe_Head.None;
        public Syringe_Head HeadType
        {
            get
            {
                return _HeadType;
            }
            set
            {
                _HeadType = value;
                Draw_State(_HeadType, Valve_Port.None);
            }
        }

        private Valve_Port _Valve_Open = Valve_Port.None;
        public Valve_Port Valve_Open
        {
            get
            {
                return _Valve_Open;
            }
            set
            {
                _Valve_Open = value;
                Draw_State(_HeadType, _Valve_Open);
            }
        }

        public Cmp_Syringe_Head_Bmp()
        {
            InitializeComponent();
        }

        public void Draw_State(Syringe_Head headType, Valve_Port openPort)
        {
            if (headType == Syringe_Head.None || openPort == Valve_Port.None)
            {
                return;
            }

            switch (headType)
            {
                case Syringe_Head.LeftTopRight:
                    if (openPort == Valve_Port.Bottom)
                    {
                        throw new InvalidOperationException($"Head type doesn't have {openPort}");
                    }
                    break;

                case Syringe_Head.LeftRight:
                    if (openPort == Valve_Port.Top || openPort == Valve_Port.Bottom)
                    {
                        throw new InvalidOperationException($"Head type doesn't have {openPort}");
                    }
                    break;
            }

            string sCfg = Get_CfgString(headType);
            string sOpenPort = Get_PortString(openPort);

            string resourceName = $"Syringe_Head_{sCfg}_Open_{sOpenPort}";
            object obj = Properties.Resources.ResourceManager.GetObject(resourceName);
            if (obj != null)
            {
                Bitmap bg = (Bitmap)obj;
                this.BackgroundImage = bg;
            }
        }

        private string Get_CfgString(Syringe_Head headType)
        {
            string sCfg = "";
            switch (headType)
            {
                case Syringe_Head.None:
                    sCfg = "None";
                    break;

                case Syringe_Head.LeftTopRight:
                    sCfg = "LTR";
                    break;

                case Syringe_Head.LeftRight:
                    sCfg = "LR";
                    break;
            }
            return sCfg;
        }

        private string Get_PortString(Valve_Port valvePort)
        {
            return valvePort.ToString().Substring(0, 1);
        }
    }
}
