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
    public partial class Cmp_Reagent_Bmp : UserControl
    {
        private Reagent_Color _ReagentColor = Reagent_Color.None;
        public Reagent_Color ReagentColor
        {
            get
            {
                return _ReagentColor;
            }
            set
            {
                _ReagentColor = value;
                Draw_State(_ReagentColor, _ReagentOutLoc, _Remains);
            }
        }

        private Reagent_OutLocation _ReagentOutLoc = Reagent_OutLocation.None;
        public Reagent_OutLocation ReagentOutLoc
        {
            get
            {
                return _ReagentOutLoc;
            }
            set
            {
                _ReagentOutLoc = value;
                Draw_State(_ReagentColor, _ReagentOutLoc, _Remains);
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
                Draw_State(_ReagentColor, _ReagentOutLoc, _Remains);
            }
        }

        public Cmp_Reagent_Bmp()
        {
            InitializeComponent();
        }

        public void Draw_State(Reagent_Color color, Reagent_OutLocation outLoc, int remains)
        {
            string resourceName = $"Reagent_{color}_{outLoc.ToString().Substring(0, 1)}_{remains / 10 * 10}";
            object obj = Properties.Resources.ResourceManager.GetObject(resourceName);
            if (obj != null)
            {
                Bitmap bg = (Bitmap)obj;
                this.BackgroundImage = bg;
            }
        }
    }
}
