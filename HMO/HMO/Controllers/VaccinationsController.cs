using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.GeneratedModels;

namespace HMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationsController : ControllerBase
    {
        private readonly MyDBContext _context;

        public VaccinationsController(MyDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/api/vaccinations/Ggetvaccinations")]
        public async Task<ActionResult<IEnumerable<Vaccination>>> GetVaccinations()
        {
          if (_context.Vaccinations == null)
          {
              return NotFound();
          }
            return await _context.Vaccinations.ToListAsync();
        }

        [HttpPost]
        [Route("/api/vaccinations/createvaccination")]
        public async Task<ActionResult<Vaccination>> CreateVaccination(Vaccination vaccination)
        {
            vaccination.Datevaccination = vaccination.Datevaccination.ToLocalTime();
            var result = CheckValues(vaccination.Datevaccination, vaccination.Userid);
            if (result == false)
            {
                return BadRequest();
            }

            if (_context.Vaccinations == null)
            {
              return Problem("Entity set 'MyDBContext.Vaccinations'  is null.");
            }
            _context.Vaccinations.Add(vaccination);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateVaccination", new { id = vaccination.Codevaccination }, vaccination);
        }

        private bool CheckValues(DateTime date, string id)
        {
            var result = ValidDate(date, id);
            var cnt = CntVaccinations(id);
            if (cnt == 0 || !result)
            {
                return false; 
            }
            return true;
        }

        private int CntVaccinations(string id)
        {
            var resualt = _context.Vaccinations.Count(v => v.Userid == id);
            return 4 - resualt;
        }

        private bool ValidDate(DateTime date, string id)
        {
            if (date > DateTime.Now)
                return false;
            var listVaccinations = _context.Vaccinations.Where(v => v.Userid == id).ToList();
            var vaccination = listVaccinations.LastOrDefault();
            DateTime d = date.AddMonths(-4);
            if (vaccination?.Datevaccination >= d)
                return false;
            return true;
        }
    }
}
