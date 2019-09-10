using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WristCare.Helpers
{
	public static class PopupHelper
	{
		public static async Task<bool> ProceedMessage(string title, string message)
		{
			var response = await Application.Current.MainPage.DisplayAlert(title, message, "Yes", "Cancel");
			return response;
		}

		public static async Task ActionResultMessage(string title, string message)
		{
			await Application.Current.MainPage.DisplayAlert(title, message, "Ok");
		}
	}
}
