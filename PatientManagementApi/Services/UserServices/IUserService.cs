using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.UserServices
{
	public interface IUserService
	{
		Task<User> Post(User user);
		bool Put(User user);
		Task<User> Get(string id);
		Task<bool> Remove(string id);
	}
}
