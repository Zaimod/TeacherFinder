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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            ApiService apiService = new ApiService();
            bool response = await apiService.RegisterUser(EntEmail.Text, EntPassword.Text, EntConfirmPassword.Text);
            if (!response)
            {
                await DisplayAlert("Помилка", "Щось пішло не так", "Повернутись");
            }
            else
            {
                await DisplayAlert("Вітаємо", "Ваш обліковий запис було створено", "Повернутись");
                await Navigation.PopToRootAsync();
            }
        }
    }
}