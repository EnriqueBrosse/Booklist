using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Booklist.Model;

namespace Booklist.Repository
{
    public class ComicRepository : BaseMediaRepository<Comic>
    {
        public ComicRepository() : base()
        {
            _filePath = "Booklist.Resources.Comics.json";
            _outputFile = @"..\..\Resources\Comics.json";
        }

        public override string ToString()
        {
            return "Comics";
        }

        public override List<string> GetSeriesFromEra(string era, string owned, string legends)
        {
            List<Comic> booksFromEra = GetMediaFromEra(era, owned, legends);
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

        public override List<Comic> GetMediaFromSeries(string series, string era, string owned, string legends)
        {
            if (era.Equals("All"))
            {
                return GetMedia(owned, legends);
            }

            List<Comic> bookList = GetMediaFromEra(era, owned, legends);
            if (series.Equals("All"))
            {
                return bookList;
            }
            List<Comic> returnValue = new List<Comic>();

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
