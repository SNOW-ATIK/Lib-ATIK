using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK.Device.IO
{
    public enum IODriver
    { 
        Unknown,
        CREVIS_MODBUS_TCP,
    }

    public enum IOType
    { 
        Unknown,
        Reserved,
        DIN,
        DOUT,
        AIN,
        AOUT,
    }

    public enum AnalogCurrentRange
    { 
        From_0_To_20mA,
        From_4_To_20mA,
        From_m20_To_p20mA,
        Unknown,
    }
}
