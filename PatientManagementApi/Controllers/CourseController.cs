using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientManagementApi.Services.CourseServices;
using PatientManagementBackend.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatientManagementApi.Controllers
{
	[Produces("application/json")]
	[Route("api/course")]
	[ApiController]
	public class CourseController : ControllerBase
	{
		private readonly PMDbContext _context;
		private readonly CourseService _courseService;

		public CourseController(PMDbContext context, CourseService courseService)
		{
			_context = context;
			_courseService = courseService;
		}

		// usage: api/course
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] Course course)
		{
			if (course == null) throw new NullReferenceException("Course not found");
			try
			{
				var entity = await _courseService.Post(course);
				return Ok(entity);
			}
			catch 
			{
				throw new NullReferenceException("Course Registration Failed");
			}
		}

		// usage : api/course?id=r0001
		[HttpGet]
		public async Task<ActionResult> Get(string id)
		{
			if (!_context.Users.Any()) throw new NullReferenceException("Users not found");
			var user = await _courseService.Get(id);
			return Ok(user);
		}
	}
}
