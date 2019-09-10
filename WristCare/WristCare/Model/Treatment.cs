using System.Collections.Generic;

namespace WristCare.Model
{
	public class Treatment
	{
		public int TreatmentId { get; set; }

		public int? TreatmentNumber { get; set; }
		public List<Course> Courses { get; set; }

	}
}