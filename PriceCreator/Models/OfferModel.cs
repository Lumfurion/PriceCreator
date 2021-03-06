﻿using PriceCreator.Services.Validation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace PriceCreator.Models
{
    class OfferModel:ChangeProperty,IDataErrorInfo
    {
        #region Cвойства
        string url { get; set; }
        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                OnPropertyChanged("Url");
            }
        }

        decimal price { get; set; }
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        string currencyId { get; set; }
        public string CurrencyId
        {
            get { return currencyId; }
            set
            {
                currencyId = value;
                OnPropertyChanged("CurrencyId");
            }
        }

        int categoryId { get; set; }
        public int CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }
        public ObservableCollection<string> Picture { get; set; } = new ObservableCollection<string>();
        string name { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        string vendor { get; set; }
        public string Vendor
        {
            get { return vendor; }
            set
            {
                vendor = value;
                OnPropertyChanged("Vendor");
            }
        }
        public ObservableCollection<DescriptionModel> Descriptions { get; set; } = new ObservableCollection<DescriptionModel>();
        public ObservableCollection<ParamModel> Param { get; set; } = new ObservableCollection<ParamModel>();
       
        int stock_quantity { get; set; }
        public int Stock_quantity
        {
            get { return stock_quantity; }
            set
            {
                stock_quantity = value;
                OnPropertyChanged("Stock_quantity");
            }
        }

        bool аvailable { get; set; }
        public bool Available
        {
            get { return аvailable; }
            set
            {
                аvailable = value;
                OnPropertyChanged("Available");
            }
        }

        int id { get; set; }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        #endregion

        #region Проверка свойств
        string er { get; set; }
        public string Error
        {
            get { return er; }
        }

        public string this[string propertyName]
        {
            get
            {
                string validationResult = null;
                switch (propertyName)
                {
                    case "Name":
                        validationResult = Validation.Name(Name);
                        break;
                    case "Url":
                        validationResult = Validation.Ui(Url);
                        break;
                    case "Price":
                        validationResult = Validation.Price(Price);
                        break;
                    case "Vendor":
                        validationResult = Validation.Vendor(Vendor);
                        break;
                    case "Stock_quantity":
                        validationResult = Validation.StockQuantity(Stock_quantity);
                        break;
                    default:throw new ApplicationException("Неизвестное свойство проверяется модели OfferModel.");
                }
                return validationResult;
            }
        }
        #endregion
        public OfferModel(){}
        public OfferModel(string url, decimal price, string currencyId, int categoryId, string name, string vendor, int stock_quantity, bool available, int id)
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
