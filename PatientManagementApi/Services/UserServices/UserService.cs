using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.UserServices
{
	public class UserService : IUserService
	{
		private readonly PMDbContext _context;

		public UserService(PMDbContext context)
		{
			_context = context;
		}
		public async Task<bool> Post(User user)
		{
			try
			{
				await _context.Users.AddAsync(user);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Put(User user)
		{
			try
			{
				_context.Users.Update(user);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<User> Get(string id)
		{
			var user = await _context.Users.FirstOrDefaultAsync(c => c.UserAccountId == id);
			return user ?? new User();
		}

		public Task<bool> Remove(string id)
		{
			throw new NotImplementedException();
		}
	}
}
