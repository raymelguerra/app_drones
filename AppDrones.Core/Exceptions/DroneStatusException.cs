using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Exceptions
{
    public class DroneStatusException : Exception
    {
        public DroneStatusException(string message) : base(message)
        {

        }
        
        public DroneStatusException() : base()
        {

        }
    }
}
