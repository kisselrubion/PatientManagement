using System;
using System.Collections.Generic;
using System.Text;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using WristCare.Service.Courses;
using WristCare.Service.Doctors;
using WristCare.Service.MedicalPlan;
using WristCare.Service.PatientServ;
using WristCare.Service.Scanners;
using WristCare.Service.SingUp;
using WristCare.Service.Users;
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
		public const string PatientsPage = "PatientsPage";
		public const string AddPatientInformationPage = "AddPatientInformationPage";
		public const string AddCoursePage = "AddCoursePage";
		public const string CourseDetailsPage = "CourseDetailsPage";
		public const string DoctorsPage = "DoctorsPage";
		public const string NursesPage = "NursesPage";
		public const string MedicalPlanPage = "MedicalPlanPage";
		public const string AddMedicinePage = "AddMedicinePage";
		public const string MedicinePlanContainerPage = "MedicinePlanContainerPage";
		public const string MeasurementDetailsPage = "MeasurementDetailsPage";
		public const string MedicineDetailsPage = "MedicineDetailsPage";
		public const string ProcedureDetailsPage = "ProcedureDetailsPage";
		public const string AddPatientToCoursePage = "AddPatientToCoursePage";
		public const string AddDoctorToCoursePage = "AddDoctorToCoursePage";
		public const string BleDevicesPage = "BleDevicesPage";
		public const string StaffPage = "StaffPage";
		public const string AddDoctorInformationPage = "AddDoctorInformationPage";
		public const string HealthPage = "HealthPage";
		public const string AddedPatientsPage = "AddedPatientsPage";
		public const string AddedDoctorsPage = "AddedDoctorsPage";
		public const string AddRfidToPatientPage = "AddRfidToPatientPage";
		public const string ScannedPatientProfilePage = "ScannedPatientProfilePage";

		public Locator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			//TODO: always inject ALL services here

			//Services
			SimpleIoc.Default.Register<RequestProvider.RequestProvider>();
			SimpleIoc.Default.Register<PatientService>();
			SimpleIoc.Default.Register<UserService>();
			SimpleIoc.Default.Register<CourseService>();
			SimpleIoc.Default.Register<MedicalPlanService>();
			SimpleIoc.Default.Register<RfidScannerService>();
			SimpleIoc.Default.Register<DoctorService>();
			SimpleIoc.Default.Register<ISignUpService, SignupService>();

			//View Models
			SimpleIoc.Default.Register<ScanViewModel>();
			SimpleIoc.Default.Register<CourseViewModel>();
			SimpleIoc.Default.Register<PatientsViewModel>();
			SimpleIoc.Default.Register<MedicalPlanViewModel>();
			SimpleIoc.Default.Register<CourseDetailsViewModel>();
			SimpleIoc.Default.Register<DoctorsViewModel>();

		}

		public ScanViewModel ScanViewModel => ServiceLocator.Current.GetInstance<ScanViewModel>();
		public PatientsViewModel PatientsViewModel => ServiceLocator.Current.GetInstance<PatientsViewModel>();
		public CourseViewModel CourseViewModel => ServiceLocator.Current.GetInstance<CourseViewModel>();
		public CourseDetailsViewModel CourseDetailsViewModel => ServiceLocator.Current.GetInstance<CourseDetailsViewModel>();
		public MedicalPlanViewModel MedicalPlanViewModel => ServiceLocator.Current.GetInstance<MedicalPlanViewModel>();
		public DoctorsViewModel DoctorsViewModel => ServiceLocator.Current.GetInstance<DoctorsViewModel>();

	}

	public static class ViewModelLocator
	{
		public static Locator Locator = (Locator)Application.Current.Resources["Locator"];
	}
}
