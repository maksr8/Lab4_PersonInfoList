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
    class PersonEditViewModel : INavigatable<PersonListNavigationType>, INotifyPropertyChanged
    {
        private Action _toPersonListAction;
        private MainViewModel _mainViewModel;
        public PersonListNavigationType ViewModelType => PersonListNavigationType.PersonEdit;

        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand ToPersonListCommand { get; }
        public RelayCommand EditCommand { get; }

        public bool AnythingChanged { get; set; }

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


        public PersonEditViewModel(Action toPersonListAction, MainViewModel mainViewModel)
        {
            _toPersonListAction = toPersonListAction;
            _mainViewModel = mainViewModel;
            ToPersonListCommand = new RelayCommand(toPersonListAction);
            EditCommand = new RelayCommand(EditPersonAsync, CanExecute);
        }
        private async void EditPersonAsync()
        {
            LoaderManager.Instance.ShowLoader();
            try
            {
                Person person = await Task.Run(() => new Person(FirstName, LastName, Email, (DateTime)BirthDate));
                _mainViewModel.Persons.Remove(_mainViewModel.SelectedPerson);
                _mainViewModel.Persons.Add(person);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Editing failed: {ex.Message}");
                await InitializeAsync();
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
                    && BirthDate != null
                    && AnythingChanged;
        }
        private void UpdateCanExecute()
        {
            AnythingChanged = true;
            EditCommand.NotifyCanExecuteChanged();
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async Task InitializeAsync()
        {
            FirstName = _mainViewModel.SelectedPerson.FirstName;
            LastName = _mainViewModel.SelectedPerson.LastName;
            Email = _mainViewModel.SelectedPerson.Email;
            BirthDate = _mainViewModel.SelectedPerson.BirthDate;
            AnythingChanged = false;
        }
    }
}
