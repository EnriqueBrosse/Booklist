using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Booklist.Model;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;

namespace Booklist.Repository
{
       public class Repository { }

    public class BaseMediaRepository<T> : Repository where T : BaseMedia 
    {
        public BaseMediaRepository()
        {
        }

        public List<BaseMedia>ConvertToBase(List<T> derivedList)
        {
            List < BaseMedia > returnValue = new List<BaseMedia>();
            returnValue.AddRange(derivedList);
            return returnValue;
        }

        public override string ToString()
        {
            return "BaseMedia";
        }

        protected List<T> _baseMedia;
        protected string _filePath = "Booklist.Resources.BaseMedia.json";
        public List<T> GetMedia() 
        {
            if (_baseMedia != null)
            {
                return _baseMedia;
            }
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(_filePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    _baseMedia = JsonConvert.DeserializeObject<List<T>>(json);
                }
            }
            return _baseMedia;
        }
        public List<T> GetMedia(string owned, string legends)
        {
            List<T> legendsBooks = GetMediaByLegends(GetMedia(), legends);
            List<T> returnResult = GetMediaByOwned(legendsBooks, owned);
            return returnResult;
        }

        private List<T> GetMediaByOwned(List<T> books, string owned)
        {
            if (owned.Equals("All"))
            {
                return books;
            }

            List<T> returnValue = new List<T>();
            bool isOwned = owned.Equals("True");
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Owned == isOwned)
                {
                    returnValue.Add(books[i]);
                }
            }
            return returnValue;
        }

        private List<T> GetMediaByLegends(List<T> books, string legends)
        {
            if (legends.Equals("All"))
            {
                return books;
            }
            List<T> returnValue = new List<T>();
            bool isLegends = legends.Equals("True");
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Legends == isLegends)
                {
                    returnValue.Add(books[i]);
                }
            }
            return returnValue;
        }

        protected List<string> _eras;
        public virtual List<string> GetEras()
        {
            if (_eras != null)
            {
                return _eras;
            }
            GetMedia();
            _eras = new List<string>();
            for (int i = 0; i < _baseMedia.Count; i++)
            {
                bool hasFound = false;
                for (int j = 0; j < _eras.Count; j++)
                {
                    if (_baseMedia[i].Era.Equals(_eras[j]))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    _eras.Add(_baseMedia[i].Era);
                }
            }
            return _eras;
        }
        public List<T> GetMediaFromEra(string era, string owned, string legends)
        {
            if (era.Equals("All"))
            {
                return GetMedia(owned, legends);
            }
            List<T> returnValue = new List<T>();
            List<T> bookList = GetMedia(owned, legends);
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Era.Equals(era))
                {
                    returnValue.Add(bookList[i]);
                }
            }
            return returnValue;
        }

        public virtual List<string> GetSeriesFromEra(string era, string owned, string legends)
        {
            List<string> series = new List<string>();
            series.Add("All");
            return series;
        }
        public virtual List<T> GetMediaFromSeries(string series, string era, string owned, string legends)
        {
            return GetMedia(owned, legends);
        }

        public virtual void SaveMedia(T originalMedia, T mediaToBeReplaced)
        {
            GetMedia();
            for (int i = 0; i < _baseMedia.Count; i++)
            {
                if (_baseMedia[i] == originalMedia)
                {
                    _baseMedia[i] = mediaToBeReplaced;
                    break;
                }
            }
            SaveMedia();
        }

        public virtual void SaveMedia(T mediaToAdd)
        {
            _baseMedia.Add(mediaToAdd);
            SaveMedia();
        }

        protected string _outputFile = @"..\..\Resources\BaseMedia.json";
        public virtual void SaveMedia()
        {
            using (StreamWriter file = File.CreateText(_outputFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _baseMedia);
            }
        }

        public void AddMedia(T media)
        {
            // searching for duplicates
            List<T> eraMedia = GetMediaFromEra(media.Era, "All", "All");
            for (int i = 0; i < eraMedia.Count; i++)
            {
                if (media.Name.Equals(eraMedia[i].Name))
                {
                    if (media.ReleaseYear == eraMedia[i].ReleaseYear && media.Time == eraMedia[i].Time && eraMedia[i].ImageURL.Equals(media.ImageURL))
                    {
                        return;
                    }
                }
            }
            _baseMedia.Add(media);
            SaveMedia();
        }

    }
}
