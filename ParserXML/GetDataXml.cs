using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using ParserXML.Parserse;

namespace ParserXML
{
    
    public class Param
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Param(string name, string text)
        {
            Name = name; 
            Text = text;
        }
    }

    class Offer
    {
        public string Url { get; set; }
        public decimal Price { get; set; }
        public string CurrencyId { get; set; }
        public int CategoryId { get; set; }
        public List<string> Picture { get; set; } = new List<string>();
        public string Name { get; set; }
        public string Vendor { get; set; }
        public List<string> Description { get; set; } = new List<string>();
        public List<Param> Param { get;  set; } = new List<Param>();
        public int Stock_quantity { get; set; }
        public bool Available { get; set; }
        public int Id { get; set; }
        public Offer(string url, decimal price, string currencyId, int categoryId,string name, string vendor, int stock_quantity, bool available, int id)
        {
            Url = url; 
            Price = price; 
            CurrencyId = currencyId; 
            CategoryId = categoryId;
            Name = name; 
            Vendor = vendor; 
            Stock_quantity = stock_quantity; 
            Available = available; 
            Id = id;
        }
        
    }

    public class Currency
    {
        public string Id { get; set; }
        public int Rate { get; set; }
        public Currency(string id, int rate)
        {
            Id = id;
            Rate = rate;
        }
    }
    class Seller
    {
      
        public string Name { get; set; }
        public string Company { get; set; }
        public string Url { get; set; }
        public string Date { get; set; }
        public List<string> Сategories { get; set; } = new List<string>();
        public List<Currency> Currencies { get; set; } = new List<Currency>();
        public List<Offer> Offers { get; set; } = new List<Offer>();

        public Seller(string path)
        {

            initialization(path);
        }

        private void initialization(string path)
        {
            Yml_catalog catalog;
            var serializer = new XmlSerializer(typeof(Yml_catalog));
            using (var stream = File.OpenRead(@path)) catalog = (Yml_catalog)serializer.Deserialize(stream);
            var offers = catalog.Shop.Offers.Offer;
            var shop = catalog.Shop;
            Name = shop.Name;
            Company = shop.Company;
            Url = shop.Url;
            Date = catalog.Date;

            foreach (var sh in shop.Categories.Category) Сategories.Add(sh.Text);


            int count = shop.Currencies.Currency.Count;
            for (var i = 0; i < count; i++)
                Currencies.Add(new Currency(shop.Currencies.Currency[i].Id, shop.Currencies.Currency[i].Rate));



            foreach (var offer in offers)
                Offers.Add(new Offer(offer.Url, offer.Price, offer.CurrencyId, offer.CategoryId, offer.Name, offer.Vendor, offer.Stock_quantity, offer.Available, offer.Id));



            for (var i = 0; i < Offers.Count; i++)
                for (var t = 0; t < 6; t++)
                    Offers[i].Param.Add(new Param(offers[i].Param[t].Name, offers[i].Param[t].Text));

            for (var i = 0; i < Offers.Count; i++)
            {
                int c = offers[i].Picture.Count;
                for (var t = 0; t < c; t++)
                    Offers[i].Picture.Add(offers[i].Picture[t]);
            }


            var pattern = @"<p>(.+)</p>";
            for (var i = 0; i < Offers.Count; i++)
            {
                foreach (Match match in Regex.Matches(offers[i].Description, pattern))
                    Offers[i].Description.Add(match.Groups[1].Value);
            }
        }



    }

    static class  GetDataXml
    {
        static Seller sel { get; set; }
        public static Seller GetXlmData(string path)
        {
            sel = new Seller(path);
            return sel;
        }
        
    }
      


    
}
