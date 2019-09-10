﻿using System.Threading.Tasks;
using WristCare.Model;

namespace WristCare.Service.Courses
{
	public class CourseService
	{
		private readonly RequestProvider.RequestProvider _requestProvider;

		public CourseService(RequestProvider.RequestProvider requestProvider)
		{
			_requestProvider = requestProvider;
		}

		public async Task<Course> CreateCourse(Course course)
		{
			var response = await _requestProvider.PostAsync("course", course);
			return response;
		}
	}
}
