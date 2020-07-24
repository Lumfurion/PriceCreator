using System.Collections.Generic;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{  
    [XmlRoot(ElementName = "categories")]
    public class Categories
    {   /// <summary>
        ///Список категорий магазина. 
        /// </summary>
        [XmlElement(ElementName = "category")]
        public List<Category> Category { get; set; }
        public Categories()
        {
            Category = new List<Category>();
        }
       
        public void Add(int id, string text)
        {
            Category.Add(new Category(id, text));
        }

    }
}
