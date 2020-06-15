namespace PriceCreator.Models
{
    class Description:ChangeProperty
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

        public Description() { }
        
        public Description(string text)
        {
            Text = text;
        }

    }
}
