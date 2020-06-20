using System.Collections.ObjectModel;
namespace PriceCreator.Models
{
    class DescriptionModel:ChangeProperty
    {
        private ObservableCollection<string> features = new ObservableCollection<string>();
        private string text { get; set; }

        public string Text
        {
            get { return text; }
            set 
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public ObservableCollection<string> Features
        {
            get { return features; }
            set
            {
                features = value;
                OnPropertyChanged(nameof(Features));
            }
        }
        public DescriptionModel() { }
       
        public DescriptionModel(string text)
        {
            Text = text;
        }

        public DescriptionModel(string text, ObservableCollection<string> features)
        {
            Text = text;
            Features = features;
        }

    }
}
