using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbotWeb
{
    public class RequestException<T> : Exception
    {
        public RequestException(string message)
            : base("Exception in: " + typeof(T).ToString() + " - Message: " + message)
        {

        }
    }
}
