using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCare.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CourseDetailsPage : ContentPage
	{
		public CourseDetailsPage()
		{
			InitializeComponent();
		}

		private void LstDoctors_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			//LstDoctors.SelectedItem = null;
		}

		private void LstPatients_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			App.Locator.CourseDetailsViewModel.ShowDoctorsCommand.Execute(null);
		}

		private void TapGestureRecognizer2_OnTapped(object sender, EventArgs e)
		{
			App.Locator.CourseDetailsViewModel.ShowPatientsCommand.Execute(null);

		}
	}
}