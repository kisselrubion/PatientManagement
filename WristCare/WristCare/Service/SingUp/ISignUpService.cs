using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WristCare.Model;

namespace WristCare.Service.SingUp
{
	public interface ISignUpService
	{
		Task<bool> SignUpAsync(Account account);
	}
}
