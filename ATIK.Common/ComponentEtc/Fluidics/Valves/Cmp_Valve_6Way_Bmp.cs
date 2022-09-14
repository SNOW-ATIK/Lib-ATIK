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
        private Valve_6Way_State _Valve_State = Valve_6Way_State.Link_None;
        public Valve_6Way_State Valve_State
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

        public Cmp_Valve_6Way_Bmp()
        {
            InitializeComponent();
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
        }
    }
}
