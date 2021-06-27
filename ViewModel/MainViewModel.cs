﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Controls;
using Booklist.Model;
using Booklist.View;


using System.Diagnostics;
using System.Windows.Navigation;



namespace Booklist.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public OverViewPage MainPage { get; set; } = new OverViewPage();
        public DetailPage BookPage { get; set; } = new DetailPage();
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
                Book book = (MainPage.DataContext as OverViewVM).SelectedBook;
                if (book == null)
                {
                    return;
                }
                (BookPage.DataContext as DetailVM).CurrentBook = book;
                CurrentPage = BookPage;
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
            (BookPage.DataContext as DetailVM).DetailPage = BookPage;
            (BookPage.DataContext as DetailVM).MainVM = this;
            //WebsiteScrapper.ScrapeWebsiteData("https://www.bol.com/nl/p/dark-journey/1001004001617987/?bltgh=p2ERiDb6PAwE1zVOSb9EBQ.2_9.10.ProductTitle");
        }

        public void NextBook(Book book)
        {
            List<Book> books = (MainPage.DataContext as OverViewVM).Books;
            int index = -1;
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i] == book)
                {
                    index = i;
                    break;
                }
            }
            if (index == (books.Count -1))
            {
                index = 0;
            }
            else if (index == -1)
            {
                for (int i = 0; i < books.Count; i++)
                {
                    if (books[i].Name.Equals(book.Name))
                    {
                        index = i;
                        break;
                    }
                }
                if (index == -1)
                {
                    index = 0;
                }
            }
            else
            {
                index++; 
            }

            (BookPage.DataContext as DetailVM).CurrentBook = books[index];
        }

        public void PreviousBook(Book book)
        {
            List<Book> books = (MainPage.DataContext as OverViewVM).Books;
            int index = -1;
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i] == book)
                {
                    index = i;
                    break;
                }
            }
            if (index == 0)
            {
                index = books.Count - 1;
            }
            else if (index == -1)
            {
                for (int i = 0; i < books.Count; i++)
                {
                    if (books[i].Name.Equals(book.Name))
                    {
                        index = i;
                        break;
                    }
                }
                if (index == -1)
                {
                    index = books.Count - 1;
                }
            }
            else
            {
                index--;
            }
            (BookPage.DataContext as DetailVM).CurrentBook = books[index];
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
