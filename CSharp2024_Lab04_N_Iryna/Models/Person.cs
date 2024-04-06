
using CSharp2024_Lab04_N_Iryna.Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp2024_Lab04_N_Iryna.Models
{
    internal class Person
    {

        #region Fields
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate;

        private readonly bool _isAdult;
        private readonly bool _isBirthday;
        private readonly string _sunSign;
        private readonly string _chineseSign;
        #endregion

        #region Properties
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value;
            }
        }

        public bool IsAdult => _isAdult;
        public bool IsBirthday => _isBirthday;
        public string SunSign => _sunSign;
        public string ChineseSign => _chineseSign;

        #endregion

        #region Constructors

        internal Person(string firstName, string lastName, string email, DateTime date)
        {

            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthDate = date;

            if (date > DateTime.Today)
            {
                throw new FutureDateOfBirthException();
            }

            if (date < DateTime.Today.AddYears(-135))
            {
                throw new PastDateOfBirthException();
            }

            if (!IsValidEmail(email))
            {
                throw new InvalidEmailFormatException();
            }


            _isAdult = CalculateAge();
            _isBirthday = CheckBirthday();
            _sunSign = CalculateSunSign();
            _chineseSign = CalculateChineseSign();
        }
        public Person(string firstName, string lastName, string email) :
            this(firstName, lastName, email, DateTime.Today)
        {
        }

        public Person(string firstName, string lastName, DateTime date) :
            this(firstName, lastName, "csharp@ukma.edu.ua", date)
        {
        }


        #endregion


        #region Methods

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {

                string[] parts = email.Split('@');
                if (parts.Length != 2)
                {
                    return false;
                }


                string[] domainParts = parts[1].Split('.');
                if (domainParts.Length < 2 || string.IsNullOrWhiteSpace(domainParts[0]) || string.IsNullOrWhiteSpace(domainParts[1]))
                {
                    return false;
                }


                if (string.IsNullOrWhiteSpace(parts[0]))
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private string CalculateChineseSign()
        {
            int zodiacYear = (BirthDate.Year - 4) % 12;

            switch (zodiacYear)
            {
                case 0: return "Rat";
                case 1: return "Ox";
                case 2: return "Tiger";
                case 3: return "Rabbit";
                case 4: return "Dragon";
                case 5: return "Snake";
                case 6: return "Horse";
                case 7: return "Sheep";
                case 8: return "Monkey";
                case 9: return "Rooster";
                case 10: return "Dog";
                case 11: return "Pig";
                default: return string.Empty;
            }
        }

        private string CalculateSunSign()
        {
            int day = BirthDate.Day;
            int month = BirthDate.Month;

            return
                (month == 1 && day >= 20) || (month == 2 && day <= 18) ? "Aquarius" :
                (month == 2 && day >= 19) || (month == 3 && day <= 20) ? "Pisces" :
                (month == 3 && day >= 21) || (month == 4 && day <= 19) ? "Aries" :
                (month == 4 && day >= 20) || (month == 5 && day <= 20) ? "Taurus" :
                (month == 5 && day >= 21) || (month == 6 && day <= 20) ? "Gemini" :
                (month == 6 && day >= 21) || (month == 7 && day <= 22) ? "Cancer" :
                (month == 7 && day >= 23) || (month == 8 && day <= 22) ? "Leo" :
                (month == 8 && day >= 23) || (month == 9 && day <= 22) ? "Virgo" :
                (month == 9 && day >= 23) || (month == 10 && day <= 22) ? "Libra" :
                (month == 10 && day >= 23) || (month == 11 && day <= 21) ? "Scorpio" :
                (month == 11 && day >= 22) || (month == 12 && day <= 21) ? "Sagittarius" :
                (month == 12 && day >= 22) || (month == 1 && day <= 19) ? "Capricorn" :
                string.Empty;
        }

        private bool CheckBirthday()
        {
            if (BirthDate.Day == DateTime.Today.Day && BirthDate.Month == DateTime.Today.Month)
            {
                return true;
            }
            return false;
        }

        private bool CalculateAge()
        {
            int age = DateTime.Now.Year - _birthDate.Year;
            if (DateTime.Now.DayOfYear < _birthDate.DayOfYear)
            {
                age--;
            }
            return age >= 18;
        }

        public bool isNotFilled()
        {
            if (String.IsNullOrWhiteSpace(FirstName) ||
                String.IsNullOrWhiteSpace(LastName) ||
                String.IsNullOrWhiteSpace(Email)
               )
                return true;
            return false;
        }
        #endregion

    }
}
