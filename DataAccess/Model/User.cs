using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
	public class User
	{
		public int UserId { get; set; }
		public bool IsArchived { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ContactNumber { get; set; }
		public DateTime BirthDate { get; set; }
		public string Sex { get; set; }
		public string Email { get; set; }
		public string Age { get; set; }
		public string Address { get; set; }




	}

}
