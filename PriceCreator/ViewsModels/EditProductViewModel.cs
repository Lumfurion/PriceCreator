using PriceCreator.Models;
using PriceCreator.Views;
using System.Windows;
using System.Windows.Input;

namespace PriceCreator.ViewsModels
{
    class EditProductViewModel:ChangeProperty
    { 
        public OfferModel offer { get; set; }
        public ICommand AddImage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    AddImageView viewaddImage = new AddImageView();
                    AddImageViewModel vmAddImage = new AddImageViewModel(offer.Picture);
                    viewaddImage.DataContext = vmAddImage;
                    viewaddImage.ShowDialog();

                });
            }
        }
        public ICommand AddDescription
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    offer.Descriptions.Add(new DescriptionModel());
                });
            }
        }

        public ICommand AddParam
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    offer.Param.Add(new ParamModel());

                });
            }
        }


        public ICommand RemoveImage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    string image = (string)obj;

                    if (image != null)
                    {
                        offer.Picture.Remove(image);
                    }
                });
            }
        }


        public ICommand SaveProduct
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Window win = (Window)obj;
                   
                    win.Close();
                });
            }
        }


    }
}
