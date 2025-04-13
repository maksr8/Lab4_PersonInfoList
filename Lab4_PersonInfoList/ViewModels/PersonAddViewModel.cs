using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Lab4_PersonInfoList.Managers;
using Lab4_PersonInfoList.Models;
using Lab4_PersonInfoList.Navigation;

namespace Lab4_PersonInfoList.ViewModels
{
    class PersonAddViewModel : INavigatable<PersonListNavigationType>, INotifyPropertyChanged
    {
        public PersonListNavigationType ViewModelType => PersonListNavigationType.PersonAdd;

        public RelayCommand ToPersonListCommand { get; }
        public RelayCommand AddCommand { get; }

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

        private Action _toPersonListAction;
        private MainViewModel _mainViewModel;

        public PersonAddViewModel(Action ToPersonListAction, MainViewModel mainViewModel)
        {
            _toPersonListAction = ToPersonListAction;
            _mainViewModel = mainViewModel;
            ToPersonListCommand = new RelayCommand(ToPersonListAction);
            AddCommand = new RelayCommand(AddPersonAsync, CanExecute);
        }

        private async void AddPersonAsync()
        {
            LoaderManager.Instance.ShowLoader();
            try
            {
                Person person = await Task.Run(() => new Person(FirstName, LastName, Email, (DateTime)BirthDate));
                _mainViewModel.AddPerson(person);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Adding failed: {ex.Message}");
                LoaderManager.Instance.HideLoader();
                return;
            }
            _toPersonListAction?.Invoke();
            LoaderManager.Instance.HideLoader();
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
        public async Task InitializeAsync()
        {
            
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
