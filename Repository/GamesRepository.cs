using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Booklist.Model;

namespace Booklist.Repository
{
    public class GamesRepository : BaseMediaRepository<Game>
    {
        public GamesRepository() : base()
        {
            _filePath = "Booklist.Resources.Games.json";
            _outputFile = @"..\..\Resources\Games.json";
        }
        public override string ToString()
        {
            return "Games";
        }

        public override List<string> GetSeriesFromEra(string era, string owned, string legends)
        {
            List<Game> booksFromEra = GetMediaFromEra(era, owned, legends);
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

        public override List<Game> GetMediaFromSeries(string series, string era, string owned, string legends)
        {
            List<Game> bookList;
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
            List<Game> returnValue = new List<Game>();

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
