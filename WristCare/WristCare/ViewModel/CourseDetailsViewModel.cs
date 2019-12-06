﻿using System;
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
			IsBusy = false;
			_selectedCourse = new Course();
			_selectedPatient = new Patient();
			_selectedUser = new User();
			_selectedMedicine = new Medicine();
			SelectedMedicine = new Medicine();
			Medicines = new ObservableCollection<Medicine>();
			AllPatients = new ObservableCollection<User>();
			Patients = new ObservableCollection<User>();
			Task.Run(async () => await GetAllPatients());
		}

		public ICommand ShowMedicalPlanCommand => new RelayCommand(ShowMedicalPlanPage);
		public ICommand ClosePopupCommand => new RelayCommand(async () => await ClosePopUpPage());
		public ICommand ClosePageCommand => new RelayCommand(ClosePage);
		public ICommand SelectMedicinePlanCommand => new RelayCommand(ShowMedicineDetailsPage);
		public ICommand AddMedicinePlanCommand => new RelayCommand(async () => await AddMedicalPlanToCourseHistory());
		public ICommand AddMedicineLogCommand => new RelayCommand(ShowMedicineDetailsPage);
		public ICommand ShowMedicineLogsCommand => new RelayCommand(ShowMedicineLogs);
		public ICommand ShowProcedureLogsCommand => new RelayCommand(ShowProcedureLogs);
		public ICommand ShowMeasurementLogsCommand => new RelayCommand(ShowMeasurementLogs);
		public ICommand ShowPatientsCommand => new RelayCommand(ShowAddPatientToCoursePage);
		public ICommand AddSelectedPatientToCourseCommand => new RelayCommand(async () => await AddPatientToCourse());

		private void ClosePage()
		{
			navigationService.GoBack();
		}
		private async Task ClosePopUpPage()
		{
			await PopupNavigation.Instance.PopAsync();
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


		public void ShowMedicineDetailsPage()
		{
			navigationService.NavigateTo(Locator.AddMedicinePage);
		}

		private void ShowMedicalPlanPage()
		{
			navigationService.NavigateTo(Locator.MedicalPlanPage);
		}
		private async Task AddPatientToCourse()
		{
			IsBusy = true;
			var createdCourseHistory = new CourseHistory
			{
				CourseId = _selectedCourse.CourseId,
				UserAccountNumber = _selectedUser.UserAccountId
			};

			var result = await _medicalPlanService.AddCourseHistory(createdCourseHistory);
			if (result.CourseHistoryId != 0)
			{
				await PopupHelper.ActionResultMessage("Success", "patient enrolled to course");
			}
			//to trigger refresh
			await GetPatientsInCourse(_selectedCourse);
			IsBusy = false;
		}
		private async Task GetCourseMedicines(Course course)
		{
			Medicines.Clear();
			var medicines = await _medicalPlanService.GetMedicinePlan(course);
			if (medicines.Count != 0)
			{
				foreach (var medicine in medicines)
				{
					Medicines.Add(medicine);
				}
				IsEnabled1 = true;
			}
			else
			{
				IsEnabled1 = false;
			}

		}
		private async Task AddMedicalPlanToCourseHistory()
		{
			_selectedMedicine.CourseId = _selectedCourse.CourseId;
			var medResult = await _medicalPlanService.AddMedicinePlan(_selectedMedicine);
			if (medResult != null)
			{
				await PopupHelper.ActionResultMessage("Success", "medicine plan added to course");
			}
			IsBusy = true;
			//to trigger refresh
			await GetCourseMedicines(_selectedCourse);
			IsBusy = false;
		}

		//Gets patients that are enrolled in selected course
		private async Task GetPatientsInCourse(Course selectedCourse)
		{
			Patients.Clear();
			var courseHistory = await _medicalPlanService.GetCourseHistory(selectedCourse);
			var patient = AllPatients.FirstOrDefault(c => c.UserAccountId == courseHistory.Patient.PatientNumber);
			Patients.Add(patient);
		}

		private async Task GetAllPatients()
		{
			Patients.Clear();
			var patients = await _patientService.GetAllPatientsAsync();
			if (patients.Count != 0)
			{
				foreach (var patient in patients)
				{
					AllPatients.Add(patient);
				}
			}

		}
	}
}
