using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagementApi.Services.BaseService;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Services.ProcedureServices
{
	public class ProcedureService : IBaseService<Procedure>
	{
		private readonly PMDbContext _context;

		public ProcedureService(PMDbContext context)
		{
			_context = context;
		}

		public async Task<Procedure> Post(Procedure item)
		{
			try
			{
				var addedProcedure = await _context.Procedures.AddAsync(item);
				await _context.SaveChangesAsync();
				return addedProcedure.Entity;
			}
			catch
			{
				return new Procedure();
			}
		}

		public bool Put(Procedure item)
		{
			try
			{
				_context.Procedures.Update(item);
				_context.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<Procedure> Get(string id)
		{
			var procedure = await _context.Procedures.FirstOrDefaultAsync(c => c.ProcedureNumber == int.Parse(id));
			return procedure ?? new Procedure();
		}

		public async Task<ICollection<Procedure>> GetRange(bool all)
		{
			if (!all) return new List<Procedure>();
			var procedures = await _context.Procedures.ToListAsync();
			return procedures;
		}

		public Task<bool> Remove(string id)
		{
			//todo
			throw new NotImplementedException();
		}

		public void Add(Procedure item)
		{
			throw new NotImplementedException();
		}

		public void AddRange(ICollection<Procedure> items)
		{
			throw new NotImplementedException();
		}

		public void Remove(Procedure item)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(ICollection<Procedure> items)
		{
			throw new NotImplementedException();
		}

		public void Update(Procedure item)
		{
			throw new NotImplementedException();
		}

		public void Update(ICollection<Procedure> items)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Procedure> All()
		{
			throw new NotImplementedException();
		}
	}
}
