using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientManagementApi.Services.CourseHistoryService;
using PatientManagementBackend.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatientManagementApi.Controllers
{
	[Produces("application/json")]
	[Route("api/coursehistory")]
	[ApiController]
	public class CourseHistoryController : ControllerBase
	{
		private readonly PMDbContext _context;
		private readonly CouseHistoryService _couseHistoryService;

		public CourseHistoryController(PMDbContext context, CouseHistoryService couseHistoryService)
		{
			_context = context;
			_couseHistoryService = couseHistoryService;
		}

		// usage: api/coursehistory
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CourseHistory courseHistory)
		{
			if (courseHistory == null) throw new NullReferenceException("Course History not found");
			try
			{
				var entity = await _couseHistoryService.Post(courseHistory);
				return Ok(entity);
			}
			catch
			{
				throw new NullReferenceException("Course History Registration Failed");
			}
		}

		// usage : api/coursehistory?id=c0001
		[HttpGet]
		public async Task<ActionResult> Get(string id)
		{
			if (!_context.Users.Any()) throw new NullReferenceException("Users not found");
			var user = await _couseHistoryService.Get(id);
			return Ok(user);
		}
	}
}
