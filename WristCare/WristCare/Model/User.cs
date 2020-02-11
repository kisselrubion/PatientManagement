using System;
using System.Collections.Generic;

namespace WristCare.Model
{
	public class User
	{
		public int UserId { get; set; }
		public string UserAccountId { get; set; }
		public bool IsArchived { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ContactNumber { get; set; }
		public DateTime BirthDate { get; set; }
		public string Sex { get; set; }
		public string Profession { get; set; }

		public string Email { get; set; }
		public string Age { get; set; }
		public string Address { get; set; }

		public List<Account> Accounts { get; set; }

	}
}