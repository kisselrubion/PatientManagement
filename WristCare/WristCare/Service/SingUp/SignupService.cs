using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WristCare.Model;

namespace WristCare.Service.SingUp
{
	public class SignupService : ISignUpService
	{
		private readonly ISignUpService _signUpService;
		public SignupService(ISignUpService signUpService)
		{
			_signUpService = signUpService;
		}
		public Task<bool> SignUpAsync(Account account)
		{
			throw new NotImplementedException();
		}
	}
}
