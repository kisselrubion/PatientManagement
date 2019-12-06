using System;
using System.Collections.Generic;

namespace WristCare.Model
{
	public class Procedure
	{
		public int ProcedureId { get; set; }
		public int? ProcedureNumber { get; set; }
		public DateTime  Date { get; set; }
		public int? CourseId { get; set; }
		public Course Course { get; set; }
	}
}