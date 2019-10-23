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
	public partial class CoursePage : ContentPage
	{
		public CoursePage ()
		{
			InitializeComponent ();
		}

		private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			//todo : navigate to course details page
			throw new NotImplementedException();
		}
	}
}