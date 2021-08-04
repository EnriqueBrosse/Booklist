using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booklist.Model;

namespace Booklist.Repository
{
    public class FilmMediaRepository : BaseMediaRepository<FilmMedia>
    {
        public FilmMediaRepository() : base()
        {
            _filePath = "Booklist.Resources.FilmMedia.json";
            _outputFile = @"..\..\Resources\FilmMedia.json";
        }
        public override string ToString()
        {
            return "FilmMedia";
        }

    }
}
