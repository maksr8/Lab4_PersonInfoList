using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_PersonInfoList.Exceptions
{
    class BirthDateInFutureException : Exception
    {

        public BirthDateInFutureException() :
            base("The selected birth date is in the future.")
        {
            
        }
        public BirthDateInFutureException(string message) :
            base(message)
        {
            
        }
    }
}
