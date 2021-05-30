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
    class BookReposoitory
    {
        public static List<Book> _bookList;
        public static List<Book> GetBooks()
        { 
            if(_bookList != null)
            {
                return _bookList; 
            }
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string filePath = "Booklist.Resources.Books.json";
            using (Stream stream = assembly.GetManifestResourceStream(filePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    _bookList = JsonConvert.DeserializeObject<List<Book>>(json);
                }
            }

            //List<Books2> books2 = new List<Books2>();
            //for (int i = 0; i < _bookList.Count; i++)
            //{
            //    books2.Add(Books2.Convert(_bookList[i]));
            //}

            ////File.WriteAllText(@"..\..\Resources\Books2.json", JsonConvert.SerializeObject(books2));

            //// serialize JSON directly to a file
            //using (StreamWriter file = File.CreateText(@"..\..\Resources\Books2.json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(file, books2);
            //}

            return _bookList;

        }
        public static List<Book> GetBooks(string owned, string legends)
        {
            List<Book> legendsBooks = GetBooksByLegends(GetBooks(),legends);
            List<Book> returnResult = GetBooksByOwned(legendsBooks, owned);
            return returnResult;
        }

        private static List<Book> GetBooksByOwned(List<Book> books, string owned)
        {
            if (owned.Equals("All"))
            {
                return books;
            }

            List<Book> booklist = GetBooks();
            List<Book> returnValue = new List<Book>();
            bool isOwned = owned.Equals("True");
            for (int i = 0; i < booklist.Count; i++)
            {
                if (booklist[i].Owned == isOwned)
                {
                    returnValue.Add(booklist[i]);
                }
            }
            return returnValue;
        }

        private static List<Book> GetBooksByLegends(List<Book> books, string legends)
        {
            if (legends.Equals("All"))
            {
                return books;
            }
            List<Book> booklist = GetBooks();
            List<Book> returnValue = new List<Book>();
            bool isLegends = legends.Equals("True");
            for (int i = 0; i < booklist.Count; i++)
            {
                if (booklist[i].Legends == isLegends)
                {
                    returnValue.Add(booklist[i]);
                }
            }
            return returnValue;
        }

        private static List<string> _eras;
        public static List<string> GetEras()
        { 
            if(_eras != null)
            {
                return _eras;
            }
            GetBooks();
            _eras = new List<string>();
            for (int i = 0; i < _bookList.Count; i++)
            {
                bool hasFound = false;
                for (int j = 0; j < _eras.Count; j++)
                {
                    if (_bookList[i].Era.Equals(_eras[j]))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    _eras.Add(_bookList[i].Era);
                }
            }
            return _eras;
        }
        public static List<Book> GetBooksFromEra(string era, string owned, string legends)
        {
            if (era.Equals("All"))
            {
                return GetBooks(owned, legends);
            }
            List<Book> returnValue = new List<Book>();
            List<Book> bookList = GetBooks(owned, legends);
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Era.Equals(era))
                {
                    returnValue.Add(bookList[i]);
                }
            }
            return returnValue;
        }
        public static List<string> GetSeriesFromEra(string era, string owned, string legends)
        {
            List<Book> booksFromEra = GetBooksFromEra(era, owned,legends);
            List<string> series = new List<string>();
            for (int i = 0; i < booksFromEra.Count; i++)
            {
                bool hasFound = false;
                for (int j = 0; j < series.Count; j++)
                {
                    if (booksFromEra[i].Series.Equals(series[j]))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    series.Add(booksFromEra[i].Series);
                }
            }
            // finding the ones that don't have a series and replacing it 

            for (int i = 0; i < series.Count; i++)
            {
                if (series[i].Equals(""))
                {
                    series[i] = "None";
                    break;
                }
            }
            series.Add("All");
            return series;
        }
        public static List<Book> GetBooksFromSeries(string series, string era, string owned, string legends)
        {

            if (era.Equals("All"))
            {
                return GetBooks(owned,legends);
            }

            List<Book> bookList = GetBooksFromEra(era,owned, legends);
            if (series.Equals("All"))
            {
                return bookList;
            }
            List<Book> returnValue = new List<Book>();

            if (series.Equals("None"))
            {
                series = "";
            }

            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].Series.Equals(series))
                {
                    returnValue.Add(bookList[i]);
                }
            }
            return returnValue;
        }

        public static void SaveBooks(Book originalBook, Book bookToBeReplaced)
        {
            GetBooks();
            for (int i = 0; i < _bookList.Count; i++)
            {
                if (_bookList[i] == originalBook)
                {
                    _bookList[i] = bookToBeReplaced;

                    break;
                }
            }
            SaveBooks();
        }

        public static void SaveBooks(Book bookToAdd)
        {
            _bookList.Add(bookToAdd);
            SaveBooks(); 
        }

        public static void SaveBooks()
        {
            GetBooks();
            using (StreamWriter file = File.CreateText(@"..\..\Resources\Test.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _bookList);
            }
        }

        public static void AddBook(Book book)
        {
            // searching for duplicates
            GetBooks();
            List<Book>eraBooks = GetBooksFromEra(book.Era, "All", "All");
            for (int i = 0; i < eraBooks.Count; i++)
            {
                if (book.Name.Equals(eraBooks[i].Name))
                {
                    return;
                }
            }

            _bookList.Add(book);
            SaveBooks();
        }

    }
}
