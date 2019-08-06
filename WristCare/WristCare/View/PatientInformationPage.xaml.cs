using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCare.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PatientInformationPage : ContentPage
	{
		public User SelectedUser { get; set; }
		public Account SelectedAccount { get; set; }
		public PatientInformationPage ()
		{
			InitializeComponent ();
		}
	}
}