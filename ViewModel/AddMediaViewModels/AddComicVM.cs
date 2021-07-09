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
    using View.AddMediaWindows;
    public class AddComicVM : ViewModelBase
    {
        private Comic _newComic;
        public Comic NewComic { get { return _newComic; }
            set { _newComic = value; RaisePropertyChanged("NewComic"); } }
        public AddComicWindow ComicWindow { get; set; }
        public OverViewVM OverViewViewModel { get; set; }
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
            RepositoryManager.GetInstance().ComicRepository.AddMedia(_newComic);
            OverViewViewModel.SelectedSeries = OverViewViewModel.SelectedSeries; //this will update the list
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
            if (ComicWindow == null)
            {
                return;
            }
            string data = ComicWindow.LinkAddTextBox.Text;
            ComicWindow.LinkAddTextBox.Text = "";
            if (!IsValidLink(data))
            {
                return;
            }
            List<string> links = _newComic.Links.ToList<string>();
            links.Add(data);
            _newComic.Links = links.ToArray();
            RaisePropertyChanged("NewComic");
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
            if (ComicWindow == null)
            {
                return;
            }
            object data = ComicWindow.LinkListBox.SelectedItem;
            if (data == null)
            {
                return;
            }
            // know it's a string 
            string link = data.ToString();
            List<string> list = _newComic.Links.ToList<string>();
            list.Remove(link);
            _newComic.Links = list.ToArray();
            RaisePropertyChanged("NewComic");
        }
        private bool IsValidLink(string url)
        {
            url = url.ToLower();
            return url.StartsWith("http://") || url.StartsWith("https://");
        }

    }
}
