using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Booking.Core.Entities;
using Gym.Data.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Localization;

namespace Gym.Web.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public GymClassesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            db = context;
            this.userManager = userManager;
        }

        // GET: GymClasses
        public async Task<IActionResult> Index()
        {
            return View(await db.GymClasses.ToListAsync());
        }

        public async Task<IActionResult> BookingToggle(int? id)
        {
            if (id is null) return BadRequest();

            //Check who is logged in with Get User id
            var userId = userManager.
                GetUserId(User);

            // Get current gympass
            // include gymclasses, include all rows with attending members
            var currentGymClass = await db.GymClasses.
                Include(g => g.AttendedMembers)
                // get one gym class
                .FirstOrDefaultAsync(a => a.Id == id);

            // less code line 38-46
            // var attending = db.ApplicationUserGymClasses.Find(userId, id);
            // need to change and cast Id to int: GymClassId = currentGymClass.Id --> GymClassId = (int)Id

            // Am i booked in the class?
            var attending = currentGymClass?.AttendedMembers.
                FirstOrDefault(a => a.ApplicationUserId == userId);

            if(attending is null)
            {
                // make a new booking if attending is null (no current booking)
                var booking = new ApplicationUserGymClass
                {
                    ApplicationUserId = userId,
                    GymClassId = currentGymClass.Id
                };
                // add booking to db
                db.ApplicationUserGymClasses.Add(booking);
                await db.SaveChangesAsync();
            }
            //not null I want to remove my attending
            else
            {
                db.ApplicationUserGymClasses.Remove(attending);
                await db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: GymClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await db.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // GET: GymClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                db.Add(gymClass);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        // GET: GymClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await db.GymClasses.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,Duration,Description")] GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(gymClass);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        // GET: GymClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await db.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymClass = await db.GymClasses.FindAsync(id);
            db.GymClasses.Remove(gymClass);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
            return db.GymClasses.Any(e => e.Id == id);
        }
    }
}
