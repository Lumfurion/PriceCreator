using System.Xml.Serialization;
using System.Collections.Generic;
using System;

namespace ParserXML.Parserse
{
    [XmlRoot(ElementName = "currency")]
    public class Currency
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "rate")]
        public int Rate { get; set; }
    }

    [XmlRoot(ElementName = "currencies")]
    public class Currencies
    {
        [XmlElement(ElementName = "currency")]
        public List<Currency> Currency { get; set; }
    }

    [XmlRoot(ElementName = "category")]
    public class Category
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "categories")]
    public class Categories
    {
        [XmlElement(ElementName = "category")]
        public List<Category> Category { get; set; }
    }

    [XmlRoot(ElementName = "param")]
    public class Param
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlText]
        public string Text { get; set; }
    }



    [XmlRoot(ElementName = "offer")]
    public class Offer
    {
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        [XmlElement(ElementName = "price")]
        public decimal Price { get; set; }
        [XmlElement(ElementName = "currencyId")]
        public string CurrencyId { get; set; }
        [XmlElement(ElementName = "categoryId")]
        public int CategoryId { get; set; }
        [XmlElement(ElementName = "picture")]
        public List<string> Picture { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "vendor")]
        public string Vendor { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "param")]
        public List<Param> Param { get; set; }
        [XmlElement(ElementName = "stock_quantity")]
        public int Stock_quantity { get; set; }
        [XmlAttribute(AttributeName = "available")]
        public bool Available { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }
    }

    [XmlRoot(ElementName = "offers")]
    public class Offers
    {
        [XmlElement(ElementName = "offer")]
        public List<Offer> Offer { get; set; }

        public static explicit operator Offers(List<Offer> v)
        {
            throw new NotImplementedException();
        }
    }

    [XmlRoot(ElementName = "shop")]
    public class Shop
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "company")]
        public string Company { get; set; }
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        [XmlElement(ElementName = "currencies")]
        public Currencies Currencies { get; set; }

        [XmlElement(ElementName = "categories")]
        public Categories Categories { get; set; }
        [XmlElement(ElementName = "offers")]
        public Offers Offers { get; set; }
    }

    [XmlRoot(ElementName = "yml_catalog")]
    public class Yml_catalog
    {
        [XmlElement(ElementName = "shop")]
        public Shop Shop { get; set; }

        [XmlAttribute(AttributeName = "date")]
        public string Date { get; set; }
    }

}