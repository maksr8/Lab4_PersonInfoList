using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lab4_PersonInfoList.Navigation;

namespace Lab4_PersonInfoList.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private List<INavigatable<PersonListNavigationType>> _viewModels = [];
        private INavigatable<PersonListNavigationType> _currentViewModel;
        public event PropertyChangedEventHandler? PropertyChanged;

        public INavigatable<PersonListNavigationType> CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Navigate(PersonListNavigationType.PersonList);
        }

        internal void Navigate(PersonListNavigationType type)
        {
            if (CurrentViewModel != null && CurrentViewModel.ViewModelType == type)
                return;

            INavigatable<PersonListNavigationType>? viewModel = GetViewModel(type);
            if (viewModel != null)
            {
                CurrentViewModel = viewModel;
                CurrentViewModel.IsEnabled = true;
            }
        }

        private INavigatable<PersonListNavigationType>? GetViewModel(PersonListNavigationType type)
        {
            INavigatable<PersonListNavigationType>? viewModel = _viewModels.FirstOrDefault(vm => vm.ViewModelType == type);

            if (viewModel == null)
            {
                switch (type)
                {
                    case PersonListNavigationType.PersonAdd:
                        viewModel = new PersonAddViewModel(() => Navigate(PersonListNavigationType.PersonList), this);
                        break;
                    case PersonListNavigationType.PersonEdit:
                        viewModel = new PersonEditViewModel(() => Navigate(PersonListNavigationType.PersonList), this);
                        break;
                    default:
                        return null;
                }
                _viewModels.Add(viewModel);
            }
            return viewModel;
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
