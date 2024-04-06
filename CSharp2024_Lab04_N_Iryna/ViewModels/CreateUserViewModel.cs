using CSharp2024_Lab04_N_Iryna.Models;
using CSharp2024_Lab04_N_Iryna.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CSharp2024_Lab04_N_Iryna.ViewModels
{
    class CreateUserViewModel : INotifyPropertyChanged, INavigatable
    {
        #region Fields

        private bool _isEnabled = true;
        private RelayCommand<object> _createCommand;
        private Action _gotoUserListView;


        #endregion

        public CreateUserViewModel(Action gotoDataView)
        {
            _gotoUserListView = gotoDataView ?? throw new ArgumentNullException(nameof(gotoDataView));
        }



        #region Properties 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; } = DateTime.Today;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand<object> CreateCommand =>
            _createCommand ??= new RelayCommand<object>(_ => Create(), BoxesFilled);

        #endregion

        internal async void Create()
        {
            IsEnabled = false;
            Person user;
            try
            {
                user = new Person(FirstName, LastName, Email, BirthDate);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                IsEnabled = true;
                return;
            }
            if (user.isNotFilled())
            {
                MessageBox.Show("Please fill in all the fields.");
                IsEnabled = true;
                return;
            }

            UsersDataBase.users.Add(user);
            MessageBox.Show("Person successfully added!");
            if (BirthDate.Month == DateTime.Today.Month && BirthDate.Day == DateTime.Today.Day)
            {
                MessageBox.Show("Happy Birthday!");
            }
            _gotoUserListView.Invoke();
        }

        private void GotoUserList()
        {
            _gotoUserListView.Invoke();
        }

        private bool BoxesFilled(object obj)
        {
            return !String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(LastName) &&
                   !String.IsNullOrWhiteSpace(Email);
        }

        public NavigationViewTypes ViewType => NavigationViewTypes.CreateUserViewModel;

        #region PropChange

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
