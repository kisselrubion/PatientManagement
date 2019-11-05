using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientManagementApi.Services.MeasurementServices;
using PatientManagementBackend.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatientManagementApi.Controllers
{
	[Produces("application/json")]
	[Route("api/measurement")]
	[ApiController]
	public class MonitoringController : Controller
	{
		private readonly PMDbContext _context;

		private readonly MeasurementService _measurementService;

		public MonitoringController(MeasurementService measurementService, PMDbContext context)
		{
			_measurementService = measurementService;
			_context = context;
		}

		// usage: api/measurement/id
		[HttpPost]
		[HttpPost("{id}")]
		public async Task<ActionResult> Post([FromBody] Measurement measurement, int id)
		{
			if (measurement == null) throw new NullReferenceException("measurement not found");
			try
			{
				var entity = await _measurementService.Post(measurement);

				return Ok(entity);
			}
			catch
			{
				throw new NullReferenceException("measurement Registration Failed");
			}
		}

		// usage : api/measurement/c0001
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(string id)
		{
			if (id == "") throw new NullReferenceException("measurement not found");
			if (!_context.Measurements.Any()) throw new NullReferenceException("measurement not found");
			var measurement = await _measurementService.Get(id);
			return Ok(measurement);
		}

		// usage : api/measurement?all=true
		[HttpGet]
		public ActionResult Get(bool all)
		{
			if (!all) throw new NullReferenceException("measurement not found");
			if (!_context.Measurements.Any()) throw new NullReferenceException("Measurements no entries");
			var measurements = _measurementService.GetRange(all);
			return Ok(measurements);
		}
	}

}
