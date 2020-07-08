using System;
using System.ComponentModel;
using PriceCreator.Services.Validation;
namespace PriceCreator.Models
{
    class ParamModel :ChangeProperty,IDataErrorInfo
    {
        string name { get; set; }
        string text { get; set; }
        /// <summary>
        /// Xарактеристику параметра.
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        /// <summary>
        /// Значение параметра.
        /// </summary>
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
                    case "Name":
                        validationResult = Validation.NameParam(Name);
                        break;
 
                    case "Text":
                        validationResult = Validation.TextParam(Text);
                        break;
                    default:throw new ApplicationException("Неизвестное свойство проверяется модели ParamModel.");
                }
                return validationResult;
            }
        }
        #endregion


    }
}
