using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherFinder.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeacherFinder.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private async void BtnChangePassword_Clicked(object sender, EventArgs e)
        {
            ApiService apiService = new ApiService();
            bool response = await apiService.ChangePassword(EntOldPassword.Text, EntNewPassword.Text, EntConfirmPassword.Text);
            if (!response)
            {
                await DisplayAlert("Помилка", "Щось пішло не так", "Повернутись");
            }
            else
            {
                await DisplayAlert("Вітаємо", "Ви змінили пароль", "Повернутись");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}