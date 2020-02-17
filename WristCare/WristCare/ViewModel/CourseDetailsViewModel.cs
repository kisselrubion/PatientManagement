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
using WristCare.Service.Doctors;
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
		private readonly DoctorService _doctorService;
		private readonly PatientService _patientService;
		private readonly MedicalPlanService _medicalPlanService;
		private Course _selectedCourse;
		private CourseType _selectedCourseType;
		private Medicine _selectedMedicine;
		private Medicine _mockSelectedMedicine;
		private User _selectedUser;
		private Patient _selectedPatient;
		private Doctor _selectedDoctor;

		public Medicine SelectedMedicine
		{
			get => _selectedMedicine;
			set => Set(ref _selectedMedicine, value);
		}
		public Medicine MockSelectedMedicine
		{
			get => _mockSelectedMedicine;
			set => Set(ref _mockSelectedMedicine, value);
		}
		public User SelectedUser
		{
			get => _selectedUser;
			set => Set(ref _selectedUser, value);
		}

		public Doctor SelectedDoctor
		{
			get => _selectedDoctor;
			set => Set(ref _selectedDoctor, value);
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
				Task.Run(async () => await GetDoctorsInCourse(_selectedCourse));
			}

		}
		public CourseType SelectedCourseType
		{
			get => _selectedCourseType;
			set => Set(ref _selectedCourseType, value);
		}
		public ObservableCollection<Medicine> Medicines { get; set; }
		public ObservableCollection<User> UserDoctors { get; set; }
		public ObservableCollection<Doctor> Doctors { get; set; }
		public ObservableCollection<User> AllUserDoctors { get; set; }
		public ObservableCollection<User> AllPatients { get; set; }
		public ObservableCollection<User> Patients { get; set; }
		public CourseDetailsViewModel(CourseService courseService, MedicalPlanService medicalPlanService, PatientService patientService, DoctorService doctorService)
		{
			_courseService = courseService;
			_medicalPlanService = medicalPlanService;
			_patientService = patientService;
			_doctorService = doctorService;
			InitializeData();
		}

		private void InitializeData()
		{
			IsBusy = false;
			_selectedCourse = new Course();
			_selectedPatient = new Patient();
			_selectedUser = new User();

			_mockSelectedMedicine = new Medicine
			{
				Date = DateTime.Now,
			};

			_selectedMedicine = new Medicine();

			Medicines = new ObservableCollection<Medicine>();
			AllPatients = new ObservableCollection<User>();
			AllUserDoctors = new ObservableCollection<User>();
			UserDoctors = new ObservableCollection<User>();
			Patients = new ObservableCollection<User>();
			Doctors = new ObservableCollection<Doctor>();
			Task.Run(async () => await GetAllPatients());
			Task.Run(async () => await GetAllUserDoctors());
			Task.Run(async () => await GetDoctors());
		}

		public ICommand ShowMedicalPlanCommand => new RelayCommand(ShowMedicalPlanPage);
		public ICommand ClosePopupCommand => new RelayCommand(async () => await ClosePopUpPage());
		public ICommand ClosePageCommand => new RelayCommand(ClosePage);
		public ICommand SelectMedicinePlanCommand => new RelayCommand(ShowMedicineDetailsPage);
		public ICommand AddMedicinePlanCommand => new RelayCommand(async () => await AddMedicalPlanToCourseHistory());
		public ICommand AddMedicineLogCommand => new RelayCommand(AddMedicineLog);
		public ICommand ShowMedicineLogsCommand => new RelayCommand(ShowMedicineLogs);
		public ICommand ShowProcedureLogsCommand => new RelayCommand(ShowProcedureLogs);
		public ICommand ShowMeasurementLogsCommand => new RelayCommand(ShowMeasurementLogs);
		public ICommand ShowPatientsCommand => new RelayCommand(async () => await ShowAddPatientToCoursePage());
		public ICommand ShowDoctorsCommand => new RelayCommand(async () => await ShowAddDoctorToCoursePage());
		public ICommand AddSelectedPatientToCourseCommand => new RelayCommand(async () => await AddPatientToCourse());
		public ICommand AddSelectedDoctorToCourseCommand => new RelayCommand(async () => await AddDoctorToCourse());
		public ICommand DeleteSelectedPatientCommand => new RelayCommand<User>( async param => await DeleteSelectedPatient(param));
		public ICommand DeleteSelectedDoctorCommand => new RelayCommand<User>( async param => await DeleteSelectedDoctor(param));
		public ICommand ArchiveCourseCommand => new RelayCommand(async () => await ArchiveCourse());
		public ICommand ShowAddedPatientsPageCommand => new RelayCommand(async () => await ShowAddedPatientsPage());
		public ICommand ShowAddedDoctorsPageCommand => new RelayCommand(async () => await ShowAddedDoctosPage());

		private async Task ShowAddedPatientsPage()
		{
			await PopupNavigation.Instance.PushAsync(new AddedPatientsPage());

		}
		private async Task ShowAddedDoctosPage()
		{
			await PopupNavigation.Instance.PushAsync(new AddedDoctorsPage());
		}


		private async Task ArchiveCourse()
		{
			var action = await PopupHelper.ProceedMessage("Warning", "Remove Course?");
			if (action)
			{
				SelectedCourse.IsArchived = true;
				var response = await _courseService.ArchiveCourse(SelectedCourse);
				if (response)
				{
					await PopupHelper.ActionResultMessage("Success", "Course archived");
					navigationService.GoBack();
					await App.Locator.CourseViewModel.GetCoursesAsync();
				}
			}
			
		}
		private async Task DeleteSelectedDoctor(User doctor)
		{
			var response = await PopupHelper.ProceedMessage("Warning", "Remove doctor?");
			if (response)
			{
				var courseHist = await _medicalPlanService.GetCourseHistory(SelectedCourse);
				courseHist.DoctorId = null;
				var update = await _medicalPlanService.UpdateCourseHistory(courseHist);
				if (update != null)
				{
					await PopupHelper.ActionResultMessage("Success", "Data Updated");

				}
			}
		}

		private async Task DeleteSelectedPatient(User patient)
		{
			var response = await PopupHelper.ProceedMessage("Warning", "Remove Patient?");
			if (response)
			{
				var courseHist = await _medicalPlanService.GetCourseHistory(SelectedCourse);
				courseHist.PatientId = null;
				courseHist.Patient = null;
				var update = await _medicalPlanService.UpdateCourseHistory(courseHist);
				if (update != null)
				{
					await PopupHelper.ActionResultMessage("Success", "Data Updated");

				}
			}
		}


		private async Task ShowAddDoctorToCoursePage()
		{
			await PopupNavigation.Instance.PushAsync(new AddDoctorToCoursePage());
		}

		private void ClosePage()
		{
			navigationService.GoBack();
		}
		private async Task ClosePopUpPage()
		{
			await PopupNavigation.Instance.PopAsync();
		}

		private async Task ShowAddPatientToCoursePage()
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
			//_mockSelectedMedicine = _selectedMedicine;
			navigationService.NavigateTo(Locator.AddMedicinePage);
			IsEnabled1 = false;
			//SelectedMedicine = new Medicine { Date = DateTime.Now };
			//_mockSelectedMedicine = null;
		}



		private void AddMedicineLog()
		{
			SelectedMedicine = new Medicine
			{
				Date = DateTime.Now
			};
			navigationService.NavigateTo(Locator.AddMedicinePage);
			IsEnabled1 = true;
		}

		private void ShowMedicalPlanPage()
		{
			navigationService.NavigateTo(Locator.MedicalPlanPage);
		}
		private async Task AddPatientToCourse()
		{
			IsBusy = true;

			var courseHistory = await _medicalPlanService.GetCourseHistory(_selectedCourse);
			if (courseHistory != null)
			{
				courseHistory.UserAccountNumber = _selectedUser.UserAccountId;
				var result = await _medicalPlanService.UpdateCourseHistory(courseHistory);
				if (result.CourseHistoryId != 0)
				{
					await PopupHelper.ActionResultMessage("Success", "patient enrolled to course");
				}
			}
			//to trigger refresh
			await GetPatientsInCourse(_selectedCourse);
			IsBusy = false;
		}

		private async Task AddDoctorToCourse()
		{
			//todo : enrollment of doctor to in its final stages
			IsBusy = true;

			var courseHistory = await _medicalPlanService.GetCourseHistory(_selectedCourse);
			if (courseHistory != null)
			{
				var doctor = Doctors.FirstOrDefault(c => c.DoctorNumber == _selectedUser.UserAccountId);
				if (doctor != null)
				{
					courseHistory.Doctor = doctor;
					courseHistory.DoctorId = doctor.DoctorId;
				}
				var result = await _medicalPlanService.UpdateCourseHistory(courseHistory);
				if (result.CourseHistoryId != 0)
				{
					await PopupHelper.ActionResultMessage("Success", "Doctor enrolled to course");
				}
			}
			await GetDoctorsInCourse(_selectedCourse);
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
				IsEnabled2 = true;
			}
			else
			{
				IsEnabled2 = false;
			}

		}
		private async Task AddMedicalPlanToCourseHistory()
		{
			_selectedMedicine.CourseId = _selectedCourse.CourseId;
			_selectedMedicine.Date = DateTime.Now;
			var medResult = await _medicalPlanService.AddMedicinePlan(_selectedMedicine);
			if (medResult != null)
			{
				await PopupHelper.ActionResultMessage("Success", "medicine plan added to course");
			}
			IsBusy = true;
			//to trigger refresh
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

		private async Task GetDoctorsInCourse(Course selectedCourse)
		{
			UserDoctors.Clear();
			var courseHistory = await _medicalPlanService.GetCourseHistory(selectedCourse);
			var doctor = AllUserDoctors.FirstOrDefault(c => c.UserAccountId == courseHistory.Doctor.DoctorNumber);
			UserDoctors.Add(doctor);
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

		private async Task GetAllUserDoctors()
		{
			AllUserDoctors.Clear();
			var doctors = await _doctorService.GetUserDoctors();
			if (doctors.Count != 0)
			{
				foreach (var doctor in doctors)
				{
					AllUserDoctors.Add(doctor);
				}
			}
		}

		private async Task GetDoctors()
		{
			Doctors.Clear();
			var doctors = await _doctorService.GetDoctors();
			if (doctors.Count != 0)
			{
				foreach (var doctor in doctors)
				{
					Doctors.Add(doctor);
				}
			}
		}
	}
}
