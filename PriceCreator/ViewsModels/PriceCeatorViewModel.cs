using Microsoft.Win32;
using PriceCreator.Models;
using System.Collections.ObjectModel;

namespace PriceCreator.ViewsModels
{
    class PriceCeatorViewModel: ChangeProperty
    {
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
        public ObservableCollection<string> Сategories { get; set; }
        public ObservableCollection<Currency> Currencies { get; set; } 
        public ObservableCollection<Offer> Offers { get; set; } 



        public PriceCeatorViewModel()
        {
            PathFile = "Name file";
            Сategories = new ObservableCollection<string>();
            Currencies = new ObservableCollection<Currency>();
            Offers = new ObservableCollection<Offer>();

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
                    }
                });
              
            }
        }



    }
}
