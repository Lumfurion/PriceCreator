using System;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{
    [XmlRoot(ElementName = "category")]
    public class Category
    {   /// <summary>
        /// Айди категории.
        /// </summary>
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }
        /// <summary>
        /// Название категории.
        /// </summary>
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
