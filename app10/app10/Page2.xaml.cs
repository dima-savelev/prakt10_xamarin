using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
        }
        string _folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
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
            StreamWriter outFile = new StreamWriter(Path.Combine(_folderPath, "settings.txt"));
            outFile.WriteLine(fio.Text);
            outFile.WriteLine(name.Text);
            outFile.WriteLine(lastName.Text);
            outFile.WriteLine(dateBirth.Date);
            outFile.WriteLine(gender.SelectedItem);
            outFile.WriteLine(hostel.SelectedItem);
            outFile.WriteLine(headman.SelectedItem);
            outFile.WriteLine(image.ImageSource.ToString().Substring(6));
            outFile.Close();
            DisplayAlert("Выполнено!", "Данные сохранены", "OK");
        }

        private void Open_Clicked(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(_folderPath, "settings.txt")) == true)
            {
                StreamReader outFile = new StreamReader(Path.Combine(_folderPath, "settings.txt"));
                fio.Text = outFile.ReadLine();
                name.Text = outFile.ReadLine();
                lastName.Text = outFile.ReadLine();
                dateBirth.Date = Convert.ToDateTime(outFile.ReadLine());
                gender.SelectedItem = outFile.ReadLine();
                hostel.SelectedItem = outFile.ReadLine();
                headman.SelectedItem = outFile.ReadLine();
                image.ImageSource = outFile.ReadLine();
                outFile.Close();
            }
            else DisplayAlert("Ошибка", "Файла с сохранение не обнаружено", "OK");
        }
    }
}