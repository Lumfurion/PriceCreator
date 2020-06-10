using System.Collections.ObjectModel;
namespace PriceCreator.Models
{
    class Seller : ChangeProperty
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
        public ObservableCollection<Category> Сategories { get; set; }
        public ObservableCollection<Currency> Currencies { get; set; }
        public ObservableCollection<Offer> Offers { get; set; }


        public Seller()
        {
            Сategories = new ObservableCollection<Category>();
            Currencies = new ObservableCollection<Currency>();
            Offers = new ObservableCollection<Offer>();
        }

    }
}
