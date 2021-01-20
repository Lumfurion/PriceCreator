using PriceCreator.ManagerXml.DeserializeObjects;
using PriceCreator.Models;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml
{

    static  class  GetDataXml
    {
       static SellerModel Seller { get; set; }
       public static void Run(string path, SellerModel seller)
       {
            
            Seller = seller;
            Сlean();
            Yml_catalog catalog;
            var serializer = new XmlSerializer(typeof(Yml_catalog));
            using (var stream = File.OpenRead(@path))
            {
                catalog = (Yml_catalog)serializer.Deserialize(stream);
            }
            var shop = catalog.Shop;
            Seller.Name = shop.Name;
            Seller.Company = shop.Company;
            Seller.Url = shop.Url;
            Seller.Date = catalog.Date;
            var offers = shop.Offers.Offer;
            //Категории
            foreach (var sh in shop.Categories.Category)
            {
                Seller.Сategories.Add(new CategoryModel(sh.Id, sh.Text));
            }
            //Список валют и коэффициенты.
            int count = shop.Currencies.Currency.Count;
            for (var i = 0; i < count; i++)
            {
                Seller.Currencies.Add(new CurrencyModel(shop.Currencies.Currency[i].Id, shop.Currencies.Currency[i].Rate));
            }

            foreach (var offer in offers)
            {
                Seller.Offers.Add(new OfferModel(offer.Url, offer.Price, offer.CurrencyId, offer.CategoryId, offer.Name, offer.Vendor, offer.Stock_quantity, offer.Available, offer.Id));
            }

            //Характеристики
            var Offers = Seller.Offers;//Смотрим сколько товаров.
            for (var i = 0; i < Offers.Count; i++)//Количество товаров.
            {
                int c = offers[i].Param.Count();//Количество параметров в товара.
                for (var t = 0; t < c; t++)
                {
                    Seller.Offers[i].Param.Add(new ParamModel(offers[i].Param[t].Name, offers[i].Param[t].Text));
                }
            }
            //Картинки
            for (var i = 0; i < Offers.Count; i++)
            {
                int c = offers[i].Picture.Count;
                for (var t = 0; t < c; t++)
                {
                    Offers[i].Picture.Add(offers[i].Picture[t]);
                }
            }
            //Описанные
            var pattern = @"<p>(.+)</p>";
            for (var i = 0; i < Offers.Count; i++)
            {
                foreach (Match match in Regex.Matches(offers[i].Description, pattern))
                {
                    Offers[i].Descriptions.Add(new DescriptionModel(match.Groups[1].Value));
                }
            }
        }

        public static void Test(string path, SellerModel seller)
        {

            Seller = seller;
            Сlean();
            Yml_catalog catalog;
            var serializer = new XmlSerializer(typeof(Yml_catalog));
            using (var stream = File.OpenRead(@path))
            {
                catalog = (Yml_catalog)serializer.Deserialize(stream);
            }
            var shop = catalog.Shop;
            Seller.Name = shop.Name;
            Seller.Company = shop.Company;
            Seller.Url = shop.Url;
            Seller.Date = catalog.Date;
            var offers = shop.Offers.Offer;
            //Категории
            foreach (var sh in shop.Categories.Category)
            {
                Seller.Сategories.Add(new CategoryModel(sh.Id, sh.Text));
            }
            //Список валют и коэффициенты.
            int count = shop.Currencies.Currency.Count;
            for (var i = 0; i < count; i++)
            {
                Seller.Currencies.Add(new CurrencyModel(shop.Currencies.Currency[i].Id, shop.Currencies.Currency[i].Rate));
            }

            foreach (var offer in offers)
            {
                Seller.Offers.Add(new OfferModel(offer.Url, offer.Price, offer.CurrencyId, offer.CategoryId, offer.Name, offer.Vendor, offer.Stock_quantity, offer.Available, offer.Id));
                int IdOffers = offers.IndexOf(offer);
               
                foreach (var param in offer.Param)//Характеристики
                {
                   Seller.Offers[IdOffers].Param.Add(new ParamModel(param.Name,param.Text));
                }

                foreach (var image in offer.Picture)//Картинки
                {
                    Seller.Offers[IdOffers].Picture.Add(image);
                }

                string patr = @"<p>(.+)</p>";
                foreach (Match match in Regex.Matches(offers[IdOffers].Description, patr))//Описанные
                {
                   Seller.Offers[IdOffers].Descriptions.Add(new DescriptionModel(match.Groups[1].Value));
                }

            }

        }

        static void Сlean()
        {
            Seller.Name = " ";
            Seller.Company = " ";
            Seller.Url = " ";
            Seller.Date = " ";
            Seller.Сategories.Clear();
            Seller.Currencies.Clear();
            Seller.Offers.Clear();
        }

    }

    

}
