using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WristCare.Model;

namespace WristCare.Service.MedicalPlan
{
	public class MedicalPlanService
	{
		private RequestProvider.RequestProvider _requestProvider;

		public MedicalPlanService(RequestProvider.RequestProvider requestProvider)
		{
			_requestProvider = requestProvider;
		}

		public async Task<CourseHistory> GetCourseHistory(Course course)
		{
			var data = await _requestProvider.PostAsync<CourseHistory>("courseHistory/history/",course);
			return data;
		}

		public async Task<List<Medicine>> GetMedicinePlan(Course course)
		{
			var data = await _requestProvider.GetAsync<List<Medicine>>("medicine/MedicineCourse/" + course.CourseId);
			return data;
		}
		public async Task<Medicine> AddMedicinePlan(Medicine medicine)
		{
			var data = await _requestProvider.PostAsync("medicine", medicine);
			return data;
		}

		public async Task<CourseHistory> AddCourseHistory(CourseHistory courseHistory )
		{
			var data = await _requestProvider.PostAsync("coursehistory/", courseHistory);
			return data;
		}
	}
}
