using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4_PersonInfoList.ViewModels;

namespace Lab4_PersonInfoList.Navigation
{
    enum PersonListNavigationType
    {
        PersonAdd,
        PersonEdit,
        PersonList
    }
    interface INavigatable<TEnum> where TEnum : Enum
    {
        public TEnum ViewModelType { get; }

        Task InitializeAsync();

    }
}
