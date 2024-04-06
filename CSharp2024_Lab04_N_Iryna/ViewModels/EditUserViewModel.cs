using CSharp2024_Lab04_N_Iryna.Tools;
using CSharp2024_Lab04_N_Iryna.Models;
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
    internal class EditUserViewModel : INotifyPropertyChanged, INavigatable
    {

        private RelayCommand<object> _editPersonCommand;
        private bool _isEnabled = true;
        private Action _gotoUserListView;

        public EditUserViewModel(Action gotoDataView)
        {
            _gotoUserListView = gotoDataView;
        }

        #region Properties

        public string FirstName { get; set; } = UserListViewModel.SelectedPerson.FirstName;
        public string LastName { get; set; } = UserListViewModel.SelectedPerson.LastName;
        public string Email { get; set; } = UserListViewModel.SelectedPerson.Email;
        public DateTime BirthDate { get; set; } = UserListViewModel.SelectedPerson.Person.BirthDate;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand<object> UpdateCommand => _editPersonCommand ??= new RelayCommand<object>(_ => UpdatePerson());

        #endregion

        private async void UpdatePerson()
        {
            IsEnabled = false;
            try
            {
                await Task.Run(async () =>
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
                    int currentIndex = UsersDataBase.users.IndexOf(UserListViewModel.SelectedPerson.Person);
                    UsersDataBase.users.Remove(UserListViewModel.SelectedPerson.Person);
                    UsersDataBase.users.Insert(currentIndex, user);
                    MessageBox.Show("Person successfully edited!");
                    if (BirthDate.Month == DateTime.Today.Month && BirthDate.Day == DateTime.Today.Day)
                    {
                        MessageBox.Show("Happy Birthday!");
                    }
                    _gotoUserListView.Invoke();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsEnabled = true;
            }
        }
        private void GotoUserList()
        {
            _gotoUserListView.Invoke();
        }
        public NavigationViewTypes ViewType => NavigationViewTypes.EditUserViewModel;

        #region PropChange

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
