using System;
using System.Collections.Generic;
using System.Text;

namespace WristCare.Model
{
	public class CourseType
	{
		public int Key { get; set; }
		public string Value { get; set; }

		public override string ToString()
		{
			return Value;
		}
	}
}
