using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
namespace Booklist.ViewModel.AddMediaViewModels
{
    using Model;
    using Repository;
    using View.AddMediaWindows;

    public class AddMusicAlbumVM : ViewModelBase
    {
        private MusicAlbum _newAlbum;
        public MusicAlbum NewAlbum
        {
            get { return _newAlbum; }
            set { _newAlbum = value; RaisePropertyChanged("NewAlbum"); }
        }
        public AddMusicAlbumWindow AlbumWindow { get; set; }
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
            RepositoryManager.GetInstance().MusicRepository.AddMedia(_newAlbum);
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
            if (AlbumWindow == null)
            {
                return;
            }
            string data = AlbumWindow.LinkAddTextBox.Text;
            AlbumWindow.LinkAddTextBox.Text = "";
            if (!IsValidLink(data))
            {
                return;
            }
            List<string> links = _newAlbum.Links.ToList<string>();
            links.Add(data);
            NewAlbum.Links = links.ToArray();
            AlbumWindow.LinkAddTextBox.Text = "";
            RaisePropertyChanged("NewAlbum");
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
            if (AlbumWindow == null)
            {
                return;
            }
            object data = AlbumWindow.LinkListBox.SelectedItem;
            if (data == null)
            {
                return;
            }
            // know it's a string 
            string link = data.ToString();
            List<string> list = _newAlbum.Links.ToList<string>();
            list.Remove(link);
            NewAlbum.Links = list.ToArray();
            RaisePropertyChanged("NewAlbum");
        }
        private bool IsValidLink(string url)
        {
            url = url.ToLower();
            return url.StartsWith("http://") || url.StartsWith("https://");
        }

    }
}
