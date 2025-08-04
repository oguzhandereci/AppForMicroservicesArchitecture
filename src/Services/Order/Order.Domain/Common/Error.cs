using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Common
{
    public class Error
    {
        public string ErrDescription { get; private set; }
        public string StackTrace { get; private set; }

        public Error(string desc, string stackTrace)
        {
            ErrDescription = desc;
            StackTrace = stackTrace;
        }
    }
}
