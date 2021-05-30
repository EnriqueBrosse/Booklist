using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace Booklist.Model
{
    public class Book
    {
        [JsonProperty(PropertyName = "releaseYear")]
        public int ReleaseYear { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "imageURL")]
        public string ImageURL { get; set; }
        [JsonProperty(PropertyName = "era")]
        public string Era { get; set; }
        [JsonProperty(PropertyName = "series")]
        public string Series { get; set; }
        [JsonProperty(PropertyName = "links")]
        public string[] Links { get; set; }
        [JsonProperty(PropertyName = "Owned")]
        public bool Owned { get; set; }
        [JsonProperty(PropertyName = "legends")]
        public bool Legends { get; set; }
        [JsonProperty(PropertyName ="writer")]
        public string Writer { get; set; }

        //[JsonProperty(PropertyName = "releaseMonth")]
        //public int ReleaseMonth { get; set; }
        //[JsonProperty(PropertyName = "releaseYear")]
        //public int ReleaseYear { get; set; }
        //[JsonProperty(PropertyName = "name")]
        //public string Name { get; set; }
        //[JsonProperty(PropertyName = "imageURL")]
        //public string ImageURL { get; set; }
        //[JsonProperty(PropertyName = "era")]
        //public string Era { get; set; }
        //[JsonProperty(PropertyName = "series")]
        //public string Series { get; set; }
        //[JsonProperty(PropertyName = "amazonLink")]
        //public string AmazonLink { get; set; }
        //[JsonProperty(PropertyName = "bolLink")]
        //public string BolLink { get; set; }
        //[JsonProperty(PropertyName = "owned")]
        //public bool Owned { get; set; }
        //[JsonProperty(PropertyName = "legends")]
        //public bool Legends { get; set; }
        //[JsonProperty(PropertyName = "isBook")]
        //public bool IsBook { get; set; }
        //[JsonProperty(PropertyName = "Issues")]
        //public List<string> Issues { get; set; }
    }
}
