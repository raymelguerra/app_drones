using AppDrones.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Dto
{
    public class RegistryReqDto
    {
        public string SerialNumber { get; set; } = null!;
        public string Model { get; set; } = null!;
        public double WeightLimit { get; set; }
        public int BatteryCapacity { get; set; }
        public string State { get; set; } = null!;
    }
}
