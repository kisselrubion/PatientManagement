using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Controllers
{
	[Produces("application/json")]
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly PMDbContext _context;


		public AccountController(PMDbContext context)
		{
			_context = context;

		}

		// api/account?id=r0001
		[HttpGet]
		public ActionResult Get(string id)
		{
			if (!_context.Accounts.Any()) throw new NullReferenceException("Accounts not found");
			var account = _context.Accounts.FirstOrDefault(c => c.AccountNumber == id);
			var user = _context.Users.FirstOrDefault(c => c.UserAccountId == account.AccountNumber);
			return Ok(user);
		}

		//api/account
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] Account account)
		{
			if (account == null)
			{
				throw new NoNullAllowedException("No Account found");
			}

			try
			{
				await _context.Accounts.AddAsync(account);
				return Ok();
			}
			catch
			{
				throw new NullReferenceException("Register Failed");
			}
		}
	}
}