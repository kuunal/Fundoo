using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomException
{
    public class FundooException : Exception
    {
        string message;
        public FundooException(string message) : base(message)
        {
            this.message = message;
        }
    }
}
