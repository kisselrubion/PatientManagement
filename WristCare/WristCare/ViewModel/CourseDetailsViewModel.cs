using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using WristCare.Helpers;
using WristCare.Model;
using WristCare.Service.Courses;
using WristCare.View;
using WristCare.ViewModel.Base;

namespace WristCare.ViewModel
{
	public class CourseDetailsViewModel : BaseViewModel
	{
		private readonly CourseService _courseService;
		private Course _selectedCourse;
		private CourseType _selectedCourseType;

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

		public CourseDetailsViewModel(CourseService courseService)
		{
			_courseService = courseService;

			CourseTypes = new ObservableCollection<CourseType>
			{
				new CourseType{Key = 1,Value = "Medication"},
				new CourseType{Key = 2,Value = "Procedure"},
				new CourseType{Key = 3,Value = "Monitoring"},
			};
		}

		public ICommand AddMedicalPlanCommand => new RelayCommand(ShowMedicalPlanPage);

		private void ShowMedicalPlanPage()
		{
			navigationService.NavigateTo(Locator.MedicalPlanPage);
		}
	}
}
