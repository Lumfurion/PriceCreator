
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{   /// <summary>
    /// Список курсов валют магазина. 
    /// </summary>
    [XmlRoot(ElementName = "currencies")]
    public class Currencies
    {
        [XmlElement(ElementName = "currency")]
        public List<Currency> Currency { get; set; }

        public Currencies()
        {
            Currency = new List<Currency>();
        }
        public void Add(string id, int rate)
        {
            Currency.Add(new Currency(id, rate));
        }

    }
}
