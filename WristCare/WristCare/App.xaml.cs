using System;
using WristCare.Helpers;
using WristCare.Service.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WristCare
{
	public partial class App : Application
	{
		public static Locator Locator;
		public static NavigationService NavigationService { get; set; }
		private readonly NavigationHelper _initNavigation;

		public App()
		{
			InitializeComponent();
			if (_initNavigation == null)
			{
				_initNavigation = new NavigationHelper();
			}
			NavigationService = _initNavigation.navigationService;
			_initNavigation.SetPages();
			Locator = new Locator();
			MainPage = _initNavigation.InitializeMainPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
