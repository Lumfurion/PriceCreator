namespace PriceCreator.ViewsModels
{
    class ImageViewerViewModel:ChangeProperty
    {
        string image { get; set; }
        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }



    }
}
