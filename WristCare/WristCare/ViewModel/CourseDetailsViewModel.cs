using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using WristCare.Helpers;
using WristCare.Model;
using WristCare.Service.Courses;
using WristCare.Service.MedicalPlan;
using WristCare.View;
using WristCare.ViewModel.Base;

namespace WristCare.ViewModel
{
	public class CourseDetailsViewModel : BaseViewModel
	{
		private readonly CourseService _courseService;
		private readonly MedicalPlanService _medicalPlanService;
		private Course _selectedCourse;
		private CourseType _selectedCourseType;
		private Medicine _selectedMedicine;

		public Medicine SelectedMedicine
		{
			get => _selectedMedicine;
			set => Set(ref _selectedMedicine, value);
		}

		public Course SelectedCourse
		{
			get => _selectedCourse;
			set => Set(ref _selectedCourse, value);
		}

		public CourseType SelectedCourseType
		{
			get => _selectedCourseType;
			set => Set(ref _selectedCourseType, value);
		}

		public ObservableCollection<CourseType> CourseTypes { get; set; }

		public CourseDetailsViewModel(CourseService courseService,MedicalPlanService medicalPlanService )
		{
			_courseService = courseService;
			_medicalPlanService = medicalPlanService;
			_selectedCourse = new Course();
			InitializeData();
		}

		private void InitializeData()
		{
			_selectedMedicine = new Medicine
			{
				CourseId = _selectedCourse.CourseId
			};
			CourseTypes = new ObservableCollection<CourseType>
			{
				new CourseType{Key = 1,Value = "Medication"},
				new CourseType{Key = 2,Value = "Procedure"},
				new CourseType{Key = 3,Value = "Monitoring"},
			};
		}

		public ICommand ShowMedicalPlanCommand => new RelayCommand(ShowMedicalPlanPage);
		public ICommand SelectMedicinePlanCommand => new RelayCommand(ShowMedicineDetailsPage);
		public ICommand AddMedicinePlanCommand => new RelayCommand(async () => await AddMedicalPlanToCourseHistory());

		private void ShowMedicineDetailsPage()
		{
			navigationService.NavigateTo(Locator.AddMedicinePage);
		}

		private void ShowMedicalPlanPage()
		{
			navigationService.NavigateTo(Locator.MedicalPlanPage);
		}

		public async Task AddMedicalPlanToCourseHistory()
		{
			var result = await _medicalPlanService.AddMedicinePlan(_selectedMedicine);
			if (result != null)
			{ 
				await PopupHelper.ActionResultMessage("Success", "medicine plan added to course");
			}
		}
	}
}
