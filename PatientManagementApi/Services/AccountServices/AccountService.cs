using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.AccountServices
{
	public class AccountService : IAccountService
	{
		private readonly PMDbContext _context;

		public AccountService(PMDbContext context)
		{
			_context = context;
		}
		public async Task<bool> Post(Account account)
		{
			try
			{
				await _context.Accounts.AddAsync(account);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Put(Account account)
		{
			throw new NotImplementedException();

		}

		public async Task<Account> Get(string id)
		{
			var user = await _context.Accounts.FirstOrDefaultAsync(c => c.AccountNumber == id);
			return user ?? new Account();
		}

		public Task<bool> Remove(string id)
		{
			throw new NotImplementedException();
		}
	}
}
