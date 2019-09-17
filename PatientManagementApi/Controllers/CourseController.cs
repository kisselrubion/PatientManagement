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

		// usage : api/course?id=c0001
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(string id)
		{
			if (id == null) throw new NullReferenceException("Users not found");
			if (!_context.Users.Any()) throw new NullReferenceException("Users not found");
			var user = await _courseService.Get(id);
			return Ok(user);
		}

		// usage : api/course?all=true
		[HttpGet]
		public ActionResult Get(bool all)
		{
			if (!all) throw new NullReferenceException("Courses not found");
			if (!_context.Courses.Any()) throw new NullReferenceException("Course no entries");
			var courses = _courseService.GetRange(all);
			return Ok(courses);
		}
	}
}
