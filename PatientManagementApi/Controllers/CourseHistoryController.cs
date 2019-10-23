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
		private readonly CourseHistoryService _couseHistoryService;

		public CourseHistoryController(PMDbContext context, CourseHistoryService couseHistoryService)
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
		public async Task<ActionResult> Get(int id)
		{
			if (!_context.CourseHistories.Any()) throw new NullReferenceException("Course History not found");
			var courseHistory = await _couseHistoryService.Get(id);
			return Ok(courseHistory);
		}
	}
}
