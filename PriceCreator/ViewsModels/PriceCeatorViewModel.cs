using Microsoft.Win32;
using System.Linq;
using System.Collections.ObjectModel;
using PriceCreator.Views;
using PriceCreator.ManagerXml.Serialization;
using PriceCreator.ManagerXml;
using PriceCreator.Models;

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
        public OfferModel selectOffer { get; set; }
        public OfferModel SelectOffer
        {
            get { return selectOffer; }
            set
            {
                selectOffer = value;
                OnPropertyChanged("SelectOffer");
            }
        }
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
                            //GetDataXml.Run(PathFile,seller);
                            GetDataXml.Test(PathFile, seller);
                            OffersWithCategory.Clear();
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
                            Generate.DataTransfer(seller,PathFile);
                            OffersWithCategory.Clear();
                            isOpenFile = false;
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
                    viewAddProduct.ShowDialog();
                    SelectOffer = OffersWithCategory.FirstOrDefault();
                }, (obj) => SelectCategory != null);
            }
        }


        public DelegateCommand EditProduct
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var  indexOffersWithCategory = seller.Offers.IndexOf(SelectOffer);
                    var indexOffer = OffersWithCategory.IndexOf(SelectOffer);
                    EditProductView viewEditProduct = new EditProductView();
                    EditProductViewModel vmEditProduct = new EditProductViewModel(SelectOffer, seller, OffersWithCategory)
                    {   
                        Currencies = seller.Currencies,
                        IndexOffer = indexOffer,
                        indexOffersWithCategory = indexOffersWithCategory
                    };
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
                    SelectOffer = OffersWithCategory.FirstOrDefault();
                }, (obj) => SelectOffer != null);
            }
        }
        #endregion
    }
}
