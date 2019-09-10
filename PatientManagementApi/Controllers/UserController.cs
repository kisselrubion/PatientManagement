using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientManagementApi.Services.UserServices;
using PatientManagementBackend.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatientManagementApi.Controllers
{
	[Produces("application/json")]
	[Route("api/user")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly PMDbContext _context;
		private readonly UserService _userService;
		public UserController(PMDbContext context, UserService userService)
		{
			_context = context;
			_userService = userService;
		}

		// api/User?r0001
		[HttpGet]
		public async Task<ActionResult> Get(string id)
		{
			if (!_context.Users.Any()) throw new NullReferenceException("Users not found");
			var user = await _userService.Get(id);
			return Ok(user);
		}

		//api/User
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] User user)
		{
			if (user == null) throw new NullReferenceException("User not found");
			var entity =  await _userService.Post(user);
			return Ok(entity);
		}


	}
}
