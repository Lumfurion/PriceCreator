using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace PriceCreator.ManagerXml.SerializeObjects
{
    [XmlRoot(ElementName = "offer")]
    public class Offer
    {   /// <summary>
        /// Ссылка на товар на вашем сайте.
        /// </summary>
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        /// <summary>
        /// Цена товара.
        /// </summary>
        [XmlElement(ElementName = "price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Валюта, в которой указана цена товара: UAH, USD, EUR, RUR.
        /// </summary>
        [XmlElement(ElementName = "currencyId")]
        public string CurrencyId { get; set; }
        /// <summary>
        /// ID категории, к которой привязан данный товар.
        /// </summary>
        [XmlElement(ElementName = "categoryId")]
        public int CategoryId { get; set; }
        /// <summary>
        /// Ссылка на фото товара.
        /// </summary>
        [XmlElement(ElementName = "picture")]
        public List<string> Picture { get; set; } = new List<string>();
        /// <summary>
        /// Название товара.
        /// </summary>
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        /// <summary>
        /// Бренд-производитель товара.
        /// </summary>
        [XmlElement(ElementName = "vendor")]
        public string Vendor { get; set; }
        /// <summary>
        /// Описание товара.
        /// </summary>
        private string _description;
        [XmlElement(ElementName = "description")]
        public XmlCDataSection Description 
        {
            get => new XmlDocument().CreateCDataSection(_description);
            set => _description = value.Value;
        }
        /// <summary>
        /// Характеристики (параметры) товара.
        /// </summary>
        [XmlElement(ElementName = "param")]
        public List<Param> Param { get; set; } = new List<Param>();
        /// <summary>
        /// Остатки товара. Товар будет в наличии на сайте до тех пор, пока этот параметр больше 0.
        /// </summary>
        [XmlElement(ElementName = "stock_quantity")]
        public int Stock_quantity { get; set; }
        /// <summary>
        ///Атрибут available — указывает наличие товара: true — товар в наличии, false — товар не в наличии.
        /// </summary>
        [XmlAttribute(AttributeName = "available")]
        public bool Available { get; set; }
        /// <summary>
        ///Должен оставаться неизменным, допускается использование символов: Aa-Zz и 0-9, не допускается использование кириллицы и пробелов.
        /// </summary>
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        public Offer() { }
        public Offer(string url, decimal price, string currencyId, int categoryId, string name, string vendor, int stock_quantity, bool available, int id)
        {
            Url = url;
            Price = price;
            CurrencyId = currencyId;
            CategoryId = categoryId;
            Name = name;
            Vendor = vendor;
            Stock_quantity = stock_quantity;
            Available = available;
            Id = id;   
        }
       


    }
}
