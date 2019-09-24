using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementApi.Services.BaseService;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.TreatmentServices
{
	public class TreatmentService : IBaseService<Treatment>
	{
		private readonly PMDbContext _context;

		public TreatmentService(PMDbContext context)
		{
			_context = context;
		}

		public async Task<Treatment> Post(Treatment item)
		{
			try
			{
				var addedTreatment = await _context.Treatments.AddAsync(item);
				await _context.SaveChangesAsync();
				return addedTreatment.Entity;
			}
			catch
			{
				return new Treatment();
			}
		}

		public bool Put(Treatment item)
		{
			try
			{
				_context.Treatments.Update(item);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<Treatment> Get(string id)
		{
			var treatment = await _context.Treatments.FirstOrDefaultAsync(c => c.TreatmentNumber == int.Parse(id));
			return treatment ?? new Treatment();
		}

		public async Task<ICollection<Treatment>> GetRange(bool all)
		{
			if (!all) return new List<Treatment>();
			var treatments = await _context.Treatments.ToListAsync();
			return treatments;
		}

		public Task<bool> Remove(string id)
		{
			//todo
			throw new NotImplementedException();
		}

		public void Add(Treatment item)
		{
			throw new NotImplementedException();
		}

		public void AddRange(ICollection<Treatment> items)
		{
			throw new NotImplementedException();
		}

		public void Remove(Treatment item)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(ICollection<Treatment> items)
		{
			throw new NotImplementedException();
		}

		public void Update(Treatment item)
		{
			throw new NotImplementedException();
		}

		public void Update(ICollection<Treatment> items)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Treatment> All()
		{
			throw new NotImplementedException();
		}
	}
}
