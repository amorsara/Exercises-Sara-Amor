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
    public class UsersController : ControllerBase
    {
        private readonly MyDBContext _context;

        public UsersController(MyDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/api/users/getusers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        [Route("/api/users/createuser")]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            
            user.Dateofbirth = user.Dateofbirth.ToLocalTime();
            var result = CheckUser(user);
            if(result == false)
            {
                return BadRequest();
            }

            if (_context.Users == null)
            {
              return Problem("Entity set 'MyDBContext.Users'  is null.");
            }
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Userid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("CreateUser", new { id = user.Userid }, user);
        }

        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.Userid == id)).GetValueOrDefault();
        }

        private static bool CheckUser(User user)
        {
            var result1 = ValidID(user.Userid);
            var result2 = ValidPhone(user.Phone);
            var result3 = ValidMobile(user.Mobile);
            var result4 = ValidDate(user.Dateofbirth);
            var result5 = ValidName(user.Firstname);
            var result6 = ValidName(user.Lastname);
            var result7 = ValidName(user.City);
            var result8 = ValidName(user.Street);
            if (result1 && result2 && result3 && result4 && result5 && result6 && result7 && result8)
                return true;
            return false;
        }

        private static bool ValidID(string strID)
        {
            int[] id_12_digits = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int count = 0;

            if (strID == null)
                return false;

            strID = strID.Trim();
            if (strID.Length > 9)
            {
                return false;
            }

            string pattern = "^[0-9]+$";
            bool result = Regex.IsMatch(strID, pattern);
            if (!result)
            {
                return false;
            }

            strID = strID.PadLeft(9, '0');

            for (int i = 0; i < 9; i++)
            {
                int num = Int32.Parse(strID.Substring(i, 1)) * id_12_digits[i];

                if (num > 9)
                    num = (num / 10) + (num % 10);

                count += num;
            }

            return (count % 10 == 0);
        }

        private static bool ValidPhone(string? phone)
        {
            if (phone == null) return true;
            phone = phone.Trim();
            if (phone.Length < 9 || phone.Length > 11)
            {
                return false;
            }
            string pattern = "^0[234589]\\-?[0-9]{7}";
            bool result = Regex.IsMatch(phone, pattern);
            if (result)
            {
                return true;
            }
            pattern = "^071\\-?8[0-9]{6}";
            result = Regex.IsMatch(phone, pattern);
            if (result)
            {
                return true;
            }
            pattern = "^072\\-?[23][0-9]{6}";
            result = Regex.IsMatch(phone, pattern);
            if (result)
            {
                return true;
            }
            pattern = "^073\\-?[237][0-9]{6}";
            result = Regex.IsMatch(phone, pattern);
            if (result)
            {
                return true;
            }
            pattern = "^074\\-?7[0-9]{6}";
            result = Regex.IsMatch(phone, pattern);
            if (result)
            {
                return true;
            }
            pattern = "^076\\-?[258][0-9]{6}";
            result = Regex.IsMatch(phone, pattern);
            if (result)
            {
                return true;
            }
            pattern = "^077\\-?[0-9]{7}";
            result = Regex.IsMatch(phone, pattern);
            if (result)
            {
                return true;
            }
            pattern = "^079\\-?[2356789][0-9]{6}";
            result = Regex.IsMatch(phone, pattern);
            if (result)
            {
                return true;
            }
            return false;
        }

        private static bool ValidMobile(string? phone)
        {
            if (phone == null) return true;
            phone = phone.Trim();
            if (phone.Length > 11 || phone.Length < 10)
            {
                return false;
            }
            string pattern = "^05[01234689]\\-?[0-9]{7}";
            bool result = Regex.IsMatch(phone, pattern);
            if (result)
            {
                return true;
            }
            pattern = "^055\\-?[235789][0-9]{6}";
            result = Regex.IsMatch(phone, pattern);
            if (result)
            {
                return true;
            }
            pattern = "^055\\-?44[0-9]{5}";
            result = Regex.IsMatch(phone, pattern);
            if (result)
            {
                return true;
            }
            return false;
        }

        private static bool ValidName(string? name)
        {
            if(name == null) return false;
            name = name.Trim();
            string pattern = "^[a-zA-Z]+$|^[\u0590-\u05fe]+$"; 
            bool result = Regex.IsMatch(name, pattern);
            if (!result)
            {
                return false;
            }
            return true;
        }

        private static bool ValidDate(DateTime date)
        {
            if (date > DateTime.Now)
                return false;
            return true;
        }

        // פונקציה של שאלת הבונוס
        [HttpGet("/api/users/sumunvaccinatedusers")]
        public int SumUnvaccinatedUsers()
        {
            var sumPatients = _context.Patients.Count();
            var sumUsers = _context.Users.Count();
            var sumVaccinations = _context.Vaccinations.Select(v => v.Userid).Distinct().Count();
            return sumUsers - (sumPatients + sumVaccinations);
        }

    }
}
