using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Poz1.NFCForms.Abstract;
using Rg.Plugins.Popup.Services;
using WristCare.Helpers;
using WristCare.Model;
using WristCare.Service.Courses;
using WristCare.Service.PatientServ;
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
		private Course _createdCourse;
		private CourseType _selectedCourseType;
		private string _searchText;
		private ObservableCollection<Course> _courses;

		public Course SelectedCourse
		{
			get => _selectedCourse;
			set => Set(ref _selectedCourse, value);
		}

		public Course CreatedCourse
		{
			get => _createdCourse;
			set => Set(ref _createdCourse, value);
		}

		public CourseType SelectedCourseType
		{
			get => _selectedCourseType;
			set => Set(ref _selectedCourseType, value);
		}

		public string SearchText
		{
			get => _searchText;
			set
			{
				if (Set(ref _searchText, value))
				{
					SearchCourses(SearchText);
					IsBusy = false;
				}
				
			}
		}

		public ObservableCollection<Course> Courses
		{
			get => _courses;
			set => Set(ref _courses, value);
		}

		public List<Course> CoursesList { get; set; }
		public IEnumerable<Course> CoursesIEnumerable { get; set; }
		public ObservableCollection<Course> CoursesFromDb { get; set; }

		public CourseViewModel(CourseService courseService, PatientService patientService)
		{
			IsBusy = false;
			_courseService = courseService;
			_patientService = patientService;
			SelectedCourse = new Course();
			//Todo : remove this sample data
			CreatedCourse = new Course
			{
				Title = "Title",
				IsArchived = false,
				CourseDate = DateTime.Now,
				Description = "Sample description",
			};
			InitializeData();
		}

		public void InitializeData()
		{
			CoursesList = new List<Course>();
			Courses = new ObservableCollection<Course>();
			CoursesFromDb = new ObservableCollection<Course>();
			Task.Run(async () => await GetCoursesAsync());
			IsBusy = false;
		}

		public async Task GetCoursesAsync()
		{
			//IsBusy = true;
			Courses.Clear();
			var courses = await _courseService.GetAllCoursesAsync();
			CoursesList = courses;
			Courses = new ObservableCollection<Course>(CoursesList);
			//foreach (var course in courses)
			//{
			//	Courses.Add(course);
			//}

			//IsBusy = false;
		}
		public async Task GetPatientsAsync()
		{
			//Todo : Priority! Get patients that are enrolled with courses
			var patientsWithCourses = await _patientService.GetPatientsWithCourses();
		}

		public ICommand AddCourseCommand => new RelayCommand(async () => await CoursePopup());
		public ICommand CancelCourseCommand => new RelayCommand(async () => await CoursePopupCancel());
		public ICommand CreateCourseCommand => new RelayCommand(async () => await CreateCourse());

		public ICommand PerformSearch => new Command<string>(query => SearchCourses(query));

		private void SearchCourses(string query)
		{

			if (string.IsNullOrEmpty(query)) Courses = new ObservableCollection<Course>(CoursesList);
			IsBusy = true;
			Courses = new ObservableCollection<Course>(CoursesList.FindAll(c=>c.Title.ToLowerInvariant().Contains(query) || c.TransactionId.ToString().ToLowerInvariant().Contains(query)));
			IsBusy = false;
		}

		private async Task CoursePopupCancel()
		{
			await PopupNavigation.Instance.PopAsync();
		}

		private async Task CreateCourse()
		{
			try
			{
				if (CreatedCourse != null)
				{
					//Todo : add " are you sure you want to create..." popup
					var result = await _courseService.CreateCourse(CreatedCourse);
					if (result.CourseId != 0)
					{
						await PopupHelper.ActionResultMessage("Success", "Course created");
						await GetCoursesAsync();
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		private async Task CoursePopup()
		{
			await PopupNavigation.Instance.PushAsync(new AddCoursePage());
		}

		
	}
}
