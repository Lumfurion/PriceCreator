using Microsoft.Win32;
using PriceCreator.Models;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using PriceCreator.Views;
using PriceCreator.ManagerXml.DeserializeObjects;
using PriceCreator.ManagerXml.Serialization;

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
        public ObservableCollection<OfferModel> OffersWithCategory { get; set; }
        public OfferModel SelectOffer { get; set; }
        public SellerModel seller { get; set; }
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
        private CategoryModel selectCategory { get; set; }
        public CategoryModel SelectCategory
        {
            get { return selectCategory; }
            set 
            {
                selectCategory = value;

                if (selectCategory != null && isOpenFile)
                {   

                    var temp = seller.Offers.Where(of => of.CategoryId == SelectCategory.Id).ToList();
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
            seller = new SellerModel();
            OffersWithCategory = new ObservableCollection<OfferModel>();
            SelectedIndex = -1;
        }


        public DelegateCommand OvpenFile
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                   
                    OpenFileDialog dlg = new OpenFileDialog();
                    SaveFileDialog sld = new SaveFileDialog();
                    dlg.Filter = "XML files (*.XML)|*.XML";
                    sld.Filter = "xml files (*.xml)|*.xml";
                    if (!isOpenFile)
                    {
                        if (dlg.ShowDialog() == true)
                        {
                            PathFile = dlg.FileName;
                            initialization(PathFile);
                            isOpenFile = true;
                            if (SelectedIndex == -1)
                            {
                                SelectedIndex = 0;
                            }

                        }
                    }
                    else
                    {
                        if (sld.ShowDialog() == true)
                        {
                            PathFile = sld.FileName;
                            Generate.DataTransfer(seller, PathFile);
                            isOpenFile = false;
                            Сlean();


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
                        seller.Сategories.Add(new CategoryModel(Id, name));
                       
                    }

                });

            }
        }

        #region Добавление,изменение,удаление,товаров.
        public DelegateCommand AddProduct
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    AddProductView viewAddProduct = new AddProductView();
                    AddProductViewModel vmaddProduct = new AddProductViewModel(SelectCategory.Id, seller, OffersWithCategory);
                    viewAddProduct.DataContext = vmaddProduct;
                    viewAddProduct.Show();

                }, (obj) => SelectCategory != null);
            }
        }


        public DelegateCommand EditProduct
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    EditProductView viewEditProduct = new EditProductView();
                    EditProductViewModel vmEditProduct = new EditProductViewModel() { offer= SelectOffer};
                    viewEditProduct.DataContext = vmEditProduct;
                    viewEditProduct.ShowDialog();
                }, (obj) => SelectOffer != null);
            }
        }

        public DelegateCommand DeleteProduct
        {
            get
            {
                return new DelegateCommand((obj) =>
                {   
                    seller.Offers.Remove(SelectOffer);//удаление товара из модели.
                    OffersWithCategory.Remove(SelectOffer);//удаление из временного списка.
                    SelectOffer = null;
                }, (obj) => SelectOffer != null);
            }
        }
        #endregion


        void initialization(string path)
        {
            Сlean();
            Yml_catalog catalog;
            var serializer = new XmlSerializer(typeof(Yml_catalog));
            using (var stream = File.OpenRead(@path))
            {
                catalog = (Yml_catalog)serializer.Deserialize(stream);
            }
            var offers = catalog.Shop.Offers.Offer;
            var shop = catalog.Shop;
            seller.Name = shop.Name;
            seller.Company = shop.Company;
            seller.Url = shop.Url;
            seller.Date = catalog.Date;

            foreach (var sh in shop.Categories.Category)
            {
                seller.Сategories.Add(new CategoryModel(sh.Id, sh.Text));
            }


            int count = shop.Currencies.Currency.Count;
            for (var i = 0; i < count; i++)
            {
                seller.Currencies.Add(new CurrencyModel(shop.Currencies.Currency[i].Id, shop.Currencies.Currency[i].Rate));
            }



            foreach (var offer in offers)
            {
                seller.Offers.Add(new OfferModel(offer.Url, offer.Price, offer.CurrencyId, offer.CategoryId, offer.Name, offer.Vendor, offer.Stock_quantity, offer.Available, offer.Id));
            }

            //Характеристики
            var Offers = seller.Offers;
            for (var i = 0; i < Offers.Count; i++)
            {
                int c = offers[i].Param.Count();
                for (var t = 0; t < c; t++)
                {
                    seller.Offers[i].Param.Add(new ParamModel(offers[i].Param[t].Name, offers[i].Param[t].Text));
                }
            }
            //Картинки
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
                    Offers[i].Descriptions.Add(new DescriptionModel(match.Groups[1].Value));
                }
            }
        }
        void Сlean()
        {
            seller.Name = " ";
            seller.Company = " ";
            seller.Url = " ";
            seller.Date = " ";
            seller.Сategories.Clear();
            seller.Currencies.Clear();
            seller.Offers.Clear();
            OffersWithCategory.Clear();

        }


    }
}
