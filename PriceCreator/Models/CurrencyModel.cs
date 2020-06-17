
namespace PriceCreator.Models
{
    class CurrencyModel:ChangeProperty
    {
        string id { get; set; }
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        int rate { get; set; }
        public int Rate
        {
            get { return rate; }
            set
            {
                rate = value;
                OnPropertyChanged("Rate");
            }
        }
        public CurrencyModel(string id, int rate)
        {
            Id = id;
            Rate = rate;
        }
    }
}
