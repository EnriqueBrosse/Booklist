using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;
namespace Booklist.Model
{
    class WebsiteScrapper
    {
        public static WebsitePrice ScrapeWebsiteData(string website)
        {
            if (website.Contains("amazon"))
            {
                // amazon is sometimes a bitch and doesn't want to give data 
                Tuple<WebsitePrice, bool> temp = ScrapeAmazonData(website);
                while (!temp.Item2)
                {
                    temp = ScrapeAmazonData(website);
                }
                return temp.Item1;
            }
            else if (website.Contains("bol.com"))
            {
                return ScrapeBolData(website);
            }
            else if (website.Contains("standaardboekhandel.be"))
            {
                return ScrapeStandaardBoekHandelData(website);
            }
            return null;
        }
        private static WebsitePrice ScrapeBolData(string website)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(website);
            WebsitePrice returnvalue = new WebsitePrice();
            returnvalue.Link = website;
            returnvalue.BookPrices = new List<BookPrice>();
            var temp = doc.DocumentNode.SelectNodes("//span[@class='feature-option__value']");
            if (temp != null)
            {
                var options = temp.ToList();
                bool isEven = options.Count % 2 == 0;
                if (!isEven)
                {
                    bool wasLastValuePrice = true;
                    for (int i = 0; i < options.Count; i++)
                    {
                        string innertext = options[i].InnerText;
                        if (innertext.Contains("&euro; "))
                        {
                            wasLastValuePrice = true;
                        }
                        else
                        {
                            if (wasLastValuePrice)
                            {
                                BookPrice bookPrice = new BookPrice();
                                bookPrice.Bindwise = innertext;
                                returnvalue.BookPrices.Add(bookPrice);
                            }
                            else
                            {
                                BookPrice bookPrice = new BookPrice();
                                bookPrice.Bindwise = innertext;
                                returnvalue.BookPrices[returnvalue.BookPrices.Count-1] = bookPrice;

                            }
                            wasLastValuePrice = false;
                        }
                    }
                    string lastBindwise = "";
                    for (int i = 0; i < options.Count; i++)
                    {
                        //Console.WriteLine(options[i].InnerText);
                        string innertext = options[i].InnerText;
                        if (innertext.Contains("&euro; "))
                        {
                            string priceString = innertext.Replace("&euro; ", "");
                            double price = Convert.ToDouble(priceString);
                            for (int j = 0; j < returnvalue.BookPrices.Count; j++)
                            {
                                if (lastBindwise.Equals(returnvalue.BookPrices[j].Bindwise))
                                {
                                    returnvalue.BookPrices[j].Price = price;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            lastBindwise = innertext;
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < options.Count / 2; i++)
                    {
                        returnvalue.BookPrices.Add(new BookPrice());
                    }
                    for (int i = 0; i < options.Count; i++)
                    {
                        //Console.WriteLine(options[i].InnerText);
                        string innertext = options[i].InnerText;
                        if (innertext.Contains("&euro; "))
                        {
                            string priceString = innertext.Replace("&euro; ", "");
                            double price = Convert.ToDouble(priceString);
                            returnvalue.BookPrices[i / 2].Price = price;
                        }
                        else
                        {
                            returnvalue.BookPrices[i / 2].Bindwise = innertext;
                        }
                    }
                }
            }
            var temp2 = doc.DocumentNode.SelectNodes("//span[@class='promo-price']");
            if (temp2 != null)
            {
                string priceString = temp2.ToList()[0].InnerText.Replace("\n", "");
                priceString = priceString.Replace("  ", ",");
                priceString = priceString.Replace("-", "");
                double price = Convert.ToDouble(priceString);
                returnvalue.PromoPrice = price;
            }
            return returnvalue;
        }

        private static WebsitePrice ScrapeStandaardBoekHandelData(string website)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(website);
            WebsitePrice returnvalue = new WebsitePrice();
            returnvalue.Link = website;
            returnvalue.BookPrices = new List<BookPrice>();

            // getting the possible prices from the bar 
            var temp = doc.DocumentNode.SelectNodes("//div[@class='o-flex c-product-detail-colours']");
            if (temp != null)
            {
                List<HtmlNode> list = temp.ToList();
                if (list.Count != 0)
                {
                    string text = list[0].InnerText;
                    text = text.Replace(" ", String.Empty);
                    text = text.Replace("\r\n", " ");
                    string[] words = text.Split(' ');
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i].Contains("-"))
                        {
                            string[] bindingAndCost = words[i].Split('-');
                            BookPrice bookPrice = new BookPrice();
                            bookPrice.Bindwise = bindingAndCost[0];
                            bindingAndCost[1] = bindingAndCost[1].Replace("€", "");
                            double price = Convert.ToDouble(bindingAndCost[1]);
                            bookPrice.Price = price;
                            returnvalue.BookPrices.Add(bookPrice);
                        }
                    }
                }
            }

            // getting the promo price from the website 
            var temp2 = doc.DocumentNode.SelectNodes("//span[@class='c-product__detail__price--largest']");
            if (temp2 != null)
            {
                List<HtmlNode> list = temp2.ToList();
                string innerText = list[0].InnerText;
                innerText = innerText.Replace("€", "");
                returnvalue.PromoPrice = Convert.ToDouble(innerText);
            }

            return returnvalue;
        }

        private static Tuple<WebsitePrice,bool> ScrapeAmazonData(string website)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(website);
            WebsitePrice returnvalue = new WebsitePrice();
            returnvalue.Link = website;
            returnvalue.BookPrices = new List<BookPrice>();
            var temp = doc.DocumentNode.SelectNodes("//span[@class='a-size-medium a-color-price offer-price a-text-normal']");
            bool didSomething = false;
            if (temp != null)
            {
                List<HtmlNode> list = temp.ToList();
                string innertext = list[0].InnerText;
                // replace the euro sign if it's there for the nl website 
                innertext=innertext.Replace("EUR", "");
                innertext = innertext.Replace("€", "");
                innertext = innertext.Replace("\n", "");
                innertext = innertext.Trim();
                returnvalue.PromoPrice = Convert.ToDouble(innertext);
                didSomething = true;
            }

            temp = doc.DocumentNode.SelectNodes("//a[@class='a-button-text']");
            if (temp != null)
            {
                didSomething = true;
                List<HtmlNode> list = temp.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    string bookPriceString = list[i].InnerText;
                    bookPriceString = bookPriceString.Replace("\n", "");
                    string[] strings = bookPriceString.Split(' ');
                    bool foundPrice = false;
                    for (int j = 0; j < strings.Length; j++)
                    {
                        if (strings[j].Contains("€ "))
                        {
                            foundPrice = true;
                        }
                    }
                    if (!foundPrice)
                    {
                        continue;
                    }

                    BookPrice bookPrice = new BookPrice();
                    bookPrice.Bindwise = strings[0];
                    for (int j = 1; j < strings.Count(); j++)
                    {
                        if (strings[j].Contains("€ "))
                        {
                            strings[j] = strings[j].Replace("€ ", "");
                            bookPrice.Price = Convert.ToDouble(strings[j]);
                            break;
                        }
                    }
                    returnvalue.BookPrices.Add(bookPrice);
                }
            }
            return new Tuple<WebsitePrice, bool>(returnvalue, didSomething);
        }
    }
}
