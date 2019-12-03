using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using WristCare.Helpers;
using WristCare.Model;
using WristCare.Service.Courses;
using WristCare.Service.MedicalPlan;
using WristCare.Service.PatientServ;
using WristCare.View;
using WristCare.ViewModel.Base;
using WristCare.Views;
using Xamarin.Forms;

namespace WristCare.ViewModel
{
	public class CourseDetailsViewModel : BaseViewModel
	{
		private readonly CourseService _courseService;
		private readonly PatientService _patientService;
		private readonly MedicalPlanService _medicalPlanService;
		private Course _selectedCourse;
		private CourseType _selectedCourseType;
		private Medicine _selectedMedicine;
		private User _selectedUser;
		private Patient _selectedPatient;

		public Medicine SelectedMedicine
		{
			get => _selectedMedicine;
			set => Set(ref _selectedMedicine, value);
		}
		public User SelectedUser
		{
			get => _selectedUser;
			set => Set(ref _selectedUser, value);
		}

		public Patient SelectedPatient
		{
			get => _selectedPatient;
			set => Set(ref _selectedPatient, value);
		}

		public Course SelectedCourse
		{
			get => _selectedCourse;
			set
			{
				if (!Set(ref _selectedCourse, value)) return;
				Task.Run(async () => await GetCourseMedicines(_selectedCourse));
				Task.Run(async () => await GetPatientsInCourse(_selectedCourse));
			}

		}
		public CourseType SelectedCourseType
		{
			get => _selectedCourseType;
			set => Set(ref _selectedCourseType, value);
		}
		public ObservableCollection<Medicine> Medicines { get; set; }
		public ObservableCollection<User> AllPatients { get; set; }
		public ObservableCollection<User> Patients { get; set; }
		public CourseDetailsViewModel(CourseService courseService, MedicalPlanService medicalPlanService, PatientService patientService)
		{
			_courseService = courseService;
			_medicalPlanService = medicalPlanService;
			_patientService = patientService;
			InitializeData();
		}

		private void InitializeData()
		{
			_selectedCourse = new Course();
			_selectedPatient = new Patient();
			_selectedUser = new User();
			//Todo : remove this sample data
			_selectedMedicine = new Medicine
			{
				MedicineName = "medicine name",
				MedicineNumber = 12123,
				Comments = "comments",
				CourseId = _selectedCourse.CourseId,
				Dosage = "2ml",
				Interval = DateTime.Now.Hour,
			};
			Medicines = new ObservableCollection<Medicine>();
			AllPatients = new ObservableCollection<User>();
			Patients = new ObservableCollection<User>();
			Task.Run(async () => await GetAllPatients());
		}

		public ICommand ShowMedicalPlanCommand => new RelayCommand(ShowMedicalPlanPage);
		public ICommand SelectMedicinePlanCommand => new RelayCommand(ShowMedicineDetailsPage);
		public ICommand AddMedicinePlanCommand => new RelayCommand(async () => await AddMedicalPlanToCourseHistory());
		public ICommand AddMedicineLogCommand => new RelayCommand(async () => await AddMedicalPlanToCourseHistory());
		public ICommand ShowMedicineLogsCommand => new RelayCommand(ShowMedicineLogs);
		public ICommand ShowProcedureLogsCommand => new RelayCommand(ShowProcedureLogs);
		public ICommand ShowMeasurementLogsCommand => new RelayCommand(ShowMeasurementLogs);
		public ICommand ShowPatientsCommand => new RelayCommand(ShowAddPatientToCoursePage);
		public ICommand AddSelectedPatientToCourseCommand => new RelayCommand(async () => await AddPatientToCourse());

		private async Task AddPatientToCourse()
		{
			var createdCourseHistory = new CourseHistory
			{
				CourseId = _selectedCourse.CourseId,
				UserAccountNumber = _selectedUser.UserAccountId
				//patientId is only used to store the selectedPatient.userAccountId temporarily
			};

			var result = await _medicalPlanService.AddCourseHistory(createdCourseHistory);
			if (result.CourseHistoryId != 0)
			{
				await PopupHelper.ActionResultMessage("Success", "patient enrolled to course");
			}
		}

		private async void ShowAddPatientToCoursePage()
		{
			await PopupNavigation.Instance.PushAsync(new AddPatientToCoursePage());
		}


		private void ShowProcedureLogs()
		{
			navigationService.NavigateTo(Locator.ProcedureDetailsPage);
		}

		private void ShowMeasurementLogs()
		{
			navigationService.NavigateTo(Locator.MeasurementDetailsPage);
		}

		private void ShowMedicineLogs()
		{
			navigationService.NavigateTo(Locator.MedicineDetailsPage);
		}

		private async Task GetCourseMedicines(Course course)
		{
			var medicines = await _medicalPlanService.GetMedicinePlan(course);
			foreach (var medicine in medicines)
			{
				Medicines.Add(medicine);
			}
		}
		private void ShowMedicineDetailsPage()
		{
			navigationService.NavigateTo(Locator.AddMedicinePage);
		}

		private void ShowMedicalPlanPage()
		{
			navigationService.NavigateTo(Locator.MedicalPlanPage);
		}

		private async Task AddMedicalPlanToCourseHistory()
		{
			_selectedMedicine.CourseId = _selectedCourse.CourseId;
			var medResult = await _medicalPlanService.AddMedicinePlan(_selectedMedicine);
			if (medResult != null)
			{
				await PopupHelper.ActionResultMessage("Success", "medicine plan added to course");
			}
		}

		//Gets patients that are enrolled in selected course
		private async Task GetPatientsInCourse(Course selectedCourse)
		{
			IsBusy = true;
			var courseHistory = await _medicalPlanService.GetCourseHistory(selectedCourse);
			var patient = AllPatients.FirstOrDefault(c => c.UserAccountId == courseHistory.Patient.PatientNumber);
			Patients.Add(patient);
			IsBusy = false;
		}

		private async Task GetAllPatients()
		{
			IsBusy = true;
			var patients = await _patientService.GetAllPatientsAsync();
			foreach (var patient in patients)
			{
				AllPatients.Add(patient);
			}
			IsBusy = false;
		}
	}
}
