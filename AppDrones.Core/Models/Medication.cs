using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Models
{
    public class Medication
    {
        public int MedicationId { get; set; }
        public string Name { get; set; } = null!;
        public int Weight { get; set; }
        public string Code { get; set; } = null!;
        public string Image { get; set; } = null!;

        public Drone Drone { get; set; } = null!;
        public int DroneId { get; set; }
    }
}
