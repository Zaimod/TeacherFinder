using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherFinder.Models;
using TeacherFinder.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeacherFinder.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<Instructor> Instructors;
        public bool First = true;
        public HomePage()
        {
            InitializeComponent();
            Instructors = new ObservableCollection<Instructor>();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (First)
            {
                ApiService apiService = new ApiService();
                var instructors = await apiService.GetInstructors();

                foreach (var instructor in instructors)
                {
                    Instructors.Add(instructor);
                }
                LvInstructors.ItemsSource = Instructors;
                BusyIndicator.IsRunning = false;
            }
            First = false;
        }

        private void LvInstructors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedInstructor = e.SelectedItem as Instructor; 
            if(selectedInstructor != null)
            {
                Navigation.PushAsync(new InstructorProfilePage(selectedInstructor.Id));
            }
            ((ListView)sender).SelectedItem = null;
        }

        private void TbSearch_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FindInstructorPage());
        }
    }
}