using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_PersonInfoList.Exceptions
{
    class TooHighAgeException : Exception
    {

        public TooHighAgeException() :
            base("The age entered is too high.")
        {

        }
        public TooHighAgeException(string message) :
            base(message)
        {

        }
    }
}
