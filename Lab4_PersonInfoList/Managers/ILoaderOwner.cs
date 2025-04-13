using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab4_PersonInfoList.Managers
{
    internal interface ILoaderOwner
    {
        public bool IsEnabled { get; set; }

        public Visibility LoaderVisibility { get; set; }
    }
}
