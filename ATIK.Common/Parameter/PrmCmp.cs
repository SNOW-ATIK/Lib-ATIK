using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK
{
    public class PrmCmp
    {
        public const int MIN_SPLITDISTANCE = 30;

        public const int MIN_PRM_NAME_WIDTH = 100;
        public const int MIN_PRM_VALUE_WIDTH = 100;
        public const int MIN_PRM_NAME_HEIGHT = 22;
        public const int MIN_PRM_VALUE_HEIGHT = 25;

        public enum PrmType
        {
            Boolean,
            Integer,
            Double,
            String,
            UserCollection,
        }
    }
}
