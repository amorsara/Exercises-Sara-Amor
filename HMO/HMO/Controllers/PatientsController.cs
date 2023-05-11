using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.GeneratedModels;

namespace HMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly MyDBContext _context;

        public PatientsController(MyDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/api/patients/getpatients")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
          if (_context.Patients == null)
          {
              return NotFound();
          }
            return await _context.Patients.ToListAsync();
        }

        [HttpPost]
        [Route("/api/patients/createpatient")]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            patient.Datenegative = patient.Datenegative.ToLocalTime();
            patient.Datepositive = patient.Datepositive.ToLocalTime();

            if (!ValidDate(patient.Datepositive, patient.Datenegative))
            {
                return BadRequest();
            }

            if (_context.Patients == null)
            {
              return Problem("Entity set 'MyDBContext.Patients'  is null.");
            }

            if(PatientExists(patient.Userid)) return BadRequest();

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreatePatient", new { id = patient.Codepatient }, patient);
        }

        private bool PatientExists(string id)
        {
            return (_context.Patients?.Any(e => e.Userid == id)).GetValueOrDefault();
        }

        private static bool ValidDate(DateTime date1,DateTime date2)
        {
            if (date1 > DateTime.Now || date2 > DateTime.Now)
                return false;
            if (date1 > date2)
                return false;
            return true;
        }
    }
}
