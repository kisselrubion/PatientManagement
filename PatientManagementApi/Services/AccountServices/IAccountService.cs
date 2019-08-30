using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.AccountServices
{
	public interface IAccountService
	{
		Task<bool> Post(Account account);
		bool Put(Account account);
		Task<Account> Get(string id);
		Task<bool> Remove(string id);
	}
}
