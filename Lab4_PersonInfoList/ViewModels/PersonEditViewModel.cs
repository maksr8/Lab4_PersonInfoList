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
        private Action _toPersonListAction;
        private MainViewModel _mainViewModel;

        public PersonListNavigationType ViewModelType => PersonListNavigationType.PersonEdit;

        public event PropertyChangedEventHandler? PropertyChanged;

        public PersonEditViewModel(Action toPersonListAction, MainViewModel mainViewModel)
        {
            _toPersonListAction = toPersonListAction;
            _mainViewModel = mainViewModel;
        }


        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async Task InitializeAsync()
        {
            
        }
    }
}
