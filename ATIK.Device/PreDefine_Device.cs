using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATIK.Device.ATIK_MainBoard;

namespace ATIK.Device
{
    public class Syringe_Command
    {
        public MB_SyringeFlow Flow;
        public MB_SyringeDirection Direction;
        public int Speed;
        public double Volume_mL;
    }
}
