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
    public partial class Cmp_Syringe_Bmp : UserControl
    {
        private Syringe_Color _SyringeColor = Syringe_Color.None;
        public Syringe_Color SyringeColor
        {
            get
            {
                return _SyringeColor;
            }
            set
            {
                _SyringeColor = value;
                Draw_State(_SyringeColor, _Remains);
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
                Draw_State(SyringeColor, _Remains);
            }
        }

        public Cmp_Syringe_Bmp()
        {
            InitializeComponent();
        }

        public void Draw_State(Syringe_Color color, int remains)
        {
            string resourceName = $"Syringe_{color}_{remains / 10 * 10}";
            object obj = Properties.Resources.ResourceManager.GetObject(resourceName);
            if (obj != null)
            {
                Bitmap bg = (Bitmap)obj;
                this.BackgroundImage = bg;
            }
        }
    }
}
