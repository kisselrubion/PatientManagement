using System;
using System.Collections.Generic;
using System.Text;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using WristCare.ViewModel;
using Xamarin.Forms;

namespace WristCare.Helpers
{
	public class Locator
	{
		public const string HomePage = "HomePage";
		public const string Dashboard = "Dashboard";
		public const string CoursePage = "CoursePage";
		public const string ScanPage = "ScanPage";
		public const string PatientInformationPage = "PatientInformationPage";

		public Locator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
			SimpleIoc.Default.Register<ScanViewModel>();
		}

		public ScanViewModel ScanViewModel => ServiceLocator.Current.GetInstance<ScanViewModel>();

	}

	public static class ViewModelLocator
	{
		public static Locator Locator = (Locator)Application.Current.Resources["Locator"];
	}
}
