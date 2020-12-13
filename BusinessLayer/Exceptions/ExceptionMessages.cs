using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Exceptions
{
    public class ExceptionMessages
    {
        public static readonly string NO_SUCH_NOTE = "No such note!";
        public static readonly string SELF_COLLABORATE = "Cannot collaborate with self";
        public static readonly string ALREADY_COLLABORATER = "Already added as collaborator";
        public static readonly string NO_SUCH_USER = "No such user";
        public static readonly string NO_SUCH_COLLABORATOR = "No such collaborator";
        public static readonly string INVALID_CREDENTIALS = "Invalid ID or Password";
        public static readonly string NO_SUCH_LABEL = "No such label exist!";
        public static readonly string INVALID_TOKEN = "Invalid Token";
        public static readonly string TOKEN_EXPIRED = "Token expired";

    }
}
