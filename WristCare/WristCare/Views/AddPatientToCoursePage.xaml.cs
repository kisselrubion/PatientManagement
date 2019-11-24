using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPatientToCoursePage 
	{
		public AddPatientToCoursePage()
		{
			InitializeComponent();
		}

		private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			var vm = App.Locator.CourseDetailsViewModel;
			vm.AddSelectedPatientToCourseCommand.Execute(null);
		}
	}
}