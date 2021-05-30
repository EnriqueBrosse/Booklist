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
    using Repository;
    public class AddBookVM : ViewModelBase
    {
        private Book _newBook;
        public Book NewBook { get { return _newBook; }
            set { _newBook = value; RaisePropertyChanged("NewBook"); } }
        public OverViewVM overViewViewModel { get; set; }

        private RelayCommand _saveBookCommand;

        public RelayCommand SaveBookCommand
        {
            get 
            {
                if (_saveBookCommand == null)
                {
                    _saveBookCommand = new RelayCommand(SaveBook);
                }
                return _saveBookCommand;
            }
        }
        private void SaveBook()
        {
            BookReposoitory.AddBook(_newBook);
            overViewViewModel.SelectedSeries = overViewViewModel.SelectedSeries; //this will update the list
        }
    }
}
