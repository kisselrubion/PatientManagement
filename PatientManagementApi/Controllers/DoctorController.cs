using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientManagementApi.Services.DoctorServices;
using PatientManagementApi.Services.PatientServices;
using PatientManagementBackend.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatientManagementApi.Controllers
{
	[Produces("application/json")]
	[Route("api/doctor")]
	[ApiController]
	public class DoctorController : Controller
	{
		private readonly PMDbContext _context;
		private readonly DoctorService _doctorService;

		public DoctorController(PMDbContext context, DoctorService doctorService)
		{
			_context = context;
			_doctorService = doctorService;
		}

		// usage: api/doctor
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] Doctor doctor)
		{
			if (doctor == null) throw new NullReferenceException("doctor not found");
			try
			{
				var entity = await _doctorService.Post(doctor);

				return Ok(entity);
			}
			catch
			{
				throw new NullReferenceException("Doctor Registration Failed");
			}
		}

		// usage: api/doctor
		[HttpPut]
		public ActionResult Put([FromBody] Doctor doctor)
		{
			if (doctor == null) throw new NullReferenceException("doctor not found");
			try
			{
				var entity = _doctorService.Put(doctor);
				return Ok(entity);
			}
			catch
			{
				throw new NullReferenceException("doctor Registration Failed");
			}
		}

		// usage : api/doctor/p0001
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(string id)
		{
			if (id == null) throw new NullReferenceException("doctor not found");
			if (!_context.Doctors.Any()) throw new NullReferenceException("doctor not found");
			var doctor = await _doctorService.Get(id);
			return Ok(doctor);
		}

		// usage : api/doctor?all=true
		[HttpGet]
		public async Task<ActionResult> Get(bool all)
		{
			if (!all) throw new NullReferenceException("Doctor not found");
			if (!_context.Doctors.Any()) throw new NullReferenceException("Doctor no entries");
			var doctors = await _doctorService.GetRange(all);
			return Ok(doctors);
		}
	}
}
