using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ATIK
{
    public interface IPrmCmp
    {
        Color Color_Name { get; set; }
        Color Color_Value { get; set; }
        string Prm_Name { get; set; }
        object Prm_Value { get; set; }
        IParam GenParam { get; set; }
        PrmCmp.PrmType Prm_Type { get; set; }
        Orientation Orientation { get; set; }
        int SplitterDistance { get; set; }
        void UpdateNamePlate();
        void Restore();
        void ChangeLanguage_Title(string language, string title);
    }
}
