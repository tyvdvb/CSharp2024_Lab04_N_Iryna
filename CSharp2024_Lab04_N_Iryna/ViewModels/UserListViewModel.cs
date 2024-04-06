using CSharp2024_Lab04_N_Iryna.Tools;
using CSharp2024_Lab04_N_Iryna.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSharp2024_Lab04_N_Iryna.ViewModels
{
    class UserListViewModel : INotifyPropertyChanged, INavigatable
    {
        #region Fields
        private ObservableCollection<PersonViewModel> _people;

        private Action _goToEditView;
        private Action _goToCreateView;

        private RelayCommand<object> _createPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _detelePersonCommand;

        private RelayCommand<object> _applyFilters;
        private string _sorter;
        private string _filter;
        #endregion

        #region Constructor
       
        public UserListViewModel(Action goToAddView, Action goToEditView) 
        {
            _people = new ObservableCollection<PersonViewModel>(
               UsersDataBase.users.Select(person => new PersonViewModel(person)));


            _goToCreateView = goToAddView ?? throw new ArgumentNullException(nameof(goToAddView));
            _goToEditView = goToEditView ?? throw new ArgumentNullException(nameof(goToAddView));
        }
        #endregion
        #region Prperties

        public ObservableCollection<PersonViewModel> People
        {
            get => _people;
            set
            {
                _people = value;
                NotifyPropertyChanged();
            }
        }

        public static PersonViewModel? SelectedPerson { get; set; }


        public string SortingBy
        {
            get => _sorter;
            set
            {
                _sorter = value;
                PerformSorting();
            }
        }

        public string FilterBy
        {
            get => _filter;
            set
            {
                _filter = value;
            }
        }

        public string FilterValue { get; set; }

        #endregion

        #region Filtering and Sorting

        public static List<string> SortingFields { get; } = new List<string>
        {
            "First name", "Last name", "Email", "Birth date", "Is adult?", "Sun sign", "Chinese sign", "Is B-day?",
            "None"
        };

        private void PerformSorting()
        {
            switch (_sorter)
            {
                case "First name":
                    People = new ObservableCollection<PersonViewModel>(_people.OrderBy(i => i.FirstName));
                    break;
                case "Last name":
                    People = new ObservableCollection<PersonViewModel>(_people.OrderBy(i => i.LastName));
                    break;
                case "Email":
                    People = new ObservableCollection<PersonViewModel>(_people.OrderBy(i => i.Email));
                    break;
                case "Birth date":
                    People = new ObservableCollection<PersonViewModel>(_people.OrderBy(i => i.Person.BirthDate));
                    break;
                case "Is B-day?":
                    People = new ObservableCollection<PersonViewModel>(_people.OrderBy(i => i.IsBirthday));
                    break;
                case "Is adult?":
                    People = new ObservableCollection<PersonViewModel>(_people.OrderBy(i => i.IsAdult));
                    break;
                case "Sun sign":
                    People = new ObservableCollection<PersonViewModel>(_people.OrderBy(i => i.SunSign));
                    break;
                case "Chinese sign":
                    People = new ObservableCollection<PersonViewModel>(_people.OrderBy(i => i.ChineseSign));
                    break;
                default:
                    People = new ObservableCollection<PersonViewModel>();
                    foreach (var person in UsersDataBase.users)
                        _people.Add(new PersonViewModel(person));
                    break;
            }
        }

        private void PerformFiltering()
        {
            People = new ObservableCollection<PersonViewModel>();
            foreach (var person in UsersDataBase.users)
                _people.Add(new PersonViewModel(person));

            switch (_filter)
            {
                case "First name":
                    People = new ObservableCollection<PersonViewModel>(from person in _people
                                                                       where person.FirstName.Equals(FilterValue)
                                                                       select person);
                    break;
                case "Last name":
                    People = new ObservableCollection<PersonViewModel>(from person in _people
                                                                       where person.LastName.Equals(FilterValue)
                                                                       select person);
                    break;
                case "Email":
                    People = new ObservableCollection<PersonViewModel>(from person in _people
                                                                       where person.Email.Equals(FilterValue)
                                                                       select person);
                    break;
                case "Birth date":
                    People = new ObservableCollection<PersonViewModel>(from person in _people
                                                                       where person.BirthDate.Equals(FilterValue)
                                                                       select person);
                    break;
                case "Is B-day?":
                    People = new ObservableCollection<PersonViewModel>(from person in _people
                                                                       where person.IsBirthday
                                                                       select person);
                    break;
                case "Is adult?":
                    People = new ObservableCollection<PersonViewModel>(from person in _people
                                                                       where person.IsAdult
                                                                       select person);
                    break;
                case "Sun sign":
                    People = new ObservableCollection<PersonViewModel>(from person in _people
                                                                       where person.SunSign.Equals(FilterValue)
                                                                       select person);
                    break;
                case "Chinese sign":
                    People = new ObservableCollection<PersonViewModel>(from person in _people
                                                                       where person.ChineseSign.Equals(FilterValue)
                                                                       select person);
                    break;
                default:
                    People = new ObservableCollection<PersonViewModel>();
                    foreach (var person in UsersDataBase.users)
                        _people.Add(new PersonViewModel(person));
                    break;
            }
        }

        #endregion


        #region Commands

        public RelayCommand<object> CreatePersonCommand =>
            _createPersonCommand ??= new RelayCommand<object>(_ => AddPerson());

        public RelayCommand<object> EditPersonCommand =>
            _editPersonCommand ??= new RelayCommand<object>(_ => EditPerson());

        public RelayCommand<object> DeletePersonCommand =>
            _detelePersonCommand ??= new RelayCommand<object>(_ => DeletePerson());

        public RelayCommand<object> ApplyFilterCommand =>
            _applyFilters ??= new RelayCommand<object>(_ => PerformFiltering());



        private void AddPerson()
        {
            _goToCreateView?.Invoke();
        }

        private void EditPerson()
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show("First select a person to edit!");
                return;
            }
            _goToEditView?.Invoke();
        }

        private void DeletePerson()
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show("First select a person to delete");
                return;
            }

            UsersDataBase.users.Remove(SelectedPerson.Person);
            People.Remove(SelectedPerson);
            MessageBox.Show("Person successfully deleted!");
        }

        #endregion

        #region Interface
        public NavigationViewTypes ViewType => NavigationViewTypes.UserListViewModel;


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


    }
}
