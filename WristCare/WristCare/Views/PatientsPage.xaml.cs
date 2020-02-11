using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WristCare.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCare.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PatientsPage : ContentPage
	{
		public PatientsPage()
		{
			InitializeComponent();
		}

		private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
		}

		private void LstPatients_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			App.NavigationService.NavigateTo(Locator.AddPatientInformationPage);
			App.Locator.PatientsViewModel.IsEnabled1 = false;
		}
	}
}