using AppDrones.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDrones.Core.Exceptions
{
    public class WeightLimitException : Exception
    {
        public WeightLimitException(string message): base(message)
        {
        }
    }
}
