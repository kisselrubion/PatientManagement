using System.Collections.Generic;

namespace WristCare.Model
{
	public class Medicine
	{
		public int MedicineId { get; set; }

		public int? MedicineNumber { get; set; }
		public List<Course> Courses { get; set; }

	}
}