using System;
using System.Collections.Generic;
using System.Text;
using WristCare.Model;
using WristCare.ViewModel.Base;

namespace WristCare.ViewModel
{
	public class PatientInformationViewModel : BaseViewModel
	{
		private Course _selectedCourse;
		public Course SelectedCourse
		{
			get => _selectedCourse;
			set => Set(ref _selectedCourse, value);
		}
		public Account Patient { get; set; }
		public PatientInformationViewModel()
		{
			
		}
	}
}
