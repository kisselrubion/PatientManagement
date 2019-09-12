using System;
using System.Collections.Generic;

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


		public List<Transfer> Transfers { get; set; }
		public List<Treatment> Treatments { get; set; }
		public List<Medicine> Medicines { get; set; }
		public List<Procedure> Procedures { get; set; }
	}
}