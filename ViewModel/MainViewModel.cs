using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Controls;
using Booklist.Model;
using Booklist.View;
using Booklist.View.DetailPages;
using Booklist.Repository;
using Booklist.ViewModel.DetailViewModels;
using System.Diagnostics;
using System.Windows.Navigation;


namespace Booklist.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        public OverViewPage MainPage { get; set; } = new OverViewPage();
        public BookDetailPage BookPage { get; set; } = new BookDetailPage();
        public ComicDetailPage ComicPage { get; set; } = new ComicDetailPage();
        public MusicAlbumDetailPage AlbumPage { get; set; } = new MusicAlbumDetailPage();
        private Page _currentPage;

        public string CommandText
        {
            get
            {
                if (_currentPage is OverViewPage)
                {
                    return "Go to details menu";
                }
                return "Go to overview page";
            }
        }

        private RelayCommand _switchPageCommand;
        public RelayCommand SwitchPageCommand
        {
            get
            {
                if (_switchPageCommand == null)
                {
                    _switchPageCommand = new RelayCommand(SwitchPage);
                }
                return _switchPageCommand;
            }
        }
        void SwitchPage()
        {
            if (CurrentPage is OverViewPage)
            {
                BaseMedia media = (MainPage.DataContext as OverViewVM).SelectedBook;
                if (media is Book)
                {
                    Book book = (MainPage.DataContext as OverViewVM).SelectedBook as Book;
                    if (book == null)
                    {
                        return;
                    }
                    (BookPage.DataContext as BookDetailVM).CurrentBook = book;
                    CurrentPage = BookPage;
                }
                else if (media is Comic)
                {
                    Comic comic = (MainPage.DataContext as OverViewVM).SelectedBook as Comic;
                    if (comic == null)
                    {
                        return;
                    }
                    (ComicPage.DataContext as ComicDetailVM).CurrentComic = comic;
                    CurrentPage = ComicPage;
                }
                else if (media is MusicAlbum)
                {
                    MusicAlbum musicAlbum = (MainPage.DataContext as OverViewVM).SelectedBook as MusicAlbum;
                    if (musicAlbum == null)
                    {
                        return;
                    }
                    (AlbumPage.DataContext as MusicAlbumDetailVM).CurrentMusicAlbum = musicAlbum;
                    CurrentPage = AlbumPage;
                }
            }
            else
            {
                CurrentPage = MainPage;
            }
            RaisePropertyChanged("CommandText");
        }

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged("CurrentPage");
            }
        }
        public MainViewModel()
        {
            CurrentPage = MainPage;
            (BookPage.DataContext as DetailVM).MainVM = this;
            (ComicPage.DataContext as DetailVM).MainVM = this;
            (AlbumPage.DataContext as DetailVM).MainVM = this;
            //WebsiteScrapper.ScrapeWebsiteData("https://www.bol.com/nl/p/dark-journey/1001004001617987/?bltgh=p2ERiDb6PAwE1zVOSb9EBQ.2_9.10.ProductTitle");
        }

        public void NextBook(BaseMedia book)
        {
            BaseMedia media = (MainPage.DataContext as OverViewVM).Next(book);
            if (media is Book)
            {
                Book currentBook = media as Book;
                (BookPage.DataContext as BookDetailVM).CurrentBook = currentBook;
            }
            else if (media is Comic)
            {
                Comic currentComic = media as Comic;
                (ComicPage.DataContext as ComicDetailVM).CurrentComic = currentComic;
            }
            else if (media is MusicAlbum)
            {
                MusicAlbum musicAlbum = media as MusicAlbum;
                (AlbumPage.DataContext as MusicAlbumDetailVM).CurrentMusicAlbum = musicAlbum;
            }
        }

        public void PreviousBook(BaseMedia book)
        {
            BaseMedia media = (MainPage.DataContext as OverViewVM).Previous(book);
            if (media is Book)
            {
                Book currentBook = media as Book;
                (BookPage.DataContext as BookDetailVM).CurrentBook = currentBook;
            }
            else if (media is Comic)
            {
                Comic currentComic = media as Comic;
                (ComicPage.DataContext as ComicDetailVM).CurrentComic = currentComic;
            }
            else if (media is MusicAlbum)
            {
                MusicAlbum musicAlbum = media as MusicAlbum;
                (AlbumPage.DataContext as MusicAlbumDetailVM).CurrentMusicAlbum = musicAlbum;
            }
        }
        public void UpdateOverviewVM()
        {
            OverViewVM overview = (MainPage.DataContext as OverViewVM);
            string owned = overview.OwnedBool;
            for (int i = 0; i < overview.OwnedList.Count; i++)
            {
                if (!owned.Equals(overview.OwnedList[i]))
                {
                    overview.OwnedBool = overview.OwnedList[i];
                    break;
                }
            }
            overview.OwnedBool = owned; 
        }
    }
}
