using System.Collections.Generic;

namespace WristCare.Model
{
	public class Procedure
	{
		public int ProcedureId { get; set; }
		public int? ProcedureNumber { get; set; }

		public List<Course> Courses { get; set; }
	}
}