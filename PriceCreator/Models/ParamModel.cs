namespace PriceCreator.Models
{
    class ParamModel : ChangeProperty
    {
        string name { get; set; }
        string text { get; set; }
     
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
       
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }


        public ParamModel() { }
        public ParamModel(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }
}
