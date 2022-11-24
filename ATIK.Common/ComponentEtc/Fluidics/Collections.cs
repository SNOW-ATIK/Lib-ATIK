using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK.Common.ComponentEtc.Fluidics
{
    public enum Valve_Category
    { 
        TwoWay,
        ThreeWay,
        SixWay,
    }

    public enum Valve_2Way_Cfg
    { 
        None,
        TopBottom,
        LeftRight
    }

    public enum Valve_6Way_Cfg
    { 
        None,
    }

    public enum Valve_3Way_Cfg
    {
        None,
        TopRightBottom,
        RightBottomLeft,
        BottomLeftTop,
        LeftTopRight,
    }

    public enum Valve_6Way_State
    { 
        Link_None,
        Link_12,
        Link_23
    }

    public enum Valve_PortType
    { 
        None,
        Single,
        Dual
    }

    public enum Valve_PortDirection
    { 
        None,
        Top,
        Right,
        Bottom,
        Left,
        Link_12,
        Link_23
    }

    public enum Valve_State
    { 
        NotExist,
        Common,        
        Close,
        Open,
    }

    public enum Reagent_Color
    { 
        None,
        Orange,
        Yellow,
    }

    public enum Reagent_OutLocation
    { 
        None,
        Left,
        Right
    }

    public enum Syringe_Color
    { 
        None,
        Orange,
        Yellow
    }

    public enum Syringe_Head
    { 
        None,
        LeftTopRight,
        LeftRight
    }

    public enum Vessel_Color
    { 
        None,
        Green,
    }
}
