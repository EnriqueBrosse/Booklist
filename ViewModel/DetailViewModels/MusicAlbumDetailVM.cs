using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booklist.ViewModel.DetailViewModels
{
    using EditMediaViewModels;
    using Model;
    using View.EditMediaPages;
    using Repository;
    class MusicAlbumDetailVM : DetailVM
    {
        private MusicAlbum _currentMusicAlbum;

        public MusicAlbum CurrentMusicAlbum
        {
            get { return _currentMusicAlbum; }
            set 
            {
                _currentMusicAlbum = value;
                CurrentLink = null;
                LinkToAdd = "";
                RaisePropertyChanged("CurrentLink");
                RaisePropertyChanged("LinkToAdd");
                RaisePropertyChanged("CurrentMusicAlbum");
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
            List<string> links = _currentMusicAlbum.Links.ToList<string>();
            links.Add(LinkToAdd);
            _currentMusicAlbum.Links = links.ToArray();
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
            List<string> list = _currentMusicAlbum.Links.ToList<string>();
            list.Remove(CurrentLink);
            _currentMusicAlbum.Links = list.ToArray();
            RaisePropertyChanged("CurrentBook");
        }

        protected override void OpenScreen()
        {
            EditMusicAlbum editMusicAlbum = new EditMusicAlbum();
            (editMusicAlbum.DataContext as EditMusicAlbumVM).CopiedMusicAlbum = CopyValue(_currentMusicAlbum);
            (editMusicAlbum.DataContext as EditMusicAlbumVM).DetailViewModel = this;
            editMusicAlbum.ShowDialog();
        }


        private MusicAlbum CopyValue(MusicAlbum valueToCopy)
        {
            MusicAlbum returnValue = new MusicAlbum();
            returnValue.Artist = valueToCopy.Artist;
            returnValue.Era = valueToCopy.Era;
            returnValue.ImageURL = valueToCopy.ImageURL;
            returnValue.Legends = valueToCopy.Legends;
            returnValue.Links = valueToCopy.Links;
            returnValue.Name = valueToCopy.Name;
            returnValue.Owned = valueToCopy.Owned;
            returnValue.ReleaseYear = valueToCopy.ReleaseYear;
            returnValue.Time = valueToCopy.Time;
            return returnValue;
        }

        public void Save(MusicAlbum copiedMusicAlbum)
        {
            RepositoryManager.GetInstance().MusicRepository.SaveMedia(_currentMusicAlbum, copiedMusicAlbum);
            CurrentMusicAlbum = copiedMusicAlbum;
            MainVM.UpdateOverviewVM();
        }

        protected override void Save()
        {
            RepositoryManager.GetInstance().MusicRepository.SaveMedia();
        }

        protected override void GoToPreviousBook()
        {
            MainVM.PreviousBook(_currentMusicAlbum);
        }

        protected override void GoToNextBook()
        {
            MainVM.NextBook(_currentMusicAlbum);
        }
    }
}
