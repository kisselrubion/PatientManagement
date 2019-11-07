using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WristCare.Helpers;
using WristCare.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCare.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoursePage : ContentPage
	{
		public CoursePage ()
		{
			InitializeComponent ();
		}

		//Used so the ripple animation shows
		private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			App.Locator.CourseDetailsViewModel.SelectedCourse = App.Locator.CourseViewModel.SelectedCourse;
			App.NavigationService.NavigateTo(Locator.CourseDetailsPage);
			LstCourses.SelectedItem = null;
		}
	}
}