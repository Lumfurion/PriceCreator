using System.Collections.ObjectModel;
namespace PriceCreator.Models
{
    class SellerModel : ChangeProperty
    {
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

        string сompany { get; set; }
        public string Company
        {
            get { return сompany; }
            set
            {
                сompany = value;
                OnPropertyChanged("Company");
            }
        }
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
        string date { get; set; }
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public ObservableCollection<CategoryModel> Сategories { get; set; }
        public ObservableCollection<CurrencyModel> Currencies { get; set; }
        public ObservableCollection<OfferModel> Offers { get; set; }


        public SellerModel()
        {
            Сategories = new ObservableCollection<CategoryModel>();
            Currencies = new ObservableCollection<CurrencyModel>();
            Offers = new ObservableCollection<OfferModel>();
        }

    }
}
