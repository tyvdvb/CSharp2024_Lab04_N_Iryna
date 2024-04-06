using CSharp2024_Lab04_N_Iryna.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharp2024_Lab04_N_Iryna.ViewModels
{
    internal enum NavigationViewTypes
    {
        UserListViewModel,
        CreateUserViewModel,
        EditUserViewModel
    }

    internal class NavigationViewModel : INotifyPropertyChanged
    {
        private INavigatable _currentViewModel;

        public NavigationViewModel()
        {
            Navigate(NavigationViewTypes.UserListViewModel);
        }

        public INavigatable CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                _currentViewModel = value;
                NotifyPropertyChanged();
            }
        }

        #region Navigation Methods

        internal void Navigate(NavigationViewTypes type)
        {
            if (CurrentViewModel != null && CurrentViewModel.ViewType == type)
                return;
            INavigatable viewModel = GetVM(type);
            if (viewModel == null)
                return;
            CurrentViewModel = viewModel;
        }

        private INavigatable GetVM(NavigationViewTypes type)
        {
            INavigatable viewModel;
            switch (type)
            {
                case NavigationViewTypes.UserListViewModel:
                    viewModel = new UserListViewModel(() => Navigate(NavigationViewTypes.CreateUserViewModel),
                        () => Navigate(NavigationViewTypes.EditUserViewModel));
                    break;
                case NavigationViewTypes.CreateUserViewModel:
                    viewModel = new CreateUserViewModel(() => Navigate(NavigationViewTypes.UserListViewModel));
                    break;
                case NavigationViewTypes.EditUserViewModel:
                    viewModel = new EditUserViewModel(() => Navigate(NavigationViewTypes.UserListViewModel));
                    break;
                default:
                    return null;
            }
            return viewModel;
        }

        #endregion

        #region PropChange

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
