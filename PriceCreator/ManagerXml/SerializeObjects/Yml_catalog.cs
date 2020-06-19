using System;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{
    [XmlRoot(ElementName = "yml_catalog")]
    public class Yml_catalog
    {
        [XmlElement(ElementName = "shop")]
        public Shop Shop { get; set; }

        [XmlAttribute(AttributeName = "date")]
        public string Date { get; set; }
        public Yml_catalog() { }

        public Yml_catalog(string name, string company, string url)
        {
            Shop = new Shop(name,company,url);
            Date = DateTime.Now.ToString();
        }

    }
}
