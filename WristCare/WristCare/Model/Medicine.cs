using System.Collections.Generic;

namespace WristCare.Model
{
	public class Medicine
	{
		public int MedicineId { get; set; }

		public int? MedicineNumber { get; set; }
		public int? CourseId { get; set; }
		public Course Course { get; set; }
	}
}