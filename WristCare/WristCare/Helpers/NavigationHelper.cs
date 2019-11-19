﻿using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using WristCare.Service.Navigation;
using WristCare.View;
using WristCare.Views;
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
			navigationService.Configure(Locator.AddCoursePage, typeof(AddCoursePage));
			navigationService.Configure(Locator.CoursePage, typeof(CoursePage));
			navigationService.Configure(Locator.HomePage, typeof(HomePage));
			navigationService.Configure(Locator.Dashboard, typeof(Dashboard));
			navigationService.Configure(Locator.PatientInformationPage, typeof(PatientInformationPage));
			navigationService.Configure(Locator.PatientsPage, typeof(PatientsPage));
			navigationService.Configure(Locator.AddPatientInformationPage, typeof(AddPatientInformationPage));
			navigationService.Configure(Locator.CourseDetailsPage, typeof(CourseDetailsPage));
			navigationService.Configure(Locator.DoctorsPage, typeof(DoctorPage));
			navigationService.Configure(Locator.NursesPage, typeof(NursePage));
			navigationService.Configure(Locator.MedicalPlanPage, typeof(MedicalPlanPage));
			navigationService.Configure(Locator.AddMedicinePage, typeof(AddMedicinePage));
			navigationService.Configure(Locator.MedicinePlanContainerPage, typeof(MedicinePlanContainerPage));
			navigationService.Configure(Locator.MeasurementDetailsPage, typeof(MeasurementDetailsPage));
			navigationService.Configure(Locator.MedicineDetailsPage, typeof(MedicineDetailsPage));
			navigationService.Configure(Locator.ProcedureDetailsPage, typeof(ProcedureDetailsPage));
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
