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
    public partial class Cmp_Valve_3Way_Bmp : UserControl
    {
        private Valve_Port _Valve_Common_Port = Valve_Port.None;
        public Valve_Port Valve_Common_Port { get => _Valve_Common_Port; set => _Valve_Common_Port = value; }

        private Valve_3Way_Cfg _Valve_Config = Valve_3Way_Cfg.None;
        public Valve_3Way_Cfg Valve_Config
        {
            get
            {
                return _Valve_Config;
            }
            set
            {
                _Valve_Config = value;
                InitBackground(_Valve_Config, Valve_Common_Port);
            }
        }

        public Cmp_Valve_3Way_Bmp()
        {
            InitializeComponent();
        }

        public void Draw_State(Valve_Port port1, bool port1_state, Valve_Port port2, bool port2_state)
        {
            if (port1 == Valve_Common_Port || port2 == Valve_Common_Port)
            {
                throw new InvalidOperationException("Can't set state on Common Port.");
            }

            // Check Config
            string sCfg = Get_CfgString(Valve_Config);

            // Check Common Port
            string sCommonPort = $"Common_{Get_PortString(Valve_Common_Port)}";

            // Check State
            string sState = "";
            if (port1_state == true && port2_state == true)
            {
                sState = "Open_All";
            }
            else if (port1_state == false && port2_state == false)
            {
                sState = "Close_All";
            }
            else
            {
                string sPort1 = Get_PortString(port1);
                string sPort2 = Get_PortString(port2);
                if (sCfg.Contains(sPort1) == false || sCfg.Contains(sPort2) == false)
                {
                    return;
                }

                if (port1_state == true)
                {
                    sState = $"Open_{sPort1}";
                }
                else // if Iport2_state == true)
                {
                    sState = $"Open_{sPort2}";
                }
            }

            string resourceName = $"Valve3Way_{sCfg}_{sCommonPort}_{sState}";
            object obj = Properties.Resources.ResourceManager.GetObject(resourceName);
            if (obj != null)
            {
                Bitmap bg = (Bitmap)obj;
                this.BackgroundImage = bg;
            }
        }

        public void InitBackground(Valve_3Way_Cfg valveCfg, Valve_Port cmnPort)
        {
            Valve_Common_Port = cmnPort;
            if (Valve_Common_Port == Valve_Port.None)
            {
                return;
            }

            string sCmnPort = Get_PortString(Valve_Common_Port);
            string sCfg = Get_CfgString(valveCfg);
            
            string resourceName = $"Valve3Way_{sCfg}_Common_{sCmnPort}_ClosedAll";
            object obj = Properties.Resources.ResourceManager.GetObject(resourceName);
            if (obj != null)
            {
                Bitmap bg = (Bitmap)obj;
                this.BackgroundImage = bg;
            }
        }

        private string Get_CfgString(Valve_3Way_Cfg valveCfg)
        {
            string sCfg = "";
            switch (valveCfg)
            {
                case Valve_3Way_Cfg.TopRightBottom:
                    sCfg = "TRB";
                    break;

                case Valve_3Way_Cfg.RightBottomLeft:
                    sCfg = "RBL";
                    break;

                case Valve_3Way_Cfg.BottomLeftTop:
                    sCfg = "BLT";
                    break;

                case Valve_3Way_Cfg.LeftTopRight:
                    sCfg = "LTR";
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
