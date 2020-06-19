using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{
    [XmlRoot(ElementName = "param")]
    public class Param
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlText]
        public string Text { get; set; }
        public Param() { }
        public Param(string name, string text)
        {
            Name = name;
            Text = text;
        }

       
    }

}
