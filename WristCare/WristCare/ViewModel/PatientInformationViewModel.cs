using System;
using System.Collections.Generic;
using System.Text;
using WristCare.Model;
using WristCare.ViewModel.Base;

namespace WristCare.ViewModel
{
	public class PatientInformationViewModel : BaseViewModel
	{
		public Account Patient { get; set; }
		public PatientInformationViewModel()
		{
			
		}
	}
}
