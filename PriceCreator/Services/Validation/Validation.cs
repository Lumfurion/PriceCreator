using System.Collections.ObjectModel;

namespace PriceCreator.Services.Validation
{
    static class Validation
    {
        public static string Name(string Name)
        {
            if (string.IsNullOrEmpty(Name))
                return "Название продукта необходимо ввести.";
            else if (Name.Length < 6)
                return "Название продукта должно иметь более 6 букв.";
            else
                return string.Empty;
        }

        public static string Ui(string Url)
        {
            if (string.IsNullOrEmpty(Url))
                return "Пустой адрес";
            else if (!Url.StartsWith("http://"))
                return "Некорректный адрес сайта,проверьте адрес.(начало сайта должно начинаться http://)";
            else return string.Empty;

        }

        public static string Price(decimal Price)
        {
            if (Price == 0)
                return "Цена товара не может быть 0!!";
            else return string.Empty;
        }

        public static string StockQuantity(int Stock_quantity)
        {

            if (Stock_quantity == 0)
                return "Количество товара не может быть 0.(Если количество товара будет стоять 0 значит в продаже товара не будет)";
            else return string.Empty;
        }


        public static string Vendor(string Vendor)
        {
            if (string.IsNullOrEmpty(Vendor))
                return "Имя продавца товара необходимо ввести.";
            else if (Vendor.Length < 5)
                return "Имя продавца должно иметь более 5 букв.";
            else
                return string.Empty;
        }

        //DescriptionModel-Провека свойства  Text.
        public static string Text(string Text)
        {
            if (string.IsNullOrEmpty(Text))
                return "Необходимо ввести описания  товара.";
            else if (Text.Length < 7)
                return "Описания  товара должно иметь более 7 букв.";
            else
                return string.Empty;
        }
        public static bool TextBool(string Text)
        {
            bool rez = false;
            if (string.IsNullOrEmpty(Text))
                rez = true;
            else if (Text.Length < 7)
                rez = true;

            return rez;
        }
        //ParamModel-Провека свойств  Name,Text.
        public static string NameParam(string Name)
        {
            if (string.IsNullOrEmpty(Name))
                return "Необходимо ввести  характеристику параметра.";
            else if (Name.Length > 50)
                return "Слишком большой размер характеристики.";
            else
                return string.Empty;
        }
        public static string TextParam(string Text)
        {
            if (string.IsNullOrEmpty(Text))
                return "Необходимо ввести  значение параметра.";
            else if (Text.Length > 150)
                return "Слишком большой размер  значение.";
            else return string.Empty;
        }


        public static bool NameBoolParam(string Name)
        {
            bool rez = false;
            if (string.IsNullOrEmpty(Name))
                rez = true;
            else if (Name.Length > 50)
                rez = true;
          
                return rez;
        }
        public static bool TextBoolParam(string Text)
        {
            bool rez = false;
            if (string.IsNullOrEmpty(Text))
                rez = true;
            else if (Text.Length > 150)
                rez = true;
            return rez;
        }


    }
}
