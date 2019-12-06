using System;
using System.Collections.Generic;
using System.Text;

namespace WristCare.Model
{
	public class Measurement
	{
		public int MeasurementId { get; set; }
		public int? MeasurementNumber { get; set; }
		public string Description { get; set; }
		public string Comments { get; set; }
		public DateTime  Date { get; set; }
		public string MeasurementValue { get; set; }
		public int? ReminderInterval { get; set; }

		//FK
		public int? MedicineId { get; set; }
		public int? TreatmentId { get; set; }
		public Medicine Medicine { get; set; }
		public Treatment Treatment { get; set; }
	}
}
