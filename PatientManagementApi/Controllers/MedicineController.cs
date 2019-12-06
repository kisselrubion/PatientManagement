using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientManagementApi.Services.MedicineServices;
using PatientManagementBackend.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PatientManagementApi.Controllers
{
	[Produces("application/json")]
	[Route("api/medicine")]
	[ApiController]

	public class MedicineController : Controller
	{

		private readonly PMDbContext _context;
		private readonly MedicineService _medicineService;

		public MedicineController(PMDbContext context, MedicineService medicineService)
		{
			_context = context;
			_medicineService = medicineService;
		}

		// usage: api/medicine
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] Medicine medicine)
		{
			if (medicine == null) throw new NullReferenceException("medicine not found");
			try
			{
				var entity = await _medicineService.Post(medicine);
				return Ok(entity);
			}
			catch
			{
				throw new NullReferenceException("medicine Registration Failed");
			}
		}

		// usage : api/medicine/c0001
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(string id)
		{
			if (id == "") throw new NullReferenceException("medicine not found");
			if (!_context.Medicines.Any()) throw new NullReferenceException("medicine not found");
			var medicine = await _medicineService.Get(id);
			return Ok(medicine);
		}

		// usage : api/medicine?all=true
		[HttpGet]
		public async Task<ActionResult> Get(bool all)
		{
			if (!all) throw new NullReferenceException("medicine not found");
			if (!_context.Medicines.Any()) throw new NullReferenceException("medicine no entries");
			var medicines = await _medicineService.GetRange(all);
			return Ok(medicines);
		}

		// usage : api/medicine/MedicineCourseHistory
		[HttpGet("MedicineCourse/{id}")]
		public async Task<ActionResult> MedicineCourse(int id)
		{
			if (id == 0) throw new NullReferenceException("medicine course  not found");
			if (!_context.Medicines.Any()) throw new NullReferenceException("medicine no entries");
			var medicines = await _medicineService.GetByForeignKey(id);
			return Ok(medicines ?? new List<Medicine>());
		}

		//api/medicine
		[HttpPut]
		public ActionResult Put([FromBody] Medicine medicine)
		{
			if (medicine == null)
			{
				throw new NoNullAllowedException("No medicine found");
			}

			try
			{
				var entity = _medicineService.Put(medicine);
				return Ok(entity);
			}
			catch
			{
				throw new NullReferenceException("medicine Update Failed");
			}
		}


	}

}
