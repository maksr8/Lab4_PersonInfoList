using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lab4_PersonInfoList.Navigation;

namespace Lab4_PersonInfoList.ViewModels
{
    class PersonEditViewModel : INavigatable<PersonListNavigationType>, INotifyPropertyChanged
    {
        private Action value;
        private MainViewModel mainViewModel;

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

        public PersonEditViewModel(Action value, MainViewModel mainViewModel)
        {
            this.value = value;
            this.mainViewModel = mainViewModel;
        }

        public PersonListNavigationType ViewModelType => PersonListNavigationType.PersonEdit;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
