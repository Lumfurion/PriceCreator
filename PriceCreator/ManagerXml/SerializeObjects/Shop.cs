using System;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{

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
        public Shop(){}
        public Shop(string name, string company, string url)
        {
            Name = name;
            Company = company;
            Url = url;
            Currencies = new Currencies();
            Categories = new Categories();
            Offers = new Offers(); 
        }

         
    }
}
