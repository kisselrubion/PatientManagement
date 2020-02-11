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
	public partial class AddDoctorToCoursePage 
	{
		public AddDoctorToCoursePage()
		{
			InitializeComponent();
		}

		private void LstDoctors_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			var vm = App.Locator.CourseDetailsViewModel;
			vm.AddSelectedDoctorToCourseCommand.Execute(null);
		}
	}
}