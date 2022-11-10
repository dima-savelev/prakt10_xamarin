using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app10
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void dateBirth_DateSelected(object sender, DateChangedEventArgs e)
        {
            int age = DateTime.Now.Year - dateBirth.Date.Year;
            ageText.Text = "Возраст - " + age.ToString();
        }

        private async void addPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var options = new PickOptions
                {
                    PickerTitle = "Выберите картинку",
                    FileTypes = FilePickerFileType.Images,
                };
                var result = await FilePicker.PickAsync(options);
                image.ImageSource = result.FullPath;
            }
            catch { };
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            if (dateBirth.TextColor == Color.Red)
            {
                DisplayAlert("Ошибка", "Ты что еще не родился?", "OK");
                return;
            }
            if (string.IsNullOrEmpty(fio.Text) || string.IsNullOrEmpty(name.Text) || string.IsNullOrEmpty(lastName.Text) || string.IsNullOrEmpty(image.ImageSource.ToString()))
            {
                DisplayAlert("Ошибка", "Не все данные заполнены", "OK");
                return;
            }
            App.Current.Properties["FIO"] = fio.Text;
            App.Current.Properties["Name"] = name.Text;
            App.Current.Properties["LastName"] = lastName.Text;
            App.Current.Properties["Date"] = dateBirth.Date;
            App.Current.Properties["Gender"] = gender.SelectedItem;
            App.Current.Properties["Hostel"] = hostel.SelectedItem;
            App.Current.Properties["Headman"] = headman.SelectedItem;
            App.Current.Properties["Image"] = image.ImageSource.ToString().Substring(6);
            App.Current.SavePropertiesAsync();
            DisplayAlert("Выполнено!", "Данные сохранены", "OK");
        }

        private void Open_Clicked(object sender, EventArgs e)
        {
            fio.Text = App.Current.Properties["FIO"].ToString();
            name.Text = App.Current.Properties["Name"].ToString();
            lastName.Text = App.Current.Properties["LastName"].ToString();
            dateBirth.Date = (DateTime)App.Current.Properties["Date"];
            gender.SelectedItem = App.Current.Properties["Gender"];
            hostel.SelectedItem = App.Current.Properties["Hostel"];
            headman.SelectedItem = App.Current.Properties["Headman"];
            image.ImageSource = App.Current.Properties["Image"].ToString();
        }
    }
}