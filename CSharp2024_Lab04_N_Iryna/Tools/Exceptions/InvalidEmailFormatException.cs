using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp2024_Lab04_N_Iryna.Tools.Exceptions
{
    internal class InvalidEmailFormatException : Exception
    {
        public InvalidEmailFormatException() : base("Invalid email format")
        {
        }
    }
}
