using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Models
{
    public class Drone
    {
        public int DroneId { get; set; }
        public string SerialNumber { get; set; } = null!;
        public Model Model { get; set; }
        public double WeightLimit { get; set; }
        public int BatteryCapacity { get; set; }
        public State State { get; set; }

        public IEnumerable<Medication> Medications { get; set; } = Enumerable.Empty<Medication>();
    }
}
