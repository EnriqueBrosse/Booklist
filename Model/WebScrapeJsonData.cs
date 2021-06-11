using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
namespace Booklist.Model
{
    class WebScrapeJsonData
    {
        [JsonProperty(PropertyName = "bookName")]
        public string BookName { get; set; }
        [JsonProperty(PropertyName = "websitePrices")]
        public List<WebsitePrice> WebsitePrices { get; set; }
    }

    class WebsitePrice
    {
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }
        [JsonProperty(PropertyName = "promoPrice")]
        public double PromoPrice { get; set; }
        [JsonProperty(PropertyName = "bookPrices")]
        public List<BookPrice> BookPrices {get;set;}
    }
    class BookPrice
    {
        [JsonProperty(PropertyName = "bindwise")]
        public string Bindwise { get; set; }
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
    }

}
