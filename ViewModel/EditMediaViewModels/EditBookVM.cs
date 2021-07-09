using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Booklist.ViewModel
{
    using Model;
    public class EditBookVM : ViewModelBase
    {
        private Book _copiedBook;
        public DetailVM DetailViewModel{get;set;}
        public Book CopiedBook
        {
            get { return _copiedBook; }
            set { _copiedBook = value; RaisePropertyChanged("CopiedBook"); }
        }

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

        private void Save()
        {
            DetailViewModel.Save(CopiedBook);
        }
    }
}
