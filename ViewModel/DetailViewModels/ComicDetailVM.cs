using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booklist.ViewModel.DetailViewModels
{
    using Model;
    using View.EditMediaPages;
    using Repository;
    using EditMediaViewModels;
    class ComicDetailVM : DetailVM
    {
        private Comic _currentComic;

        public Comic CurrentComic
        {
            get { return _currentComic; }
            set 
            {
                _currentComic = value;
                CurrentLink = null;
                LinkToAdd = "";
                RaisePropertyChanged("CurrentLink");
                RaisePropertyChanged("LinkToAdd");
                RaisePropertyChanged("CurrentComic");
            }
        }
        protected override void AddLink()
        {
            //string data = DetailPage.LinkAddTextBox.Text;
            //DetailPage.LinkAddTextBox.Text = "";
            if (!IsValidLink(LinkToAdd))
            {
                LinkToAdd = "";
                RaisePropertyChanged("CurrentLink");
                return;
            }
            List<string> links = _currentComic.Links.ToList<string>();
            links.Add(LinkToAdd);
            _currentComic.Links = links.ToArray();
            LinkToAdd = "";
            RaisePropertyChanged("CurrentComic");
            RaisePropertyChanged("LinkToAdd");
        }

        protected override void RemoveLink()
        {
            if (CurrentLink == null)
            {
                return;
            }
            // know it's a string 
            List<string> list = _currentComic.Links.ToList<string>();
            list.Remove(CurrentLink);
            _currentComic.Links = list.ToArray();
            RaisePropertyChanged("CurrentComic");
        }

        protected override void OpenScreen()
        {
            EditComic editBook = new EditComic();
            (editBook.DataContext as EditComicVM).CopiedComic = CopyValue(_currentComic);
            (editBook.DataContext as EditComicVM).DetailViewModel = this;
            editBook.ShowDialog();
        }

        private Comic CopyValue(Comic comicToCopy)
        {
            Comic returnValue = new Comic();
            returnValue.ImageURL = comicToCopy.ImageURL;
            returnValue.Legends = comicToCopy.Legends;
            returnValue.Era = comicToCopy.Era;
            returnValue.Links = comicToCopy.Links;
            returnValue.Name = comicToCopy.Name;
            returnValue.Owned = comicToCopy.Owned;
            returnValue.ReleaseYear = comicToCopy.ReleaseYear;
            returnValue.Series = comicToCopy.Series;
            returnValue.Writer = comicToCopy.Writer;
            returnValue.SeriesNumber = comicToCopy.SeriesNumber;
            returnValue.Time = comicToCopy.Time;
            returnValue.IssueFrom = comicToCopy.IssueFrom;
            returnValue.IssueTo = comicToCopy.IssueTo;
            return returnValue;
        }


        public void Save(Comic copiedComic)
        {
            RepositoryManager.GetInstance().ComicRepository.SaveMedia(_currentComic, copiedComic);
            CurrentComic = copiedComic;
            MainVM.UpdateOverviewVM();
        }

        protected override void Save()
        {
            RepositoryManager.GetInstance().ComicRepository.SaveMedia();
        }

        protected override void GoToPreviousBook()
        {
            MainVM.PreviousBook(_currentComic);
        }

        protected override void GoToNextBook()
        {
            MainVM.NextBook(_currentComic);
        }
    }
}
