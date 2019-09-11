using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.CourseHistoryService
{
	public interface ICourseHistoryService
	{
		Task<CourseHistory> Post(CourseHistory courseHistory);
		bool Put(CourseHistory courseHistory);
		Task<CourseHistory> Get(string id);
		Task<bool> Remove(string id);
	}
}
