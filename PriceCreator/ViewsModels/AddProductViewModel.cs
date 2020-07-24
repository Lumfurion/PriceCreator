using PriceCreator.Models;
using PriceCreator.Services.Validation;
using PriceCreator.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PriceCreator.ViewsModels
{
    class AddProductViewModel : ChangeProperty, IDataErrorInfo
    {

        #region Для провеки свойств
        /// <summary>
        /// Хранения валидности свойств.
        /// </summary>
        private Dictionary<string, bool> validProperties;
        /// <summary>
        /// Включить кнопку если все свойства валлидни.
        /// </summary>
        private bool allPropertiesValid = false;
        public bool AllPropertiesValid
        {
            get { return allPropertiesValid; }
            set
            {
                if (allPropertiesValid != value)
                {
                    allPropertiesValid = value;
                    OnPropertyChanged("AllPropertiesValid");
                }
            }
        }

        #endregion

        #region Свойства AddProductViewModel
        /// <summary>
        /// Товар корый будет дабавлен.
        /// </summary>
        readonly OfferModel offer;
        public ObservableCollection<CurrencyModel> Currencies { get; set; }
        /// <summary>
        /// Выбраная валюта.
        /// </summary>
        private CurrencyModel currencySelected { get; set; }
        public CurrencyModel CurrencySelected
        {
            get { return currencySelected; }
            set 
            { 
                currencySelected = value; 
            }
        }
        /// <summary>
        /// Индекс валюты.
        /// </summary>
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

        readonly SellerModel seller;
        /// <summary>
        /// Номер категории.
        /// </summary>
        readonly int category;
        readonly ObservableCollection<OfferModel> offers;
        #endregion

        #region Свойства товара 
        public bool Available
        {
            get { return (offer != null) ? offer.Available : false; }
            set
            {
                if (offer.Available != value)
                {
                    offer.Available = value;
                    OnPropertyChanged("Available");
                }
            }
        }

        public string Name
        {
            get { return (offer != null) ? offer.Name : null; }
            set
            {
                if (offer.Name != value)
                {
                    offer.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Url
        {
            get { return (offer != null) ? offer.Url : null; }
            set
            {
                if (offer.Url != value)
                {
                    offer.Url = value;
                    OnPropertyChanged("Url");
                }
            }
        }


        public decimal Price
        {
            get { return (offer != null) ? offer.Price : 0; }
            set
            {
                if (offer.Price != value)
                {  
                    offer.Price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        public string Vendor
        {
            get { return (offer != null) ? offer.Vendor : null;}
            set
            {
                if (offer.Vendor != value)
                {
                    offer.Vendor = value;
                    OnPropertyChanged("Vendor");
                }
            }
        }

        public int Stock_quantity
        {
            get { return (offer != null) ? offer.Stock_quantity : 0; }
            set
            {
                if (offer.Stock_quantity != value)
                {
                    offer.Stock_quantity = value;
                    Available = (Stock_quantity > 0) ? true : false;//устанавливать что есть товар в наличии.
                    OnPropertyChanged("Stock_quantity");
                }
            }
        }



        public ObservableCollection<string> Picture
        {
            get { return (offer != null) ? offer.Picture : null; }
            set
            {
                if (offer.Picture != value)
                {
                    offer.Picture = value;
                    OnPropertyChanged("Picture");
                }
            }
        }



        public ObservableCollection<DescriptionModel> Descriptions
        {
            get{ return (offer != null) ? offer.Descriptions : null; }
            set
            {
                if (offer.Descriptions != value)
                {
                    offer.Descriptions = value;
                    OnPropertyChanged("Descriptions");
                }
            }
        }

        public ObservableCollection<ParamModel> Param
        {
            get { return (offer != null) ? offer.Param : null; }
            set
            {
                if (offer.Param != value)
                {
                    offer.Param = value;
                    OnPropertyChanged("Param");
                }
            }
        }

        #endregion

        public AddProductViewModel() { }
        public AddProductViewModel(int category, SellerModel seller, ObservableCollection<OfferModel> offers)
        {
            offer = new OfferModel();
            #region Валидации данных
            validProperties = new Dictionary<string, bool>();//нужен для того чтобы включить кнопку добавленные если все данные будут коректные.
            validProperties.Add("Name", false);
            validProperties.Add("Url", false);
            validProperties.Add("Price", false);
            validProperties.Add("Vendor", false);
            validProperties.Add("Stock_quantity", false);
            validProperties.Add("Picture", false);
            validProperties.Add("Descriptions", false);
            validProperties.Add("Param", false);
            //Проверки на валидность ввода Picture Descriptions Param.
            Picture.CollectionChanged += (s, e) =>
            {
                validProperties["Picture"] = Picture.Count() > 0 ? true : false;
                ValidateProperties();
            };
            Descriptions.CollectionChanged += (s, e) =>
            {

                bool isEmpty = false;
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (DescriptionModel item in e.NewItems)//Добавление новые элементы
                    {
                        isEmpty = Validation.TextBool(item.Text) ? false : true;
                        item.PropertyChanged += (sender, argument) => //Проходимся по свойствам модели.
                        {
                            int i = 0;
                            foreach (var des in Descriptions)
                            {
                                if (Validation.TextBool(des.Text))
                                {
                                    i++;
                                }
                            }
                            validProperties["Descriptions"] = (i == 0) ? true : false;
                            isEmpty = (i == 0) ? true : false;
                            ValidateProperties();

                        };

                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    int i = 0;
                    foreach (var des in Descriptions)
                    {
                        if (Validation.TextBool(des.Text))
                        {
                            i++;
                        }
                    }
                    isEmpty = (i == 0) ? true : false;
                }

                validProperties["Descriptions"] = (Descriptions.Count > 0 && isEmpty != false) ? true : false;
                ValidateProperties();
            };
            Param.CollectionChanged += (s, e) =>
            {
                bool isEmpty = false;
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (ParamModel item in e.NewItems)//Добавление новые элементы
                    {

                        var name = Validation.NameBoolParam(item.Name);
                        var text = Validation.TextBoolParam(item.Text);
                        validProperties["Param"] = (name && text) ?  false: true;
                        item.PropertyChanged += (sender, argument) => //Проходимся по свойствам модели.
                        {
                            int nameisvalid = 0;
                            int textisvalid = 0;
                            foreach (var par in Param)//Ищем неправильно заполнены параметры.
                            {
                                if (Validation.NameBoolParam(par.Name)) nameisvalid++;
                                if (Validation.TextBoolParam(par.Text)) textisvalid++;
                            }
                            validProperties["Param"] = (nameisvalid == 0 && textisvalid == 0) ? true : false;
                            isEmpty = (nameisvalid == 0 && textisvalid == 0) ? true : false;
                            ValidateProperties();

                        };

                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    int nameisvalid = 0;
                    int textisvalid = 0;
                    foreach (var par in Param)
                    {
                        if (Validation.NameBoolParam(par.Name)) nameisvalid++;
                        if (Validation.TextBoolParam(par.Text)) textisvalid++;
                    }
                    isEmpty = (nameisvalid == 0 && textisvalid == 0) ? true : false;
                }

                validProperties["Param"] = (Param.Count > 0 && isEmpty != false) ? true : false;
                ValidateProperties();
            };
            #endregion

            Descriptions.Add(new DescriptionModel());
            Descriptions.Add(new DescriptionModel());
            Descriptions.Add(new DescriptionModel());
            Descriptions.Add(new DescriptionModel());
            Param.Add(new ParamModel());
            Param.Add(new ParamModel());
            Param.Add(new ParamModel());
            Param.Add(new ParamModel());

            this.seller = seller;
            this.category = category;
            this.offers = offers;
            Currencies = seller.Currencies;
            SelectedIndex = 0;
            
        }

        #region Каманды
        public ICommand OpenImage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    string image = (string)obj;
                    if (image != null)
                    {
                        var iv = new ImageViewer()
                        {
                            DataContext = new ImageViewerViewModel
                            {
                                Image = image
                            }
                        };
                        iv.ShowDialog();
                    }
                });
            }
        }

        public ICommand AddImage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    AddImageView viewaddImage = new AddImageView();
                    AddImageViewModel vmAddImage = new AddImageViewModel(offer.Picture);
                    viewaddImage.DataContext = vmAddImage;
                    viewaddImage.ShowDialog();

                });
            }
        }
        public ICommand AddDescription
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    offer.Descriptions.Add(new DescriptionModel());
                });
            }
        }

        public ICommand DeleteDescriptions
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    DescriptionModel descriptionModel = (DescriptionModel)obj;
                    var index = offer.Descriptions.IndexOf(descriptionModel);// Получаем индекс  описания товара.
                    offer.Descriptions.RemoveAt(index);
                });
            }
        }

        public ICommand AddParam
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    offer.Param.Add(new ParamModel());

                });
            }
        }

        public ICommand DeleteParam
        {
            get
            {
                return new DelegateCommand((obj) =>
                {   ParamModel paramModel = (ParamModel)obj;
                    var index = offer.Param.IndexOf(paramModel);// Получаем индекс  параметра товара.
                    offer.Param.RemoveAt(index);
                });
            }
        }


        public ICommand RemoveImage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    string image = (string)obj;
                    if (image != null)
                    {
                        offer.Picture.Remove(image);
                    }
                });
            }
        }

        public ICommand AddProduct
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Window win = (Window)obj;
                    var lastoffer =  seller.Offers.Where(o => o.CategoryId == category).LastOrDefault();
                    var firstoffer = seller.Offers.Where(o => o.CategoryId == category).FirstOrDefault();

                    if (lastoffer != null)
                    {
                        offer.Id = lastoffer.Id + 1;
                    }
                    else if(firstoffer != null)
                    {
                        offer.Id = firstoffer.Id + 1;
                    }
                    else
                    {
                        offer.Id = 1;
                    }
                    offer.CurrencyId = CurrencySelected.Id;
                    offer.CategoryId = category;//установка категории ади.
                    seller.Offers.Add(offer);//добавленые в продовца 
                    offers.Add(offer);//добавленые в временый список для combobox.
                    win.Close();
                });
            }
        }

        #endregion

        #region Реализация IDataErrorInfo 

        public string Error
        {
            get { return (offer as IDataErrorInfo).Error; }
        }

        public string this[string propertyName]
        {
            get
            {   if(offer != null)
                {
                    string error = (offer as IDataErrorInfo)[propertyName];
                    validProperties[propertyName] = string.IsNullOrEmpty(error) ? true : false;
                    ValidateProperties();
                    CommandManager.InvalidateRequerySuggested();
                    return error;
                }
                return null;
            }
        }

        private void ValidateProperties()
        {
            foreach (bool isValid in validProperties.Values)
            {
                if (!isValid)
                {
                    AllPropertiesValid = false;
                    return;
                }
            }
            AllPropertiesValid = true;
        }

        #endregion

    }
}
