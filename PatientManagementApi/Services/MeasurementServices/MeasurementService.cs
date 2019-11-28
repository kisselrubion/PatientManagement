using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementApi.Services.BaseService;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.MeasurementServices
{
	public class MeasurementService : IBaseService<Measurement>
	{
		private readonly PMDbContext _context;

		public MeasurementService(PMDbContext context)
		{
			_context = context;
		}

		public async Task<Measurement> Post(Measurement item)
		{
			try
			{
				var addedMeasurements = await _context.Measurements.AddAsync(item);
				await _context.SaveChangesAsync();
				return addedMeasurements.Entity;
			}
			catch
			{
				return new Measurement();
			}
		}

		public bool Put(Measurement item)
		{
			try
			{
				_context.Measurements.Update(item);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<Measurement> Get(string id)
		{
			var measurement = await _context.Measurements.FirstOrDefaultAsync(c => c.MeasurementNumber == int.Parse(id));
			return measurement ?? new Measurement();
		}

		public async Task<ICollection<Measurement>> GetRange(bool all)
		{
			if (!all) return new List<Measurement>();
			var measurement = await _context.Measurements.ToListAsync();
			return measurement;
		}

		public Task<bool> Remove(string id)
		{
			//todo
			throw new NotImplementedException();
		}

		public void Add(Measurement item)
		{
			throw new NotImplementedException();
		}

		public void AddRange(ICollection<Measurement> items)
		{
			throw new NotImplementedException();
		}

		public void Remove(Measurement item)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(ICollection<Measurement> items)
		{
			throw new NotImplementedException();
		}

		public void Update(Measurement item)
		{
			throw new NotImplementedException();
		}

		public void Update(ICollection<Measurement> items)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Measurement> All()
		{
			throw new NotImplementedException();
		}
	}
}
