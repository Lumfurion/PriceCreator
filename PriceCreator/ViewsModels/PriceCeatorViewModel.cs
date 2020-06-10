using Microsoft.Win32;
using PriceCreator.Models;
using ParserXML.Parserse;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace PriceCreator.ViewsModels
{
    class PriceCeatorViewModel : ChangeProperty
    {
        #region Cвойства
        int selectedIndex { get; set; }
       

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }


        public ObservableCollection<Models.Offer> OffersWithCategory { get; set; }
        
        /// <summary>
        /// Модель продавец
        /// </summary>
        public Seller seller { get; set; }
        bool isOpenFile { get; set; } = false;
        string pathFile { get; set; }
        public string PathFile
        {
            get { return pathFile; }
            set
            {
                pathFile = value;
                OnPropertyChanged("PathFile");
            }
        }

        private string newcategory;
        public string NewCategory
        {
            get { return newcategory; }
            set
            {
                newcategory = value;
                OnPropertyChanged("NewCategory");
            }
        }

        /// <summary>
        /// Выбраная категория.
        /// </summary>
        private Models.Category selectCategory { get; set; }
        public Models.Category SelectCurrency
        {
            get { return selectCategory; }
            set 
            {
                selectCategory = value;

                if (selectCategory != null && isOpenFile)
                {   

                    var temp = seller.Offers.Where(of => of.CategoryId == SelectCurrency.Id).ToList();
                    OffersWithCategory.Clear();
                    foreach (var item in temp)
                    {
                        OffersWithCategory.Add(item);
                    }
                  
                }
                
            }
        }

        
        #endregion

        public PriceCeatorViewModel()
        {
            PathFile = "Name file";
            seller = new Seller();
            OffersWithCategory = new ObservableCollection<Models.Offer>();
            SelectedIndex = -1;
        }


        public DelegateCommand OvpenFile
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    const string Filter = "XML files (*.XML)|*.XML";
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = Filter;
                    if (dlg.ShowDialog() == true)
                    {
                        PathFile = dlg.FileName;
                        initialization(PathFile);
                        isOpenFile = true;
                        if (SelectedIndex == -1 )
                        {
                            SelectedIndex = 0;
                        }
                    }
                });
              
            }
        }

        public DelegateCommand AddCategory
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    
                    if (!string.IsNullOrWhiteSpace(NewCategory) && !string.IsNullOrEmpty(NewCategory) && isOpenFile)
                    {
                        int Id = seller.Сategories.LastOrDefault().Id + 1;//Последный адишник.
                        string name = NewCategory;
                        seller.Сategories.Add(new Models.Category(Id, name));  
                    }

                });

            }
        }

        public void Сlean()
        {
            seller.Name = " ";
            seller.Company = " ";
            seller.Url = " ";
            seller.Date = " ";
            seller.Сategories.Clear();
            seller.Currencies.Clear();
            seller.Offers.Clear();

        }

        public void initialization(string path)
        {
            Сlean();
            Yml_catalog catalog;
            var serializer = new XmlSerializer(typeof(Yml_catalog));
            using (var stream = File.OpenRead(@path)) catalog = (Yml_catalog)serializer.Deserialize(stream);
            var offers = catalog.Shop.Offers.Offer;
            var shop = catalog.Shop;
            seller.Name = shop.Name;
            seller.Company = shop.Company;
            seller.Url = shop.Url;
            seller.Date = catalog.Date;

            foreach (var sh in shop.Categories.Category)
            {
                seller.Сategories.Add(new Models.Category(sh.Id, sh.Text));
            }


            int count = shop.Currencies.Currency.Count;
            for (var i = 0; i < count; i++)
            {
                seller.Currencies.Add(new Models.Currency(shop.Currencies.Currency[i].Id, shop.Currencies.Currency[i].Rate));
            }



            foreach (var offer in offers)
            {
                seller.Offers.Add(new Models.Offer(offer.Url, offer.Price, offer.CurrencyId, offer.CategoryId, offer.Name, offer.Vendor, offer.Stock_quantity, offer.Available, offer.Id));
            }


            var Offers = seller.Offers;
            for (var i = 0; i < Offers.Count; i++)
            {
                for (var t = 0; t < 6; t++)
                {
                    seller.Offers[i].Param.Add(new Models.Param(offers[i].Param[t].Name, offers[i].Param[t].Text));
                }
            }

            for (var i = 0; i < Offers.Count; i++)
            {
                int c = offers[i].Picture.Count;
                for (var t = 0; t < c; t++)
                {
                    Offers[i].Picture.Add(offers[i].Picture[t]);
                }
            }


            var pattern = @"<p>(.+)</p>";
            for (var i = 0; i < Offers.Count; i++)
            {
                foreach (Match match in Regex.Matches(offers[i].Description, pattern))
                {
                    Offers[i].Description.Add(match.Groups[1].Value);
                }
            }
        }



    }
}
