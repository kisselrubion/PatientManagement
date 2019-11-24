using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientManagementApi.Services.AccountServices;
using PatientManagementApi.Services.PatientServices;
using PatientManagementBackend.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatientManagementApi.Controllers
{
	[Produces("application/json")]
	[Route("api/patient")]
	[ApiController]
	public class PatientController : Controller
	{
		private readonly PMDbContext _context;
		private readonly PatientService _patientService;

		public PatientController(PMDbContext context, PatientService patientService)
		{
			_context = context;
			_patientService = patientService;
		}

		// usage: api/patient
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] Patient patient)
		{
			if (patient == null) throw new NullReferenceException("Patient not found");
			try
			{
				var entity = await _patientService.Post(patient);

				return Ok(entity);
			}
			catch
			{
				throw new NullReferenceException("Patient Registration Failed");
			}
		}

		// usage: api/patient
		[HttpPut]
		public ActionResult Put([FromBody] Patient patient)
		{
			if (patient == null) throw new NullReferenceException("Patient not found");
			try
			{
				var entity = _patientService.Put(patient);
				return Ok(entity);
			}
			catch
			{
				throw new NullReferenceException("Patient Registration Failed");
			}
		}

		// usage : api/patient/p0001
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(string id)
		{
			if (id == null) throw new NullReferenceException("Patient not found");
			if (!_context.Patients.Any()) throw new NullReferenceException("Patient not found");
			var patient = await _patientService.Get(id);
			return Ok(patient);
		}

		// usage : api/course?all=true
		[HttpGet]
		public ActionResult Get(bool all)
		{
			if (!all) throw new NullReferenceException("Patient not found");
			if (!_context.Patients.Any()) throw new NullReferenceException("Patient no entries");
			var patient = _patientService.GetRange(all);
			return Ok(patient);
		}
	}
}
