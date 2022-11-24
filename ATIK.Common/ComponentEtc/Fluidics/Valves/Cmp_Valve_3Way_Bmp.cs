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
        public readonly Valve_Category Valve_Category = Valve_Category.ThreeWay;
        public string Valve_Name = "NotDefined3Way";

        private Valve_3Way_Cfg _Valve_Config = Valve_3Way_Cfg.None;
        public Valve_3Way_Cfg Valve_Config
        {
            get => _Valve_Config;
            set
            {
                _Valve_Config = value;
                if (_Valve_Config != Valve_3Way_Cfg.None && _Valve_Common_Port != Valve_PortDirection.None)
                {
                    InitBackground(_Valve_Config, _Valve_Common_Port);
                }
            }
        }

        private Valve_PortDirection _Valve_Common_Port = Valve_PortDirection.None;
        public Valve_PortDirection Valve_Common_Port 
        {
            get => _Valve_Common_Port;
            set
            {
                _Valve_Common_Port = value;
                if (_Valve_Config != Valve_3Way_Cfg.None && _Valve_Common_Port != Valve_PortDirection.None)
                {
                    InitBackground(_Valve_Config, _Valve_Common_Port);
                }
            }
        }

        public Dictionary<Valve_PortDirection, Valve_State> Port_State = new Dictionary<Valve_PortDirection, Valve_State>();

        public Cmp_Valve_3Way_Bmp()
        {
            InitializeComponent();

            Port_State.Add(Valve_PortDirection.Bottom, Valve_State.Close);
            Port_State.Add(Valve_PortDirection.Left, Valve_State.Close);
            Port_State.Add(Valve_PortDirection.Right, Valve_State.Close);
            Port_State.Add(Valve_PortDirection.Top, Valve_State.Close);
        }

        public void Init(string valveName)
        {
            Valve_Name = valveName;
        }

        public void Draw_State(Valve_PortDirection port1, bool port1_state, Valve_PortDirection port2, bool port2_state)
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
                Port_State.Keys.ToList().ForEach(key =>
                {
                    if (Port_State[key] != Valve_State.Common && Port_State[key] != Valve_State.NotExist)
                    {
                        Port_State[key] = Valve_State.Open;
                    }
                });
            }
            else if (port1_state == false && port2_state == false)
            {
                sState = "Close_All";
                Port_State.Keys.ToList().ForEach(key =>
                {
                    if (Port_State[key] != Valve_State.Common && Port_State[key] != Valve_State.NotExist)
                    {
                        Port_State[key] = Valve_State.Close;
                    }
                });
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
                    Port_State[port1] = Valve_State.Open;
                    Port_State[port2] = Valve_State.Close;
                }
                else // if Iport2_state == true)
                {
                    sState = $"Open_{sPort2}";
                    Port_State[port1] = Valve_State.Close;
                    Port_State[port2] = Valve_State.Open;
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

        public void InitBackground(Valve_3Way_Cfg valveCfg, Valve_PortDirection cmnPort)
        {
            switch (valveCfg)
            {
                case Valve_3Way_Cfg.TopRightBottom:
                    Port_State[Valve_PortDirection.Left] = Valve_State.NotExist;
                    break;

                case Valve_3Way_Cfg.RightBottomLeft:
                    Port_State[Valve_PortDirection.Top] = Valve_State.NotExist;
                    break;

                case Valve_3Way_Cfg.BottomLeftTop:
                    Port_State[Valve_PortDirection.Right] = Valve_State.NotExist;
                    break;

                case Valve_3Way_Cfg.LeftTopRight:
                    Port_State[Valve_PortDirection.Bottom] = Valve_State.NotExist;
                    break;
            }
            Port_State[cmnPort] = Valve_State.Common;

            string sCmnPort = Get_PortString(Valve_Common_Port);
            string sCfg = Get_CfgString(valveCfg);
            
            string resourceName = $"Valve3Way_{sCfg}_Common_{sCmnPort}_Close_All";
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

        private string Get_PortString(Valve_PortDirection valvePort)
        {
            return valvePort.ToString().Substring(0, 1);
        }
    }
}
