using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Poz1.NFCForms.Abstract;
using Rg.Plugins.Popup.Services;
using WristCare.Helpers;
using WristCare.Model;
using WristCare.Service.Courses;
using WristCare.View;
using WristCare.ViewModel.Base;
using Xamarin.Forms;

namespace WristCare.ViewModel
{
	public class CourseViewModel : BaseViewModel
	{
		private readonly INfcForms device;
		private readonly CourseService _courseService;
		private Course _selectedCourse;

		public Course SelectedCourse
		{
			get => _selectedCourse;
			set => Set(ref _selectedCourse, value);
		}

		public CourseViewModel(CourseService courseService)
		{
			_courseService = courseService;
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
				TransactionId = Guid.NewGuid().ToString(),
			};
		}
		public ICommand AddPatientsCommand => new RelayCommand(AddPatients);
		public ICommand SearchPatientCommand => new RelayCommand(SearchPatients);
		public ICommand AddCourseCommand => new RelayCommand(async () => await CoursePopup());
		public ICommand CancelCourseCommand => new RelayCommand(async () => await CoursePopupCancel());

		private async Task CoursePopupCancel()
		{
			await PopupNavigation.Instance.PopAsync();
		}

		public ICommand CreateCourseCommand => new RelayCommand(async () => await CreateCourse());

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
