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
    using View;
    using Repository;
    public class DetailVM : ViewModelBase
    {
        public DetailPage DetailPage { get; set; }
        public MainViewModel MainVM { get; set; }
        private Book _currentBook;
        public Book CurrentBook
        {
            get { return _currentBook; }
            set { _currentBook = value; RaisePropertyChanged("CurrentBook"); }
        }
#region Commands 
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
            if (DetailPage == null)
            {
                return;
            }
            string data = DetailPage.LinkAddTextBox.Text;
            DetailPage.LinkAddTextBox.Text = "";
            if (!IsValidLink(data))
            {
                return;
            }
            List<string> links = _currentBook.Links.ToList<string>();
            links.Add(data);
            _currentBook.Links = links.ToArray();
            RaisePropertyChanged("CurrentBook");
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
            if (DetailPage == null)
            {
                return;
            }
            object data = DetailPage.LinkListBox.SelectedItem;
            if (data == null)
            {
                return;
            }
            // know it's a string 
            string link = data.ToString();
            List<string> list = _currentBook.Links.ToList<string>();
            list.Remove(link);
            _currentBook.Links = list.ToArray();
            RaisePropertyChanged("CurrentBook");
        }
        private RelayCommand _editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(OpenScreen);
                }
                return _editCommand;
            }
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
        private void OpenScreen()
        {
            EditBook editBook = new EditBook();
            (editBook.DataContext as EditBookVM).CopiedBook =  CopyValue(_currentBook);
            (editBook.DataContext as EditBookVM).DetailViewModel = this;
            editBook.ShowDialog();
        }
        private Book CopyValue(Book bookToCopy)
        {
            Book returnValue = new Book();
            returnValue.ImageURL = bookToCopy.ImageURL;
            returnValue.Legends = bookToCopy.Legends;
            returnValue.Era = bookToCopy.Era;
            returnValue.Links = bookToCopy.Links;
            returnValue.Name = bookToCopy.Name;
            returnValue.Owned = bookToCopy.Owned;
            returnValue.ReleaseYear = bookToCopy.ReleaseYear;
            returnValue.Series = bookToCopy.Series;
            return returnValue;
        }
        private void Save()
        {
            BookReposoitory.SaveBooks();
        }

        private RelayCommand _previousBookCommand;
        public RelayCommand PreviousBookCommand
        {
            get 
            {
                if (_previousBookCommand == null)
                {
                    _previousBookCommand = new RelayCommand(GoToPreviousBook);
                }
                return _previousBookCommand; 
            }
        }

        private void GoToPreviousBook()
        {
            MainVM.PreviousBook(_currentBook);
        }

        private RelayCommand _nextBookCommand;
        public RelayCommand NextBookCommand
        {
            get
            {
                if (_nextBookCommand == null)
                {
                    _nextBookCommand = new RelayCommand(GoToNextBook);
                }
                return _nextBookCommand;
            }
        }

        private void GoToNextBook()
        {
            MainVM.NextBook(_currentBook);
        }

        #endregion Commands 

        public void Save(Book copiedBook )
        {
            BookReposoitory.SaveBooks(_currentBook, copiedBook);
            CurrentBook = copiedBook;
        }

        private bool IsValidLink(string url)
        {
            url = url.ToLower();
            return url.StartsWith("http://") || url.StartsWith("https://");
        }
    }
}
