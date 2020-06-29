using PriceCreator.Models;
using PriceCreator.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PriceCreator.ViewsModels
{
    class EditProductViewModel:ChangeProperty
    { 
        public OfferModel offer { get; set; }
        public ObservableCollection<CurrencyModel> Currencies { get; set; }
        private CurrencyModel currencySelected { get; set; }
        public CurrencyModel CurrencySelected
        {
            get { return currencySelected; }
            set { currencySelected = value; offer.CurrencyId = currencySelected.Id; }
        }
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

        public EditProductViewModel() { }
        
        public EditProductViewModel(OfferModel offer, ObservableCollection<CurrencyModel> currencies)
        {
            this.offer = offer;
            Currencies = currencies;
            SelectedIndex = 0;
        }

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
                    offer.Descriptions.Remove(descriptionModel);
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
                {
                    ParamModel paramModel = (ParamModel)obj;
                    offer.Param.Remove(paramModel);

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


        public ICommand SaveProduct
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Window win = (Window)obj;
                   
                    win.Close();
                });
            }
        }


    }
}
