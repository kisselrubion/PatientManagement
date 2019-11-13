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

		public ObservableCollection<Medicine> Medicines { get; set; }

		public CourseDetailsViewModel(CourseService courseService,MedicalPlanService medicalPlanService )
		{
			_courseService = courseService;
			_medicalPlanService = medicalPlanService;
			_selectedCourse = new Course();
			InitializeData();

			Task.Run(async () => await GetCourseMedicines());
		}

		private void InitializeData()
		{
			_selectedMedicine = new Medicine
			{
				MedicineName = "medicine name",
				MedicineNumber = 12123,
				Comments = "comments",
				CourseId = _selectedCourse.CourseId,
				Dosage = "2ml",
				Interval = 2,
			};

		}

		public ICommand ShowMedicalPlanCommand => new RelayCommand(ShowMedicalPlanPage);
		public ICommand SelectMedicinePlanCommand => new RelayCommand(ShowMedicineDetailsPage);
		public ICommand AddMedicinePlanCommand => new RelayCommand(async () => await AddMedicalPlanToCourseHistory());

		private async Task GetCourseMedicines()
		{
			var medicines = await _medicalPlanService.GetMedicinePlan(_selectedCourse);
			Medicines = new ObservableCollection<Medicine>(medicines);
		}
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
			_selectedMedicine.CourseId = _selectedCourse.CourseId;
			var medResult = await _medicalPlanService.AddMedicinePlan(_selectedMedicine);
			//var histResult = await _medicalPlanService
			if (medResult != null)
			{ 
				await PopupHelper.ActionResultMessage("Success", "medicine plan added to course");
			}
		}
	}
}
