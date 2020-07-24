using System.Xml.Serialization;
namespace PriceCreator.ManagerXml.SerializeObjects
{
    [XmlRoot(ElementName = "currency")]
    public class Currency
    {   /// <summary>
        /// Значение валют.
        /// </summary>
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        /// <summary>
        /// Коэффициент.
        /// </summary>
        [XmlAttribute(AttributeName = "rate")]
        public int Rate { get; set; }
        public Currency() { }
        public Currency(string id, int rate)
        {
            Id = id;
            Rate = rate;
        }

       
    }
}
