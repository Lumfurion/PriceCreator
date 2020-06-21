using PriceCreator.Models;
using PriceCreator.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PriceCreator.ViewsModels
{
    class AddProductViewModel : ChangeProperty
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

        readonly SellerModel seller;
        readonly int category;
        readonly ObservableCollection<OfferModel> offers;


        public AddProductViewModel() {  }
        public AddProductViewModel(int category, SellerModel seller, ObservableCollection<OfferModel> offers)
        {
            offer = new OfferModel();
            offer.Descriptions.Add(new DescriptionModel());
            offer.Descriptions.Add(new DescriptionModel());
            offer.Descriptions.Add(new DescriptionModel());
            offer.Descriptions.Add(new DescriptionModel());
            offer.Param.Add(new ParamModel());
            offer.Param.Add(new ParamModel());
            offer.Param.Add(new ParamModel());
            offer.Param.Add(new ParamModel());
            this.seller = seller;
            this.category = category;
            this.offers = offers;
            Currencies = seller.Currencies;
            SelectedIndex = 0;

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
                    offer.CategoryId = category;
                    seller.Offers.Add(offer);
                    offers.Add(offer);
                    win.Close();
                });
            }
        }



    }
}
