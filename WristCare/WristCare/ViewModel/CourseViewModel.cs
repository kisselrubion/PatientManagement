using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Poz1.NFCForms.Abstract;
using Rg.Plugins.Popup.Services;
using WristCare.Helpers;
using WristCare.Model;
using WristCare.Service.Courses;
using WristCare.Service.Patient;
using WristCare.View;
using WristCare.ViewModel.Base;
using Xamarin.Forms;

namespace WristCare.ViewModel
{
	public class CourseViewModel : BaseViewModel
	{
		private readonly INfcForms device;
		private readonly CourseService _courseService;
		private readonly PatientService _patientService;
		private Course _selectedCourse;

		public Course SelectedCourse
		{
			get => _selectedCourse;
			set => Set(ref _selectedCourse, value);
		}

		public ObservableCollection<Course> Courses { get; set; }
		public ObservableCollection<string> CourseTypes { get; set; }

		public CourseViewModel(CourseService courseService, PatientService patientService)
		{
			_courseService = courseService;
			_patientService = patientService;
			//device = DependencyService.Get<INfcForms>();
			//device.NewTag += HandleNewTag;
			InitializeData();
		}

		public void InitializeData()
		{
			SelectedCourse = new Course
			{
				Title = "Stroke Medication",
				IsArchived = false,
				CourseDate = DateTime.Now,
				Description = "Sample Description",
				//Todo: Create Id creator helper
				TransactionId = Guid.NewGuid().ToString(),
			};
			Courses = new ObservableCollection<Course>();
			CourseTypes = new ObservableCollection<string>
			{
				"Medication",
				"Treatment",
				"Procedure",
			};
			Task.Run(async () => await GetCoursesAsync());
		}

		public async Task GetCoursesAsync()
		{
			var courses = await _courseService.GetAllCoursesAsync();
			foreach (var course in courses)
			{
				Courses.Add(course);
			}
		}
		public async Task GetPatientsAsync()
		{

			//Todo : Priority! Get patients that are enrolled with courses
			var patientsWithCourses = await _patientService.GetPatientsWithCourses();
		}
		public ICommand AddPatientsCommand => new RelayCommand(AddPatients);
		public ICommand SearchPatientCommand => new RelayCommand(SearchPatients);
		public ICommand AddCourseCommand => new RelayCommand(async () => await CoursePopup());
		public ICommand CancelCourseCommand => new RelayCommand(async () => await CoursePopupCancel());
		public ICommand CreateCourseCommand => new RelayCommand(async () => await CreateCourse());

		private async Task CoursePopupCancel()
		{
			await PopupNavigation.Instance.PopAsync();
		}

		private async Task CreateCourse()
		{
			if (SelectedCourse != null)
			{
				var result = await _courseService.CreateCourse(SelectedCourse);
				if (result.CourseId != 0)
				{
					await PopupHelper.ActionResultMessage("Success", "Course created");
				}
			}
		}

		private async Task CoursePopup()
		{
			await PopupNavigation.Instance.PushAsync(new AddCoursePage());
		}

		private void AddPatients()
		{
			navigationService.NavigateTo(Locator.AddPatientInformationPage);

		}
		private void SearchPatients()
		{
			navigationService.NavigateTo(Locator.PatientsPage);
		}
	}
}
