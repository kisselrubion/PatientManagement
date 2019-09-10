using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.CourseServices
{
	public interface ICourseService
	{
		Task<Course> Post(Course course);
		bool Put(Course course);
		Task<Course> Get(string id);
		Task<bool> Remove(string id);
	}
}
