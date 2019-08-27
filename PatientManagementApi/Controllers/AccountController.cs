using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using PatientManagementBackend.Model;

namespace PatientManagementApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly PMDbContext _context;
        public AccountController(PMDbContext context)
        {
            _context = context;

        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (_context.Accounts.Count() <= 0) throw new NullReferenceException("Accounts not found");
            var account = _context.Accounts.FirstOrDefault(c => c.AccountId == id);
            return Ok(account);
        }
    }
}