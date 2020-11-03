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
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void BtnSend_Clicked(object sender, EventArgs e)
        {
            ApiService apiService = new ApiService();
            bool response = await apiService.PasswordRecovery(EntEmail.Text);
            if (!response)
            {
                await DisplayAlert("Помилка", "Щось пішло не так", "Повернутись");
            }
            else
            {
                await DisplayAlert("Вітаємо", "Новий пароль було надіслано на елетронну адресу", "Повернутись");
                await Navigation.PopToRootAsync();
            }

        }
    }
}