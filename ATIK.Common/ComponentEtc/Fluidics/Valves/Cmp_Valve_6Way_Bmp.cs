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
    public partial class Cmp_Valve_6Way_Bmp : UserControl
    {
        public readonly Valve_Category Valve_Category = Valve_Category.SixWay;
        public string Valve_Name = "NotDefined6Way";

        private Valve_6Way_State _Valve_State = Valve_6Way_State.Link_None;
        public Valve_6Way_State Link_State
        {
            get
            {
                return _Valve_State;
            }
            set
            {
                _Valve_State = value;
                Draw_State(_Valve_State);
            }
        }

        private Valve_6Way_Cfg _Valve_Config = Valve_6Way_Cfg.None;
        public Valve_6Way_Cfg Valve_Config
        {
            get => _Valve_Config;
            set
            {
                _Valve_Config = value;
            }
        }
        private Valve_PortDirection _Valve_Common_Port = Valve_PortDirection.None;
        public Valve_PortDirection Valve_Common_Port
        {
            get => _Valve_Common_Port;
            set
            {
                _Valve_Common_Port = value;
            }
        }

        public Dictionary<Valve_PortDirection, Valve_State> Port_State = new Dictionary<Valve_PortDirection, Valve_State>();

        public Cmp_Valve_6Way_Bmp()
        {
            InitializeComponent();

            Port_State.Add(Valve_PortDirection.Link_12, Valve_State.Close);
            Port_State.Add(Valve_PortDirection.Link_23, Valve_State.Close);
        }

        public void Init(string valveName)
        {
            Valve_Name = valveName;
        }

        public void Draw_State(Valve_6Way_State state)
        {
            // Check States
            string sState = state.ToString();
            string resourceName = $"Valve6Way_{sState}";
            object obj = Properties.Resources.ResourceManager.GetObject(resourceName);
            if (obj != null)
            {
                Bitmap bg = (Bitmap)obj;
                this.BackgroundImage = bg;
            }
            switch (state)
            {
                case Valve_6Way_State.Link_None:
                    Port_State[Valve_PortDirection.Link_12] = Valve_State.Close;
                    Port_State[Valve_PortDirection.Link_23] = Valve_State.Close;
                    break;

                case Valve_6Way_State.Link_12:
                    Port_State[Valve_PortDirection.Link_12] = Valve_State.Open;
                    Port_State[Valve_PortDirection.Link_23] = Valve_State.Close;
                    break;

                case Valve_6Way_State.Link_23:
                    Port_State[Valve_PortDirection.Link_12] = Valve_State.Close;
                    Port_State[Valve_PortDirection.Link_23] = Valve_State.Open;
                    break;
            }
        }
    }
}
