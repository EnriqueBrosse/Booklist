using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using Newtonsoft.Json;

namespace Booklist.Model
{
    public class BaseMedia
    {
        public BaseMedia()
        {
            ReleaseYear = 2021;
            Name = "";
            ImageURL = "";
            Era = "";
            Links = new string[0];
            Owned = false;
            Legends = false;
        }
        [JsonProperty(PropertyName = "releaseYear")]
        public int ReleaseYear { get; set; }
        [JsonProperty(PropertyName = "Time")]
        public int Time { get; set; }
        [JsonProperty(PropertyName = "Owned")]
        public bool Owned { get; set; }
        [JsonProperty(PropertyName = "legends")]
        public bool Legends { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "imageURL")]
        public string ImageURL { get; set; }
        [JsonProperty(PropertyName = "era")]
        public string Era { get; set; }
        [JsonProperty(PropertyName = "links")]
        public string[] Links { get; set; }
        private BitmapImage _image;
        [JsonIgnore]
        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; }
        }
    }
}
