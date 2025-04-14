using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using CommunityToolkit.Mvvm.Input;
using Lab4_PersonInfoList.Managers;
using Lab4_PersonInfoList.Models;
using Lab4_PersonInfoList.Navigation;
using Lab4_PersonInfoList.Services;

namespace Lab4_PersonInfoList.ViewModels
{
    class PersonListViewModel : INavigatable<PersonListNavigationType>, INotifyPropertyChanged
    {
        public PersonListNavigationType ViewModelType => PersonListNavigationType.PersonList;
        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand SaveCommand { get; }
        private Action _toPersonAddAction;
        private Action _toPersonEditAction;
        private MainViewModel _mainViewModel;
        private Person? _selectedPerson;
        public MainViewModel MainViewModel { get => _mainViewModel; }
        public bool AreChangesSaved
        {
            get => _mainViewModel.AreChangesSaved;
            set
            {
                _mainViewModel.AreChangesSaved = value;
            }
        }
        public Person? SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
                EditCommand.NotifyCanExecuteChanged();
                DeleteCommand.NotifyCanExecuteChanged();
            }
        }

        private ICollectionView _personsView;
        public ICollectionView PersonsView
        {
            get => _personsView;
            set
            {
                _personsView = value;
                OnPropertyChanged();
            }
        }

        private string _filterText;

        public string FilterText
        {
            get { return _filterText; }
            set
            { 
                _filterText = value;
                OnPropertyChanged();
                FilterAndSortData();
            }
        }
        public List<string> Fields { get; } = ["FirstName", "LastName", "Email", "BirthDate",
                                               "IsAdult", "SunSign", "ChineseSign", "IsBirthday"];

        private string _selectedField;
        public string SelectedField
        {
            get => _selectedField;
            set
            {
                _selectedField = value;
                OnPropertyChanged();
                FilterAndSortData();
            }
        }


        private string _sortingOrder;
        public string SortingOrder
        {
            get => _sortingOrder;
            set
            {
                _sortingOrder = value;
                OnPropertyChanged();
                FilterAndSortData();
            }
        }


        public PersonListViewModel(Action toPersonAddAction, Action toPersonEditAction, MainViewModel mainViewModel)
        {
            _toPersonAddAction = toPersonAddAction;
            _toPersonEditAction = toPersonEditAction;
            _mainViewModel = mainViewModel;
            PersonsView = CollectionViewSource.GetDefaultView(mainViewModel.Persons);

            EditCommand = new RelayCommand(EditPerson, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeletePerson, CanEditOrDelete);
            AddCommand = new RelayCommand(toPersonAddAction);
            SaveCommand = new RelayCommand(SavePersonsAsync);
        }

        private async void SavePersonsAsync()
        {
            await SavePersonsTaskAsync();
            MessageBox.Show("The changes have been saved.");
        }
        private async Task SavePersonsTaskAsync()
        {
            LoaderManager.Instance.ShowLoader();
            await PersonService.SaveAllPersonsAsync(_mainViewModel.Persons);
            AreChangesSaved = true;
            LoaderManager.Instance.HideLoader();
        }

        private bool CanEditOrDelete()
        {
            return SelectedPerson != null;
        }

        public async Task<bool> CancelClose()
        {
            if(!AreChangesSaved)
            {
                var result = MessageBox.Show("You have unsaved changes. Would you like to save them?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        await SavePersonsTaskAsync();
                        break;
                    case MessageBoxResult.Cancel:
                        return true;
                }
            }
            return false;
        }

        private void EditPerson()
        {
            _mainViewModel.SelectedPerson = SelectedPerson;
            _toPersonEditAction.Invoke();
        }

        private void DeletePerson()
        {
            _mainViewModel.Persons.Remove(SelectedPerson);
            FilterAndSortData();
        }

        private void FilterAndSortData()
        {
            var query = _mainViewModel.Persons.AsEnumerable();
            if (!string.IsNullOrEmpty(FilterText) && !string.IsNullOrEmpty(SelectedField))
            {
                var property = typeof(Person).GetProperty(SelectedField);
                query = query.Where(p =>
                {
                    var value = property.GetValue(p).ToString();
                    return value.Contains(FilterText);
                });
            }
            if (!string.IsNullOrEmpty(SelectedField) && !string.IsNullOrEmpty(SortingOrder))
            {
                var property = typeof(Person).GetProperty(SelectedField);
                query = SortingOrder == "Ascending" ? query.OrderBy(p => property.GetValue(p)) : query.OrderByDescending(p => property.GetValue(p));
            }
            PersonsView = CollectionViewSource.GetDefaultView(query.ToList());
        }

        public async Task InitializeAsync()
        {
            _filterText = "";
            SelectedField = "";
            SortingOrder = "";
            PersonsView = CollectionViewSource.GetDefaultView(_mainViewModel.Persons);
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
