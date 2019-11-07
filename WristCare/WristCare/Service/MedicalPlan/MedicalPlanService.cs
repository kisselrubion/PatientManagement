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

		public async Task GetAllRelatedMedicalPlan(int id)
		{
			var result = await _requestProvider.GetAsync<CourseHistory>("courseHistory/"+id);
		}
		public async Task<Medicine> AddMedicinePlan(Medicine medicine)
		{
			var response = await _requestProvider.PostAsync("medicine", medicine);
			return response;
		}
	}
}
