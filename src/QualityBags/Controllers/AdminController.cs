using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualityBags.Data;
using QualityBags.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace QualityBags.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await ShowAllMembers());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccessFailedCount,Address,ConcurrencyStamp,Email,EmailConfirmed,Enabled,FirstName,LastName,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneHome,PhoneMobile,PhoneNumber,PhoneNumberConfirmed,PhoneWork,SecurityStamp,TwoFactorEnabled,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,AccessFailedCount,Address,ConcurrencyStamp,Email,EmailConfirmed,Enabled,FirstName,LastName,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneHome,PhoneMobile,PhoneNumber,PhoneNumberConfirmed,PhoneWork,SecurityStamp,TwoFactorEnabled,UserName")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUsers.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }

        #region MyRegion
        public async Task<ActionResult> EnableDisable(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Enabled = !user.Enabled;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<ApplicationUser>> ShowAllMembers()
        {
            IdentityRole role = await _context.Roles.SingleOrDefaultAsync(r => r.Name == "Customer");
            IEnumerable<ApplicationUser> allUsers = _context.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.Id));

            return allUsers;

        }
        #endregion
    }
}
