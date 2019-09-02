using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WristCare.Model;
using WristCare.Service.Patient;
using WristCare.ViewModel.Base;

namespace WristCare.ViewModel
{
	public class PatientsViewModel : BaseViewModel
	{
		private readonly PatientService _patientService;
		public ObservableCollection<User> Patients { get; set; }
		public PatientsViewModel(PatientService patientService)
		{
			_patientService = patientService;
			Patients = new ObservableCollection<User>();
			Task.Run(async () => await GetPatientsAsync());
			IsBusy = false;
		}

		private async Task GetPatientsAsync()
		{
			IsBusy = true;
			var userPatients = await _patientService.GetAllPatientsAsync();
			foreach (var userPatient in userPatients)
			{
				Patients.Add(userPatient);
			}
			IsBusy = false;
		}
	}
}
