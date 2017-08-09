using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCC_Application.Exceptions
{
    public class GoogleMapsApiException : System.Exception
    {
        public GoogleMapsApiException(string message) : base(message)
        {
        }
    }
}
