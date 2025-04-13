using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Lab4_PersonInfoList.Models;
using Lab4_PersonInfoList.Navigation;

namespace Lab4_PersonInfoList.ViewModels
{
    class PersonAddViewModel : INavigatable<PersonListNavigationType>, INotifyPropertyChanged
    {
        public PersonListNavigationType ViewModelType => PersonListNavigationType.PersonAdd;

        public RelayCommand ToPersonListCommand { get; }

        private bool _isEnabled = true;
        private Visibility _loaderVisibility = Visibility.Collapsed;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        private string _firstName = "";
        private string _lastName = "";
        private string _email = "";
        private DateTime? _birthDate;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                UpdateCanExecute();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                UpdateCanExecute();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                UpdateCanExecute();
            }
        }

        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                UpdateCanExecute();
            }
        }

        private Action _toPersonInfoOutput;
        private MainViewModel _mainViewModel;

        public PersonAddViewModel(Action toPersonInfoOutput, MainViewModel mainViewModel)
        {
            _toPersonInfoOutput = toPersonInfoOutput;
            _mainViewModel = mainViewModel;
            ToPersonListCommand = new RelayCommand(proceedCheck, CanExecute);
        }

        private async void proceedCheck()
        {
            IsEnabled = false;
            LoaderVisibility = Visibility.Visible;

            try
            {
                Person person = await Task.Run(() => new Person(FirstName, LastName, Email, (DateTime)BirthDate));
                //_personListViewModel.Person = person;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sign in failed: {ex.Message}");
                IsEnabled = true;
                LoaderVisibility = Visibility.Collapsed;
                return;
            }
            _toPersonInfoOutput?.Invoke();


            LoaderVisibility = Visibility.Collapsed;
            
        }

        private bool CanExecute()
        {
            return !String.IsNullOrWhiteSpace(FirstName)
                    && !String.IsNullOrWhiteSpace(LastName)
                    && !String.IsNullOrWhiteSpace(Email)
                    && BirthDate != null;
        }
        private void UpdateCanExecute()
        {
            ToPersonListCommand.NotifyCanExecuteChanged();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
