﻿using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using WristCare.Service.Navigation;
using WristCare.View;
using Xamarin.Forms;

namespace WristCare.Helpers
{
    public class NavigationHelper
    {
	    public NavigationService navigationService { get; }
		public NavigationHelper()
	    {
			navigationService = new NavigationService();
			SimpleIoc.Default.Reset();
		    SimpleIoc.Default.Register<INavigationService>(() => navigationService);
		}

	    public void SetPages()
	    {
		    navigationService.Configure(Locator.CoursePage, typeof(CoursePage));
		    navigationService.Configure(Locator.HomePage, typeof(HomePage));
		    navigationService.Configure(Locator.Dashboard, typeof(Dashboard));
		    navigationService.Configure(Locator.PatientInformationPage, typeof(PatientInformationPage));
		}

	    public Page InitializeMainPage()
	    {
		    var navigationPage = new NavigationPage();
		    navigationPage = new NavigationPage(new HomePage());
		    navigationService.Initialize(navigationPage);
		    return navigationPage;
	    }
	}
}
