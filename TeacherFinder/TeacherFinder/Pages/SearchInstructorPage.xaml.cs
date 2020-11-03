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
    public partial class SearchInstructorPage : ContentPage
    {
        public ObservableCollection<Instructor> Instructors;
        public bool First = true;
        private string _course;
        private string _city;
        private string _gender;
        public SearchInstructorPage(string course, string city, string gender)
        {
            InitializeComponent();
            Instructors = new ObservableCollection<Instructor>();
            _course = course;
            _city = city;
            _gender = gender;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (First)
            {
                ApiService apiService = new ApiService();
                var instructors = await apiService.SearchInstructors(_course, _gender, _city);

                foreach (var instructor in instructors)
                {
                    Instructors.Add(instructor);
                }
                LvInstructors.ItemsSource = Instructors;
            }
            First = false;
        }

        private void LvInstructors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedInstructor = e.SelectedItem as Instructor;
            Navigation.PushAsync(new InstructorProfilePage(selectedInstructor.Id));
        }
    }
}