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
		CourseHistory Put(CourseHistory courseHistory);
		Task<CourseHistory> Get(int id);
		Task<bool> Remove(string id);
	}
}
