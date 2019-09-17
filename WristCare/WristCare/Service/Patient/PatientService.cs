using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WristCare.Model;

namespace WristCare.Service.Patient
{
	public class PatientService
	{
		private RequestProvider.RequestProvider _requestProvider;
		
		private string Uri { get; set; }
		public PatientService(RequestProvider.RequestProvider requestProvider)
		{
			_requestProvider = requestProvider;
			Uri = "http://192.168.1.5/patient/api/account";
		}

		/// <summary>
		/// Gets value of all accounts with patient type of ID
		/// </summary>
		/// <returns></returns>
		public async Task<List<User>> GetAllPatientsAsync()
		{
			var patients = await _requestProvider.GetAsync<List<User>>("account/patients");
			return patients;
		}

		public async Task<List<User>> GetPatientsWithCourses()
		{
			var patients = await _requestProvider.GetAsync<List<User>>("account/patientCourses");
			return patients;
		}

		public async Task<Account> RegisterPatient(Account account)
		{
			var response = await _requestProvider.PostAsync("account", account);
			return response;
		}
		
	}
}
