using System.Xml.Serialization;
namespace PriceCreator.ManagerXml.SerializeObjects
{
    [XmlRoot(ElementName = "currency")]
    public class Currency
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
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
