using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCare.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddCoursePage : PopupPage
	{
		public AddCoursePage()
		{
			InitializeComponent();
			BindingContext = App.Locator.CourseViewModel;
		}
	}
}