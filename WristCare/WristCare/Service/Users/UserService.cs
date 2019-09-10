using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WristCare.Model;

namespace WristCare.Service.Users
{
	public class UserService
	{
		private RequestProvider.RequestProvider _requestProvider;

		public UserService(RequestProvider.RequestProvider requestProvider)
		{
			_requestProvider = requestProvider;
		}

		public async Task<User> RegisterUser(User user)
		{
			var response = await _requestProvider.PostAsync("user", user);
			return response;
		}
	}
}
