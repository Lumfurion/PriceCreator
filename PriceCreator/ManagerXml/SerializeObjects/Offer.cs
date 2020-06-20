using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{
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
        public List<string> Picture { get; set; } = new List<string>();
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "vendor")]
        public string Vendor { get; set; }
        private string _description;
        [XmlElement(ElementName = "description")]
        public XmlCDataSection Description 
        {
            get => new XmlDocument().CreateCDataSection(_description);
            set => _description = value.Value;
        }
        [XmlElement(ElementName = "param")]
        public List<Param> Param { get; set; } = new List<Param>();
        [XmlElement(ElementName = "stock_quantity")]
        public int Stock_quantity { get; set; }
        [XmlAttribute(AttributeName = "available")]
        public bool Available { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        public Offer() { }
        public Offer(string url, decimal price, string currencyId, int categoryId, string name, string vendor, int stock_quantity, bool available, int id)
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
}
