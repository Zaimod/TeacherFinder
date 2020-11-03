using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeacherFinder.Pages;
using Xamarin.Essentials;
using Preferences = Xamarin.Essentials.Preferences;
using TeacherFinder.Services;

namespace TeacherFinder
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if(!string.IsNullOrEmpty(Preferences.Get("accesstoken", "")))
                MainPage = new MasterPage();
            else if(string.IsNullOrEmpty(Preferences.Get("useremail", "")) && string.IsNullOrEmpty(Preferences.Get("password", "")))
                MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
