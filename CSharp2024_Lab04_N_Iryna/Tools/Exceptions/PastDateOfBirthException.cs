using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp2024_Lab04_N_Iryna.Tools.Exceptions
{
    internal class PastDateOfBirthException : Exception
    {
        public PastDateOfBirthException() : base("Date of birth is too far in the past.")
        {
        }
    }
}
