namespace PriceCreator.Models
{
    class DescriptionModel:ChangeProperty
    {
        string text { get; set; }
        public string Text
        {
            get { return text; }
            set 
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public DescriptionModel() { }
        
        public DescriptionModel(string text)
        {
            Text = text;
        }

    }
}
