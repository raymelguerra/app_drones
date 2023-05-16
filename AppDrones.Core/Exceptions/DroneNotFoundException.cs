using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AppDrones.Core.Exceptions
{
    public class DroneNotFoundException : Exception
    {
        public DroneNotFoundException(string message) : base(message)
        {
        }
        public DroneNotFoundException() : base()
        {
        }
    }
}
