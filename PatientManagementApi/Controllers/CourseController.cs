using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientManagementApi.Services.CourseHistoryService;
using PatientManagementApi.Services.CourseServices;
using PatientManagementApi.Services.MeasurementServices;
using PatientManagementApi.Services.MedicineServices;
using PatientManagementApi.Services.ProcedureServices;
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
		private readonly MedicineService _medicineService;
		private readonly ProcedureService _procedureService;
		private readonly MeasurementService _measurementService;
		private readonly CourseHistoryService _courseHistoryService;

		public CourseController(
			PMDbContext context, 
			CourseService courseService, 
			MedicineService medicineService, 
			ProcedureService procedureService, 
			MeasurementService measurementService,
			CourseHistoryService courseHistoryService)
		{
			_context = context;
			_courseService = courseService;
			_medicineService = medicineService;
			_procedureService = procedureService;
			_measurementService = measurementService;
			_courseHistoryService = courseHistoryService;
		}

		// usage: api/course/id
		[HttpPost]
		[HttpPost("{id}")]
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

		// usage : api/course/c0001
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(int id)
		{
			if (id == 0) throw new NullReferenceException("Course not found");
			if (!_context.Courses.Any()) throw new NullReferenceException("Course not found");
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
