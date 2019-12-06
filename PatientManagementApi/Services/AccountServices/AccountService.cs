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
		public async Task<Account> Post(Account account)
		{
			try
			{
				var addedAccount =  await _context.Accounts.AddAsync(account);
				await _context.SaveChangesAsync();
				return addedAccount.Entity;
			}
			catch
			{
				return new Account();
			}
		}

		public bool Put(Account account)
		{
			try
			{
				_context.Accounts.Update(account);
				_context.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<Account> Get(string id)
		{
			var user = await _context.Accounts.FirstOrDefaultAsync(c => c.AccountNumber == id);
			return user ?? new Account();
		}

		public Task<bool> Remove(string id)
		{
			//todo
			throw new NotImplementedException();
		}
	}
}
