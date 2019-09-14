using System.Collections.Generic;

namespace WristCare.Model
{
	public class Treatment
	{
		public int TreatmentId { get; set; }

		public int? TreatmentNumber { get; set; }
		public int? CourseId { get; set; }

		public Course Course { get; set; }
		public List<Measurement> Measurements { get; set; }


	}
}