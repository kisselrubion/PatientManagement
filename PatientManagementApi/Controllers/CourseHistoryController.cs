using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

		public CourseHistoryController(PMDbContext context)
		{
			_context = context;
		}
	}
}
