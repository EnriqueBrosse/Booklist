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
    using View.AddMediaWindows;
    using View;
    using ViewModel.AddMediaViewModels;
    public class OverViewVM : ViewModelBase
    {
        public List<BaseMedia> Books { get; set; }
        public List<string>Eras { get; set; }
        public List<string> OwnedList { get; set; }
        public List<string> LegendsList { get; set; }
        public List<string> Series { get; set; }
        public List<string> Repositories { get; set; }
        private string _currentRepository;
        public string CurrentRepository
        {
            get { return _currentRepository; }
            set
            {
                _currentRepository = value;
                RepositoryManager repManager = RepositoryManager.GetInstance();
                repManager.SetCurrentRepository(_currentRepository);
                RaisePropertyChanged("CurrentRepository");
                SelectedEra = "All";
            }
        }

        private string _selectedEra;
        public string SelectedEra
        {
            get { return _selectedEra; }
            set
            {
                _selectedEra = value;
                RepositoryManager repManager = RepositoryManager.GetInstance();

                // not the ideal way but it's either doing it here or doing it in the base class
                // and it's a lot of repeated code but it's either here a mass class or in the base class
                if (repManager.CurrentRepository is BookReposoitory)
                {
                    BookReposoitory tempRepository = repManager.CurrentRepository as BookReposoitory;
                    Series = tempRepository.GetSeriesFromEra(_selectedEra, _ownedBool, _legendBool);
                }
                else if (repManager.CurrentRepository is ComicRepository)
                {
                    ComicRepository tempRepository = repManager.CurrentRepository as ComicRepository;
                    Series = tempRepository.GetSeriesFromEra(_selectedEra, _ownedBool, _legendBool);
                }
                else if (repManager.CurrentRepository is MusicRepository)
                {
                    MusicRepository tempRepository = repManager.CurrentRepository as MusicRepository;
                    Series = tempRepository.GetSeriesFromEra(_selectedEra, _ownedBool, _legendBool);
                }
                SelectedSeries = "All";
                RaisePropertyChanged("Series");
                RaisePropertyChanged("Books");
            }
        }
        private BaseMedia _selectedBook; 
        public BaseMedia SelectedBook
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
                RepositoryManager repManager = RepositoryManager.GetInstance();

                // not the ideal way but it's either doing it here or doing it in the base class
                // and it's a lot of repeated code but it's either here a mass class or in the base class
                _selectedSeries = value;
                if (repManager.CurrentRepository is BookReposoitory)
                {
                    BookReposoitory tempRepository = repManager.CurrentRepository as BookReposoitory;
                    if (value == null)
                    {
                        Books = tempRepository.ConvertToBase(tempRepository.GetMediaFromEra(_selectedEra, "All", "All"));
                        RaisePropertyChanged("Books");
                        _selectedSeries = "None";
                        return;
                    }
                    Books = tempRepository.ConvertToBase(tempRepository.GetMediaFromSeries(_selectedSeries, _selectedEra, _ownedBool, _legendBool));
                }
                else if (repManager.CurrentRepository is ComicRepository)
                {
                    ComicRepository tempRepository = repManager.CurrentRepository as ComicRepository;
                    if (value == null)
                    {
                        Books = tempRepository.ConvertToBase(tempRepository.GetMediaFromEra(_selectedEra, "All", "All"));
                        RaisePropertyChanged("Books");
                        _selectedSeries = "None";
                        return;
                    }
                    Books = tempRepository.ConvertToBase(tempRepository.GetMediaFromSeries(_selectedSeries, _selectedEra, _ownedBool, _legendBool));
                }
                else if (repManager.CurrentRepository is MusicRepository)
                {
                    MusicRepository tempRepository = repManager.CurrentRepository as MusicRepository;
                    if (value == null)
                    {
                        Books = tempRepository.ConvertToBase(tempRepository.GetMediaFromEra(_selectedEra, "All", "All"));
                        RaisePropertyChanged("Books");
                        _selectedSeries = "None";
                        return;
                    }
                    Books = tempRepository.ConvertToBase(tempRepository.GetMediaFromSeries(_selectedSeries, _selectedEra, _ownedBool, _legendBool));
                }


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
                SelectedSeries = SelectedSeries;
            }
        }
        private string _legendBool;
        public string LegendBool
        {
            get { return _legendBool; }
            set
            {
                _legendBool = value;
                SelectedSeries = SelectedSeries;
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
            RepositoryManager repositoryManager = RepositoryManager.GetInstance();
            if (repositoryManager.CurrentRepository == repositoryManager.BookReposoitory)
            {
                AddBookWindow bookWindow = new AddBookWindow();
                (bookWindow.DataContext as AddBookVM).OverViewViewModel = this;
                (bookWindow.DataContext as AddBookVM).BookWindow = bookWindow;
                Book newBook = new Book();

                if (_ownedBool.Equals("True"))
                {
                    newBook.Owned = true;
                }
                else if (_ownedBool.Equals("False"))
                {
                    newBook.Owned = false;
                }

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
            else if (repositoryManager.CurrentRepository == repositoryManager.ComicRepository )
            {
                AddComicWindow comicWindow = new AddComicWindow();
                (comicWindow.DataContext as AddComicVM).OverViewViewModel = this;
                (comicWindow.DataContext as AddComicVM).ComicWindow = comicWindow;
                Comic newComic = new Comic();

                if (_ownedBool.Equals("True"))
                {
                    newComic.Owned = true;
                }
                else if (_ownedBool.Equals("False"))
                {
                    newComic.Owned = false;
                }

                if (_legendBool.Equals("True"))
                {
                    newComic.Legends = true;
                }
                else if (_legendBool.Equals("False"))
                {
                    newComic.Legends = false;
                }

                if (!SelectedEra.Equals("All"))
                {
                    newComic.Era = SelectedEra;
                }

                if (!SelectedSeries.Equals("All"))
                {
                    newComic.Series = SelectedSeries;
                }
                if (!SelectedEra.Equals("All"))
                {
                    newComic.Era = SelectedEra;
                }
                
                (comicWindow.DataContext as AddComicVM).NewComic = newComic;
                comicWindow.ShowDialog();
            }
            else if (repositoryManager.CurrentRepository == repositoryManager.MusicRepository)
            {
                AddMusicAlbumWindow albumWindow = new AddMusicAlbumWindow();
                (albumWindow.DataContext as AddMusicAlbumVM).OverViewViewModel = this;
                (albumWindow.DataContext as AddMusicAlbumVM).AlbumWindow = albumWindow;
                MusicAlbum album = new MusicAlbum();
                (albumWindow.DataContext as AddMusicAlbumVM).NewAlbum = album;
                albumWindow.ShowDialog();
            }
        }

        public OverViewVM()
        {
            RepositoryManager.GetInstance().LoadRepositories();

            OwnedList = new List<string> { "True", "False", "All" };
            LegendsList = new List<string> { "True", "False", "All" };
            _ownedBool = "All";
            _legendBool = "All";
            Eras = RepositoryManager.GetInstance().BookReposoitory.GetEras();
            Eras.Add("All");
            _selectedEra = "All";
            Repositories = RepositoryManager.GetInstance().GetRepositoryNames();
            CurrentRepository = Repositories[1];
            RaisePropertyChanged("Repositories");


            //BookReposoitory.ScrapData();
        }

        public BaseMedia Next(BaseMedia book)
        {
            int index = -1;
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i] == book)
                {
                    index = i;
                    break;
                }
            }
            if (index == (Books.Count - 1))
            {
                index = 0;
            }
            else if (index == -1)
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].Name.Equals(book.Name))
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
            return Books[index];
        }

        public BaseMedia Previous(BaseMedia book)
        {
            int index = -1;
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i] == book)
                {
                    index = i;
                    break;
                }
            }
            if (index == 0)
            {
                index = Books.Count - 1;
            }
            else if (index == -1)
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].Name.Equals(book.Name))
                    {
                        index = i;
                        break;
                    }
                }
                if (index == -1)
                {
                    index = Books.Count - 1;
                }
            }
            else
            {
                index--;
            }
            return Books[index];
        }
    }
}
