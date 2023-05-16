using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Dto
{
    public class BatteryLevelTesterSettings
    {
        public int RunFrequency { get; set; }
        public int Good { get; set; }
        public int Warning { get; set; }
        public int Critical { get; set; }
    }
}
