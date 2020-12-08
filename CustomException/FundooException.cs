using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomException
{
    public class FundooException : Exception
    {
        public string Message;
        public int StatusCode;
        public FundooException(string message, int statusCode=400) : base(message)
        {
            this.Message = message;
            this.StatusCode = statusCode;
        }

        public override string ToString()
        {
            return StatusCode.ToString();
        }
    }
}
