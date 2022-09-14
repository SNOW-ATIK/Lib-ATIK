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
    public partial class Cmp_Vessel_Bmp : UserControl
    {
        private Vessel_Color _VesselColor = Vessel_Color.None;
        public Vessel_Color VesselColor
        {
            get
            {
                return _VesselColor;
            }
            set
            {
                _VesselColor = value;
                Draw_State(_VesselColor, _Remains);
            }
        }

        private int _Remains = 0;
        public int Remains
        {
            get
            {
                return _Remains;
            }
            set
            {
                _Remains = value;
                Draw_State(_VesselColor, _Remains);
            }
        }

        public Cmp_Vessel_Bmp()
        {
            InitializeComponent();
        }

        public void Draw_State(Vessel_Color color, int remains)
        {
            string resourceName = $"Vessel_{color}_{remains / 20 * 20}";
            object obj = Properties.Resources.ResourceManager.GetObject(resourceName);
            if (obj != null)
            {
                Bitmap bg = (Bitmap)obj;
                this.BackgroundImage = bg;
            }
        }
    }
}
