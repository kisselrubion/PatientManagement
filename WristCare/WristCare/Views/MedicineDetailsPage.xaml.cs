using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MedicineDetailsPage : ContentPage
	{
		public MedicineDetailsPage()
		{
			InitializeComponent();
		}

		private void LstMeds_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			var vm = App.Locator.CourseDetailsViewModel;
			App.Locator.CourseDetailsViewModel.ShowExistingMedicineDetailsPage();
			App.Locator.CourseDetailsViewModel.IsEnabled1 = false;
			LstMeds.SelectedItem = 0;
		}
	}
}