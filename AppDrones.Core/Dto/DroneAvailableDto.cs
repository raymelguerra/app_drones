using AppDrones.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Dto
{
    public class DroneAvailableDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; } = null!;
        public Model Model { get; set; }
        public double WeightLimit { get; set; }
        public int BatteryCapacity { get; set; }
        public State State { get; set; }
    }
}
