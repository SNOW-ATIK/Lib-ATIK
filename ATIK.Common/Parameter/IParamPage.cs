using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK
{
    public interface IParamSetting
    {
        void Restore();
        void UpdateChangedStatus();
        void SaveAllParams(bool askSave);
        bool IsParamChanged();
    }

    public class ParamPageUtil
    {
        public static List<IPrmCmp> GetAll_IComps(System.Windows.Forms.Control ctrl)
        {
            return Handle_UI.GetAllControlsRecursive(ctrl).OfType<IPrmCmp>().ToList();
        }

        public static List<IParam> GetAll_IParams(System.Windows.Forms.Control ctrl)
        {
            List<IParam> AllParam = new List<IParam>();
            GetAll_IComps(ctrl).ForEach(cmp => AllParam.Add(cmp.GenParam));
            return AllParam;
        }
    }
}
