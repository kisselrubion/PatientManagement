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
	public partial class DoctorPage : ContentPage
	{
		public DoctorPage()
		{
			InitializeComponent();
		}

		private void LstDoctors_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			App.NavigationService.NavigateTo(Locator.AddDoctorInformationPage);
			App.Locator.DoctorsViewModel.IsEnabled1 = false;
			LstDoctors.SelectedItem = 0;
		}
	}
}