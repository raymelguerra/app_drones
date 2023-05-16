using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Exceptions
{
    public class LowBatteryException : Exception
    {
        public LowBatteryException(string message) : base(message)
        {}
        
        public LowBatteryException() : base()
        {}
    }
}
