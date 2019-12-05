﻿using System;
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

		public ObservableCollection<Course> Courses { get; set; }

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
			Courses = new ObservableCollection<Course>();
			Task.Run(async () => await GetCoursesAsync());
			IsBusy = false;
		}

		public async Task GetCoursesAsync()
		{
			IsBusy = true;
			Courses.Clear();
			var courses = await _courseService.GetAllCoursesAsync();
			if (courses.Count != 0)
			{
				foreach (var course in courses)
				{
					Courses.Add(course);
				}
			}
			IsBusy = false;
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

		private void AddPatients()
		{
			try
			{
				navigationService.NavigateTo(Locator.AddPatientInformationPage);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		private void SearchPatients()
		{
			try
			{
				navigationService.NavigateTo(Locator.PatientsPage);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}
