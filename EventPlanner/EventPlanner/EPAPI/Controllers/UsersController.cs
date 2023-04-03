using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventPlannerModels;
using EPAPI.Models;

namespace EPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EventPlanningContext _context;

        public UsersController()
        {
            _context = new EventPlanningContext();
        }

        // GET: api/Users/UserTypes
        [HttpGet]
        [Route("UserTypes")]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.UserType>>> GetUserTypes()
        {
            if (_context.UserTypes == null)
            {
                return NotFound();
            }
            var usertypes = await (from u in _context.UserTypes
                               select new EventPlannerModels.UserType
                               {
                                   Id = u.Id,
                                   Type = u.Type,
                               }).ToListAsync();
            return usertypes;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventPlannerModels.User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
          var users = await (from u in _context.Users
                             join ut in _context.UserTypes on u.UserTypeId equals ut.Id
                             select new EventPlannerModels.User
                             {
                                 Id = u.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 UserTypeId = u.UserTypeId,
                                 UserType = new EventPlannerModels.UserType
                                 {
                                     Id = ut.Id,
                                     Type = ut.Type
                                 }
                             }).ToListAsync();
            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventPlannerModels.User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await (from u in _context.Users
                               join ut in _context.UserTypes on u.UserTypeId equals ut.Id
                              where u.Id == id
                               select new EventPlannerModels.User
                               {
                                   Id = u.Id,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Email = u.Email,
                                   UserTypeId = u.UserTypeId,
                                   UserType = new EventPlannerModels.UserType
                                   {
                                       Id = ut.Id,
                                       Type = ut.Type
                                   }
                               }).FirstAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResult>> PutUser(int id, EventPlannerModels.User user)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                string password = (from u in _context.Users where u.Id == id select u.Password).First();
                Models.User context_user = new Models.User()
                {
                    Id = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserTypeId = user.UserTypeId,
                    Password = password,
                };
                _context.Entry(context_user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
                throw;
            }


            return generalResult;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> PostUser(EventPlannerModels.User user)
        {
            GeneralResult generalResult =
                    new GeneralResult() { Result = false };
            try
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'MoviesContext.Categories'  is null.");
                }
                Models.User context_user = new Models.User()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    UserTypeId = user.UserTypeId,
                };
                _context.Users.Add(context_user);
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
                throw;
            }

            return generalResult;
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResult>> DeleteUser(int id)
        {
            GeneralResult generalResult =
                new GeneralResult() { Result = false };
            try
            {
                if (_context.Users == null)
                {
                    return NotFound();
                }
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
                throw;
            }
            return generalResult;
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
