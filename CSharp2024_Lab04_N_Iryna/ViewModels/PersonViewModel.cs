using CSharp2024_Lab04_N_Iryna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp2024_Lab04_N_Iryna.ViewModels
{
    internal class PersonViewModel
    {
        private Person _user;
        public string FirstName => _user.FirstName;
        public string LastName => _user.LastName;
        public string Email => _user.Email;
        public string BirthDate => _user.BirthDate.ToString("d");
        public bool IsAdult => _user.IsAdult;
        public bool IsBirthday => _user.IsBirthday;
        public string SunSign => _user.SunSign;
        public string ChineseSign => _user.ChineseSign;

        internal Person Person { get => _user; set => _user = value; }

        public PersonViewModel(Person person)
        {
            _user = person ?? throw new ArgumentNullException(nameof(person));
        }
    }
}
