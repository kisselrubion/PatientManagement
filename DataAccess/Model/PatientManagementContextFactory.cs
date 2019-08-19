using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Model
{
	public class PatientManagementContextFactory : IDesignTimeDbContextFactory<PatientManagementDbContext>
	{
		public PatientManagementDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<PatientManagementDbContext>();

			//optionsBuilder.UseSqlServer(
			//	"Data Source=ASUS-AVARUS\\SQLEXPRESS,1433; Initial Catalog=PatientManagementDb;User ID=sa;Password=kissel");

			return new PatientManagementDbContext(optionsBuilder.Options);
		}
	}
}
