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
    class PersonListViewModel : INavigatable<PersonListNavigationType>, INotifyPropertyChanged
    {
        public PersonListNavigationType ViewModelType => PersonListNavigationType.PersonList;
        public event PropertyChangedEventHandler? PropertyChanged;

        private Action _toPersonAddAction;
        private Action _toPersonEditAction;
        private MainViewModel _mainViewModel;

        public PersonListViewModel(Action toPersonAddAction, Action toPersonEditAction, MainViewModel mainViewModel)
        {
            _toPersonAddAction = toPersonAddAction;
            _toPersonEditAction = toPersonEditAction;
            _mainViewModel = mainViewModel;
        }


        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
