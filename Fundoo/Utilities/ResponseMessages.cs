using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo.Utilities
{
    public class ResponseMessages
    {
        Dictionary<string, int> ResponseDict = new Dictionary<string, int>()
        {
            { "Email already exists", 400 },
            { "Account created", 201 },
            { "Account created", 201 },

        };
    }
}
