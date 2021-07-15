using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booklist.ViewModel.DetailViewModels
{
    using EditMediaViewModels;
    using Model;
    using View;
    using Repository;
    public class BookDetailVM : DetailVM
    {
        private Book _currentBook;
        public Book CurrentBook
        {
            get { return _currentBook; }
            set 
            { 
                _currentBook = value;
                CurrentLink = null;
                LinkToAdd = "";
                RaisePropertyChanged("CurrentLink");
                RaisePropertyChanged("LinkToAdd");
                RaisePropertyChanged("CurrentBook"); 
            }
        }

        protected override void AddLink()
        {
            if (!IsValidLink(LinkToAdd))
            {
                LinkToAdd = "";
                RaisePropertyChanged("CurrentLink");
                return;
            }
            List<string> links = _currentBook.Links.ToList<string>();
            links.Add(LinkToAdd);
            _currentBook.Links = links.ToArray();
            LinkToAdd = "";
            RaisePropertyChanged("CurrentBook");
            RaisePropertyChanged("LinkToAdd");
        }

        protected override void RemoveLink()
        {
            if (CurrentLink == null)
            {
                return;
            }
            // know it's a string 
            List<string> list = _currentBook.Links.ToList<string>();
            list.Remove(CurrentLink);
            _currentBook.Links = list.ToArray();
            RaisePropertyChanged("CurrentBook");
        }

        protected override void OpenScreen()
        {
            EditBook editBook = new EditBook();
            (editBook.DataContext as EditBookVM).CopiedBook = CopyValue(_currentBook);
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
            returnValue.Writer = bookToCopy.Writer;
            return returnValue;
        }

        public void Save(Book copiedBook)
        {
            RepositoryManager.GetInstance().BookReposoitory.SaveMedia(_currentBook, copiedBook);
            CurrentBook = copiedBook;
            MainVM.UpdateOverviewVM();
        }

        protected override void Save()
        {
            RepositoryManager.GetInstance().BookReposoitory.SaveMedia();
        }

        protected override void GoToPreviousBook()
        {
            MainVM.PreviousBook(_currentBook);
        }

        protected override void GoToNextBook()
        {
            MainVM.NextBook(_currentBook);
        }

    }
}
