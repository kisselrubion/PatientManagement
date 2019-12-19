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
			LstDoctors.SelectedItem = null;
		}

		private void LstPatients_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			LstPatients.SelectedItem = null;
		}
	}
}