using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp2024_Lab04_N_Iryna.Tools.Exceptions
{
    internal class FutureDateOfBirthException : Exception
    {
        public FutureDateOfBirthException() : base("Date of birth cannot be in the future.")
        {
        }
    }
}
