using System;
using System.Collections.Generic;
using System.Text;
using Poz1.NFCForms.Abstract;
using Xamarin.Forms;

namespace WristCare.ViewModel
{
	public class CourseViewModel
	{
		private readonly INfcForms device;

		public CourseViewModel()
		{
			device = DependencyService.Get<INfcForms>();
			device.NewTag += HandleNewTag;
		}

		void HandleNewTag(object sender, NfcFormsTag e)
		{

		}
	}
}
