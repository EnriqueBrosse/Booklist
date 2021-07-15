using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Booklist.Model;

namespace Booklist.Repository
{
    public class MusicRepository : BaseMediaRepository<MusicAlbum>
    {
        public MusicRepository()
        {
            _filePath = "Booklist.Resources.MusicAlbums.json";
            _outputFile = @"..\..\Resources\MusicAlbums.json";
        }

        public override string ToString()
        {
            return "MusicAlbums";
        }
    }
}
