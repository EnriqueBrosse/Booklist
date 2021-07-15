using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booklist.ViewModel.EditMediaViewModels
{
    using ViewModel.DetailViewModels;
    using Model;

    class EditComicVM : EditMediaVM
    {
        private Comic _copiedComic;
        public ComicDetailVM DetailViewModel { get; set; }
        public Comic CopiedComic
        {
            get { return _copiedComic; }
            set { _copiedComic = value; RaisePropertyChanged("CopiedComic"); }
        }
        protected override void Save()
        {
            DetailViewModel.Save(CopiedComic);
            RaisePropertyChanged("CopiedComic");
        }
    }
}
