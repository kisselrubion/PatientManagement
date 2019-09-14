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

		public CourseViewModel(CourseService courseService)
		{
			_courseService = courseService;
			//device = DependencyService.Get<INfcForms>();
			//device.NewTag += HandleNewTag;
		}

		void HandleNewTag(object sender, NfcFormsTag e)
		{

		}

		public ICommand AddPatientsCommand => new RelayCommand(AddPatients);
		public ICommand SearchPatientCommand => new RelayCommand(SearchPatients);
		public ICommand AddCourseCommand => new RelayCommand(async () => await CreateCourse());

		private async Task CreateCourse()
		{
			await PopupNavigation.Instance.PushAsync(new AddCoursePage());

			//var course = new Course
			//{
			//	Title = "Stroke Medication",
			//	IsArchived = false,
			//	CourseDate = DateTime.Now,
			//	Description = "Sample Description",
			//	TransactionId = Guid.NewGuid().ToString(),
			//};

			//var result = await _courseService.CreateCourse(course);

			//if (result.CourseId != 0)
			//{
			//	await PopupHelper.ActionResultMessage("Success", "Course created");
			//}
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
