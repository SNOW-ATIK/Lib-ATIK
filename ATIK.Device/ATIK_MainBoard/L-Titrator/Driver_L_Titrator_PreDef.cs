using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK.Device.ATIK_MainBoard
{
    public partial class DrvMB_L_Titrator
    {
        public enum LineOrder
        {
            STX,
            Protocol_ID,
            Error_Code,
            Model,
            Relay_Input,
            Relay_Output,
            Parallel_Input,
            Parallel_Output,
            Alarm_Input,
            Solenoid_Output,
            Temperature_RTD,
            Temperature_TC,
            Mixer_Duty,
            Mixer_RPM,
            Syringe_1,
            Syringe_2,
            Analog_Input_Ch0,
            Analog_Input_Ch1,
            Analog_Input_Ch2,
            Analog_Input_Ch3,
            Analog_Output_Ch0,
            Analog_Output_Ch1,
            Analog_Output_Ch2,
            Analog_Output_Ch3,
            ETX
        }
    }
}
