
namespace PriceCreator.Models
{
    class CategoryModel:ChangeProperty
    {
       
        int id { get; set; }
        string name { get; set; }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public CategoryModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
