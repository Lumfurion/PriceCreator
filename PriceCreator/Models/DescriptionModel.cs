using System;
using System.ComponentModel;
using PriceCreator.Services.Validation;


namespace PriceCreator.Models
{
    class DescriptionModel:ChangeProperty,IDataErrorInfo
    {
      
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
        public DescriptionModel() { }
        public DescriptionModel(string text)
        {
            Text = text;
        }

        #region Проверка свойств
        string er { get; set; }
        public string Error
        {
            get { return er; }
        }

        public string this[string propertyName]
        {
            get
            {
                string validationResult = null;
                switch (propertyName)
                {
                    case "Text":
                        validationResult = Validation.Text(Text);
                        break;
                    default: throw new ApplicationException("Неизвестное свойство проверяется модели DescriptionModel.");
                }
                return validationResult;
            }
        }
        #endregion

    }
}
