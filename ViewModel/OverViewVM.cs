using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Booklist.Model;
using Booklist.Repository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;



namespace Booklist.ViewModel
{
    using View;
    public class OverViewVM : ViewModelBase
    {
        public List<Book> Books { get; set; }
        public List<string>Eras { get; set; }
        public List<string> OwnedList { get; set; }
        public List<string> LegendsList { get; set; }
        public List<string> Series { get; set; }
        private string _selectedEra;
        public string SelectedEra
        {
            get { return _selectedEra; }
            set
            {
                _selectedEra = value;
                if (_selectedEra.Equals("All"))
                {
                    Books = BookReposoitory.GetBooks();
                    Series = BookReposoitory.GetSeriesFromEra("all",_ownedBool,_legendBool);
                }
                else
                {
                    //_ownedBool = "All";
                    //_legendBool = "All";
                    Series = BookReposoitory.GetSeriesFromEra(_selectedEra, _ownedBool, _legendBool);
                    RaisePropertyChanged("OwnedBool");
                    RaisePropertyChanged("LegendBool");

                }
                SelectedSeries = "All";
                RaisePropertyChanged("Series");
            }
        }
        private Book _selectedBook; 
        public Book SelectedBook
        { 
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
            }
        }

        private string _selectedSeries;
        public string SelectedSeries
        {
            get { return _selectedSeries; }
            set
            {
                if (value == null)
                {
                    Books = BookReposoitory.GetBooksFromEra(_selectedEra, "All","All");
                    RaisePropertyChanged("Books");
                    _selectedSeries = "None";
                    return;
                }
                _selectedSeries = value;
                Books = BookReposoitory.GetBooksFromSeries(_selectedSeries, _selectedEra, _ownedBool,_legendBool);
                RaisePropertyChanged("Books");
                RaisePropertyChanged("SelectedSeries");
            }
        }
        private string _ownedBool;
        public string OwnedBool
        {
            get { return _ownedBool; }
            set
            {
                _ownedBool = value;
                SelectedEra = SelectedEra;
            }
        }
        private string _legendBool;
        public string LegendBool
        {
            get { return _legendBool; }
            set
            {
                _legendBool = value;
                SelectedEra = SelectedEra;
            }
        }

        private RelayCommand _addBookCommand;

        public RelayCommand AddBookCommand
        {
            get 
            {
                if (_addBookCommand == null)
                {
                    _addBookCommand = new RelayCommand(OpenAddBookWindow);
                }
                return _addBookCommand; 
            }
        }

        private void OpenAddBookWindow()
        {
            AddBookWindow bookWindow = new AddBookWindow();
            (bookWindow.DataContext as AddBookVM).overViewViewModel = this;
            (bookWindow.DataContext as AddBookVM).BookWindow = bookWindow;
            Book newBook = new Book();
            if (_legendBool.Equals("True"))
            {
                newBook.Legends = true;
            }
            else if (_legendBool.Equals("False"))
            {
                newBook.Legends = false;
            }

            if (!SelectedEra.Equals("All"))
            {
                newBook.Era = SelectedEra;
            }

            if (!SelectedSeries.Equals("All"))
            {
                newBook.Series = SelectedSeries;
            }

            (bookWindow.DataContext as AddBookVM).NewBook = newBook;
            bookWindow.ShowDialog();
        }

        public OverViewVM()
        {
            OwnedList = new List<string> { "True", "False", "All" };
            LegendsList = new List<string> { "True", "False", "All" };
            _ownedBool = "All";
            _legendBool = "All";
            Books = BookReposoitory.GetBooks();
            Eras = BookReposoitory.GetEras();
            Eras.Add("All");
            SelectedEra = "All";

            //BookReposoitory.ScrapData();
        }
    }
}
