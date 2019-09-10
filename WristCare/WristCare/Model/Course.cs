using System;

namespace WristCare.Model
{
	public class Course
	{
		public int CourseId { get; set; }

		//Pseudo Primary key used for querying
		public string TransactionId { get; set; }

		public string Title { get; set; }
		public DateTime CourseDate { get; set; }
		public bool IsArchived { get; set; }
		public string Description { get; set; }

		//Foreign key
		public int? TransferId { get; set; }
		public int? TreatmentId { get; set; }
		public int? MedicineId { get; set; }
		public int? MedProcedureId { get; set; }

		public Transfer Transfer { get; set; }
		public Treatment Treatment { get; set; }
		public Medicine Medicine { get; set; }
		public Procedure Procedure { get; set; }
	}
}