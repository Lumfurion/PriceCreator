using System;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{
    [XmlRoot(ElementName = "yml_catalog")]
    public class Yml_catalog
    {   /// <summary>
        ///  Содержит описание магазина и его товарных предложений.
        /// </summary>
        [XmlElement(ElementName = "shop")]
        public Shop Shop { get; set; }
        /// <summary>
        ///  Атрибут date указывает дату и время генерации или изменения XML (YML). 
        ///  Дата должна иметь формат YYYY-MM-DD hh:mm.
        /// </summary>
        [XmlAttribute(AttributeName = "date")]
        public string Date { get; set; }
        public Yml_catalog() { }

        public Yml_catalog(string name, string company, string url)
        {
            Shop = new Shop(name,company,url);
            Date = DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");
        }

    }
}
