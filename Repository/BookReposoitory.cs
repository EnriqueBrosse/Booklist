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
    public class BookReposoitory : BaseMediaRepository<Book>
    {
        public BookReposoitory() : base()
        {
            _filePath = "Booklist.Resources.Books.json";
            _outputFile = @"..\..\Resources\Books.json";
        }

        public override string ToString()
        {
            return "Books";
        }

        public override List<string> GetSeriesFromEra(string era, string owned, string legends)
        {
            List<Book> booksFromEra = GetMediaFromEra(era, owned,legends);
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

        public override List<Book> GetMediaFromSeries(string series, string era, string owned, string legends)
        {
            List<Book> bookList;
            if (era.Equals("All"))
            {
                bookList = GetMedia(owned, legends);
            }
            else
            {
                bookList = GetMediaFromEra(era, owned, legends);
            }
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
    }
}
