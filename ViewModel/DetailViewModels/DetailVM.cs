using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Booklist.ViewModel
{
    public class DetailVM : ViewModelBase
    {
        public MainViewModel MainVM { get; set; }
        public string LinkToAdd { get; set; } = "";
        public string CurrentLink { get; set; }

        #region Commands 
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
        protected virtual void AddLink()
        {
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
        protected virtual void RemoveLink()
        {
        }
        private RelayCommand _editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(OpenScreen);
                }
                return _editCommand;
            }
        }
        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(Save);
                }
                return _saveCommand;
            }
        }
        protected virtual void OpenScreen()
        {
        }
        protected virtual void Save()
        {
        }

        private RelayCommand _previousBookCommand;
        public RelayCommand PreviousBookCommand
        {
            get 
            {
                if (_previousBookCommand == null)
                {
                    _previousBookCommand = new RelayCommand(GoToPreviousBook);
                }
                return _previousBookCommand; 
            }
        }

        protected virtual void GoToPreviousBook()
        {
        }

        private RelayCommand _nextBookCommand;
        public RelayCommand NextBookCommand
        {
            get
            {
                if (_nextBookCommand == null)
                {
                    _nextBookCommand = new RelayCommand(GoToNextBook);
                }
                return _nextBookCommand;
            }
        }

        protected virtual void GoToNextBook()
        {
        }
        #endregion Commands 

        protected bool IsValidLink(string url)
        {
            url = url.ToLower();
            return url.StartsWith("http://") || url.StartsWith("https://");
        }
    }
}
