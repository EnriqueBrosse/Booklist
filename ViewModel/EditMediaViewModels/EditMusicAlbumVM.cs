using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booklist.ViewModel.EditMediaViewModels
{
    using ViewModel.DetailViewModels;
    using Model;
    class EditMusicAlbumVM : EditMediaVM
    {
        private MusicAlbum _copiedMusicAlbum;
        public MusicAlbum CopiedMusicAlbum
        {
            get { return _copiedMusicAlbum; }
            set { _copiedMusicAlbum = value; RaisePropertyChanged("CopiedMusicAlbum"); }
        }
        public MusicAlbumDetailVM DetailViewModel { get; set; }
        protected override void Save()
        {
            DetailViewModel.Save(CopiedMusicAlbum);
            RaisePropertyChanged("CopiedMusicAlbum");
        }
    }
}
