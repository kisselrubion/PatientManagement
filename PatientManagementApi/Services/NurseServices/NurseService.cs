using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementApi.Services.BaseService;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.NurseServices
{
	public class NurseService : IBaseService<Nurse>
	{
		private readonly PMDbContext _context;

		public NurseService(PMDbContext context)
		{
			_context = context;
		}

		public async Task<Nurse> Post(Nurse item)
		{
			try
			{
				var addedNurse = await _context.Nurses.AddAsync(item);
				await _context.SaveChangesAsync();
				return addedNurse.Entity;
			}
			catch
			{
				return new Nurse();
			}
		}

		public bool Put(Nurse item)
		{
			try
			{
				_context.Nurses.Update(item);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<Nurse> Get(string id)
		{
			var nurse = await _context.Nurses.FirstOrDefaultAsync(c => c.NurseNumber == int.Parse(id));
			return nurse ?? new Nurse();
		}

		public async Task<ICollection<Nurse>> GetRange(bool all)
		{
			if (!all) return new List<Nurse>();
			var nurses = await _context.Nurses.ToListAsync();
			return nurses;
		}

		public Task<bool> Remove(string id)
		{
			//todo
			throw new NotImplementedException();
		}

		public void Add(Nurse item)
		{
			throw new NotImplementedException();
		}

		public void AddRange(ICollection<Nurse> items)
		{
			throw new NotImplementedException();
		}

		public void Remove(Nurse item)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(ICollection<Nurse> items)
		{
			throw new NotImplementedException();
		}

		public void Update(Nurse item)
		{
			throw new NotImplementedException();
		}

		public void Update(ICollection<Nurse> items)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Nurse> All()
		{
			throw new NotImplementedException();
		}
	}
}
