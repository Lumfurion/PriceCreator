using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{
    [XmlRoot(ElementName = "param")]
    public class Param
    {   /// <summary>
        /// Xарактеристику параметра.
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        /// <summary>
        /// Значение параметра.
        /// </summary>
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
