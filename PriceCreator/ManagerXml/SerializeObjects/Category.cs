using System;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{
    [XmlRoot(ElementName = "category")]
    public class Category
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }
        [XmlText]
        public string Text { get; set; }
        public Category() { }
        public Category(int id, string text)
        {
            Id = id;
            Text = text;
        }

        
    }
}
