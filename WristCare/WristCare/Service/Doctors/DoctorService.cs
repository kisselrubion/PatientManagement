using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WristCare.Model;

namespace WristCare.Service.Doctors
{
	public class DoctorService
	{
		private RequestProvider.RequestProvider _requestProvider;

		public DoctorService(RequestProvider.RequestProvider requestProvider)
		{
			_requestProvider = requestProvider;
		}

		public async Task<Account> RegisterDoctor(Account account)
		{
			var response = await _requestProvider.PostAsync("account", account);

			var newDoc = new Doctor
			{
				AccountId = response.AccountId,
				DoctorNumber = response.AccountNumber,
			};
			var result = await _requestProvider.PostAsync("doctor", newDoc);

			return result != null ? response : new Account();
		}

		public async Task<List<User>> GetUserDoctors()
		{
			var patients = await _requestProvider.GetAsync<List<User>>("account/doctors");
			return patients;
		}

		public async Task<List<Doctor>> GetDoctors()
		{
			var doctors = await _requestProvider.GetAsync<List<Doctor>>("doctor?all=true");
			return doctors;
		}



	}
}
