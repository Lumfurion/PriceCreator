using System;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{

    [XmlRoot(ElementName = "shop")]
    public class Shop
    {   /// <summary>
        /// Короткое название магазина. 
        /// </summary>
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        /// <summary>
        /// Полное наименование компании, владеющей магазином. 
        /// </summary>
        [XmlElement(ElementName = "company")]
        public string Company { get; set; }
        /// <summary>
        /// URL главной страницы существующего магазина.
        /// </summary>
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        /// <summary>
        /// Список курсов валют магазина. 
        /// </summary>
        [XmlElement(ElementName = "currencies")]
        public Currencies Currencies { get; set; }
        /// <summary>
        /// Список категорий магазина. Каждой категории присваиваться уникальный номер, нумерация — на усмотрение магазина.
        /// </summary>
        [XmlElement(ElementName = "categories")]
        public Categories Categories { get; set; }
        /// <summary>
        /// Список предложений магазина. 
        /// </summary>
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
