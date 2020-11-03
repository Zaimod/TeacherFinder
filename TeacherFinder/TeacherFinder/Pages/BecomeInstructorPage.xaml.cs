using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherFinder.Helpers;
using TeacherFinder.Models;
using TeacherFinder.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeacherFinder.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BecomeInstructorPage : ContentPage
    {
        private MediaFile file;
        public BecomeInstructorPage()
        {
            InitializeComponent();
        }

        private async void TapCamera_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Custom,
                CompressionQuality = 60,
                CustomPhotoSize = 30, 
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            ImgProfile.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void BtnApply_Clicked(object sender, EventArgs e)
        {
            var imageArray = FilesHelper.ReadFully(file.GetStream());
            file.Dispose();
            var instructor = new Instructor()
            {
                Name = EntName.Text,
                Language = EntLanguage.Text,
                Nationality = EntNationality.Text,
                Gender = PickerGender.Items[PickerGender.SelectedIndex],
                Phone = EntPhone.Text,
                Email = EntEmail.Text,
                Education = EntEducation.Text,
                Experience = PickerExperience.Items[PickerExperience.SelectedIndex],
                CourseDomain = PickerCourceDomain.Items[PickerCourceDomain.SelectedIndex],
                HourlyRate = PickerHourlyRate.Items[PickerHourlyRate.SelectedIndex],
                City = PickerCity.Items[PickerCity.SelectedIndex],
                OneLineTitle = EntOneLineTitle.Text,
                Description = EntDescription.Text,
                ImageArray = imageArray,
            };
            ApiService apiService = new ApiService();
            var response = await apiService.BecomeAnInstructor(instructor);
            if (!response)
            {
                await DisplayAlert("Помилка", "Щось пішло не так", "Повернутись");
            }
            else
            {
                await DisplayAlert("Вітаємо", "Ви стали викладачем", "Повернутись");
            }
        }
    }
}