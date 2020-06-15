using System.Collections.ObjectModel;
using System.Windows;

namespace PriceCreator.ViewsModels
{
    class AddImageViewModel : ChangeProperty
    {
      
        public ObservableCollection<string> Picture { get; set; }
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
        public AddImageViewModel() { }
       
        public AddImageViewModel(ObservableCollection<string> picture)
        {
            Picture = picture;
        }

        public DelegateCommand AddImage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Window window = (Window)obj;
                    Picture.Add(Url);
                    window.Close();


                },(obj)=> !string.IsNullOrEmpty(Url));
            }
        }

    }
}
