﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class MainViewModel : INotifyPropertyChanged, ILoaderOwner
    {
        private List<INavigatable<PersonListNavigationType>> _viewModels = [];
        private INavigatable<PersonListNavigationType> _currentViewModel;
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _isEnabled = true;
        private Visibility _loaderVisibility = Visibility.Collapsed;

        private ObservableCollection<Person> _persons;
        

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set
            {
                _persons = value;
                OnPropertyChanged(); //try to remove
            }
        }

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
            LoaderManager.Instance.Initialize(this);
            Persons = new ObservableCollection<Person>();
            NavigateAsync(PersonListNavigationType.PersonList);
        }

        internal async Task NavigateAsync(PersonListNavigationType type)
        {
            if (CurrentViewModel != null && CurrentViewModel.ViewModelType == type)
                return;

            INavigatable<PersonListNavigationType>? viewModel = GetViewModel(type);
            if (viewModel != null)
            {
                CurrentViewModel = viewModel;
                LoaderManager.Instance.ShowLoader();
                await CurrentViewModel.InitializeAsync();
                LoaderManager.Instance.HideLoader();
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
                        viewModel = new PersonAddViewModel(() => NavigateAsync(PersonListNavigationType.PersonList), this);
                        break;
                    case PersonListNavigationType.PersonEdit:
                        viewModel = new PersonEditViewModel(() => NavigateAsync(PersonListNavigationType.PersonList), this);
                        break;
                    case PersonListNavigationType.PersonList:
                        viewModel = new PersonListViewModel(() => NavigateAsync(PersonListNavigationType.PersonAdd), () => NavigateAsync(PersonListNavigationType.PersonEdit), this);
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

        public void AddPerson(Person person)
        {
            Persons.Add(person);
        }
    }
}
