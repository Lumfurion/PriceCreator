using PriceCreator.Models;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using PriceCreator.ManagerXml.SerializeObjects;
using System.Xml;

namespace PriceCreator.ManagerXml.Serialization
{
    static class  Generate
    {
       
        public static  void DataTransfer(SellerModel seller, string path)
        {        
                Yml_catalog catalog = new Yml_catalog(seller.Name, seller.Company, seller.Url);
   
                //Категория
                var categories = seller.Сategories;
                foreach (var c in categories)
                {
                  catalog.Shop.Categories.Add(c.Id, c.Name);
                }
                //Валюта
                var currencies = seller.Currencies.ToList();
              
                foreach(var cur in currencies)
                {
                  catalog.Shop.Currencies.Add(cur.Id, cur.Rate);
                }

                //Товар
                var Offers = seller.Offers;
                foreach (var o in Offers)
                {
                
                  catalog.Shop.Offers.AddInfo(o.Url, o.Price, o.CurrencyId, o.CategoryId, o.Name, o.Vendor, o.Stock_quantity, o.Available, o.Id);

                }

                //Параметер товара
                for (var i = 0; i < Offers.Count; i++)
                {
                    var count= Offers[i].Param.Count();
                    for (var t = 0; t < count; t++)
                    {
                         catalog.Shop.Offers.Offer[i].Param.Add(new Param(Offers[i].Param[t].Name, Offers[i].Param[t].Text));
                    }
                }


                //Картинки
                for (var i = 0; i < Offers.Count; i++)
                {
                    int count = Offers[i].Picture.Count;
                    for (var t = 0; t < count; t++)
                    {
                      catalog.Shop.Offers.Offer[i].Picture.Add(Offers[i].Picture[t]);
                    }
                }

                //Описаные товара
                var dec = string.Empty;
                for (var i = 0; i < Offers.Count; i++)
                {
                   
                    int count = Offers[i].Descriptions.Count();

                    for (var t = 0; t < count; t++)
                    {
                        dec+="<p>"+Offers[i].Descriptions[t].Text+"</p>\r\n";
                    }
                    XmlCDataSection xmlDescription = new XmlDocument().CreateCDataSection(dec);
                    catalog.Shop.Offers.Offer[i].Description = xmlDescription;
                    dec = string.Empty;
                }


                using (StreamWriter sw = new StreamWriter(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Yml_catalog));

                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    serializer.Serialize(sw, catalog, ns);
                }


                seller.Name = " ";
                seller.Company = " ";
                seller.Url = " ";
                seller.Date = " ";
                seller.Сategories.Clear();
                seller.Currencies.Clear();
                seller.Offers.Clear();

        }

    }
}
