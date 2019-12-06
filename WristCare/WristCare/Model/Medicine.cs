using System;
using System.Collections.Generic;

namespace WristCare.Model
{
	public class Medicine
	{
		public int MedicineId { get; set; }
		public int? MedicineNumber { get; set; }
		public string MedicineName { get; set; }
		public DateTime  Date { get; set; }
		public int? Interval { get; set; }
		public string Comments { get; set; }
		public string Dosage { get; set; }
		public int? CourseId { get; set; }
		public Course Course { get; set; }

	}
}