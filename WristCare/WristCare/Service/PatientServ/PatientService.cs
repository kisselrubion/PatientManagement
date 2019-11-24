using System.Collections.Generic;
using System.Threading.Tasks;
using WristCare.Model;

namespace WristCare.Service.PatientServ
{
	public class PatientService
	{
		private RequestProvider.RequestProvider _requestProvider;
		
		public PatientService(RequestProvider.RequestProvider requestProvider)
		{
			_requestProvider = requestProvider;
		}

		/// <summary>
		/// Gets value of all accounts with patient type of ID
		/// </summary>
		/// <returns>
		/// List of patients if theres any but receives empty list of patients to be quantified as null
		/// </returns>
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

			//todo : test this 
			//var newPatient = new Patient
			//{
			//	AccountId = response.AccountId,
			//	PatientNumber = int.Parse(response.AccountNumber)
			//};

			//var result = await _requestProvider.PostAsync("patient", account);

			return response;
		}
		
	}
}
