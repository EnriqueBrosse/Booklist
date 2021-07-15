using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booklist.ViewModel.EditMediaViewModels
{
    using ViewModel.DetailViewModels;
    using Model;
    public class EditBookVM : EditMediaVM
    {
        private Book _copiedBook;
        public BookDetailVM DetailViewModel{get;set;}
        public Book CopiedBook
        {
            get { return _copiedBook; }
            set { _copiedBook = value; RaisePropertyChanged("CopiedMusicAlbum"); }
        }
        protected override void Save()
        {
            DetailViewModel.Save(CopiedBook);
            RaisePropertyChanged("CopiedMusicAlbum");
        }
    }
}
