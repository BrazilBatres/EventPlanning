using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventPlannerModels;

namespace EventPlanner.Controllers
{
    public class UsersController : Controller
    {
        

        public UsersController() { }
        

        // GET: Users
        public async Task<IActionResult> Index()
        {
            IEnumerable<User> users = await Functions.APIServices.UsersGetList();

            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            User user = await Functions.APIServices.UsersDetails(id);
            return View(user);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<UserType> userTypes = await Functions.APIServices.UserTypesGetList();
            ViewData["UserTypeId"] = new SelectList(userTypes, "Id", "Type");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Password,UserTypeId")] User user)
        {
            GeneralResult generalResult = await Functions.APIServices.UsersCreate(user);
            if (generalResult.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            User user = await Functions.APIServices.UsersDetails(id);
            IEnumerable<UserType> userTypes = await Functions.APIServices.UserTypesGetList();
            ViewData["UserTypeId"] = userTypes.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Type,
                Selected = (s.Id == user.UserTypeId)
            }).ToList();
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("FirstName,LastName,Email,Password,UserTypeId")] User user)
        {
            GeneralResult generalResult = await Functions.APIServices.UsersEdit(Id, user);
            if (generalResult.Result)
            {

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            User user = await Functions.APIServices.UsersDetails(id);
          
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            GeneralResult generalResult = await Functions.APIServices.UsersDelete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
