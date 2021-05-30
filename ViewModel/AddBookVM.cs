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
    using View;
    public class AddBookVM : ViewModelBase
    {
        private Book _newBook;
        public Book NewBook { get { return _newBook; }
            set { _newBook = value; RaisePropertyChanged("NewBook"); } }
        public AddBookWindow BookWindow { get; set; }
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

        private RelayCommand _addLinkCommand;
        public RelayCommand AddLinkCommand
        {
            get
            {
                if (_addLinkCommand == null)
                {
                    _addLinkCommand = new RelayCommand(AddLink);
                }
                return _addLinkCommand;
            }
        }
        void AddLink()
        {
            if (BookWindow == null)
            {
                return;
            }
            string data = BookWindow.LinkAddTextBox.Text;
            BookWindow.LinkAddTextBox.Text = "";
            if (!IsValidLink(data))
            {
                return;
            }
            List<string> links = _newBook.Links.ToList<string>();
            links.Add(data);
            NewBook.Links = links.ToArray();
            RaisePropertyChanged("CurrentBook");
            BookWindow.LinkAddTextBox.Text = "";
            RaisePropertyChanged("NewBook");
        }
        private RelayCommand _removeLinkCommand;
        public RelayCommand RemoveLinkCommand
        {
            get
            {
                if (_removeLinkCommand == null)
                {
                    _removeLinkCommand = new RelayCommand(RemoveLink);
                }
                return _removeLinkCommand;
            }
        }
        void RemoveLink()
        {
            if (BookWindow == null)
            {
                return;
            }
            object data = BookWindow.LinkListBox.SelectedItem;
            if (data == null)
            {
                return;
            }
            // know it's a string 
            string link = data.ToString();
            List<string> list = _newBook.Links.ToList<string>();
            list.Remove(link);
            NewBook.Links = list.ToArray();
            RaisePropertyChanged("NewBook");
        }
        private bool IsValidLink(string url)
        {
            url = url.ToLower();
            return url.StartsWith("http://") || url.StartsWith("https://");
        }

    }
}
