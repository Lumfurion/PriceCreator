using System.Collections.Generic;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{   /// <summary>
    /// Список предложений магазина. 
    /// </summary>
    [XmlRoot(ElementName = "offers")]
    public class Offers
    {   /// <summary>
        /// Карточка товара. 
        /// </summary>
        [XmlElement(ElementName = "offer")]
        public List<Offer> Offer { get; set; } = new List<Offer>();

        public Offers() { }
       
        public void AddInfo(string url, decimal price, string currencyId, int categoryId, string name, string vendor, int stock_quantity, bool available, int id)
        {
            Offer.Add(new Offer(url, price, currencyId, categoryId, name, vendor, stock_quantity, available, id));
        }

       


    }
}
