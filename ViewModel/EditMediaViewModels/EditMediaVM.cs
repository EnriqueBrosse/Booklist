using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Booklist.ViewModel.EditMediaViewModels
{
    public class EditMediaVM : ViewModelBase
    {
        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(Save);
                }
                return _saveCommand;
            }
        }
        protected virtual void Save()
        {
        }

    }
}
