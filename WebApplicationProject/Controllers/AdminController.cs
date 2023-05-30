using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Data;
using WebApplicationProject.Models;

namespace WebApplicationProject.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Admin
        public async Task<IActionResult> Index(string searchName, string role)
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            var usersQuery = _userManager.Users.AsQueryable();

            // Filter users by role
            if (!string.IsNullOrEmpty(role) && role != "All")
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role);
                usersQuery = usersQuery.Where(u => usersInRole.Contains(u));
            }

            // Filter users by name
            if (!string.IsNullOrEmpty(searchName))
            {
                usersQuery = usersQuery.Where(u => u.UserName.Contains(searchName));
            }

            var users = await usersQuery.ToListAsync();

            ViewBag.Roles = new SelectList(roles);
            ViewBag.SearchName = searchName;
            ViewBag.SelectedRole = role;

            return View(users);
        }





        // GET: Admin/Manage/5
        public async Task<IActionResult> Manage(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.Where(r => r.Name != "Administrator").ToListAsync();



            var model = new ManageViewModel
            {
                User = user,
                UserRoles = userRoles,
                AvailableRoles = roles
            };

            return View(model);
        }

        // POST: Admin/Manage/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(string id, string[] selectedRoles)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRolesList = selectedRoles != null ? selectedRoles.ToList() : new List<string>();

            //if(selectedRolesList.Count == 0) { }
            var rolesToAdd = selectedRolesList.Except(userRoles);
            var rolesToRemove = userRoles.Except(selectedRolesList);

            foreach (var roleToAdd in rolesToAdd)
            {
                var role = await _roleManager.FindByIdAsync(roleToAdd);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

            foreach (var roleToRemove in rolesToRemove)
            {
                await _userManager.RemoveFromRoleAsync(user, roleToRemove);
            }


            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to delete the user.");
                return View(user);
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ExportUsers(string role)
        {
            // Find the role by name
            var targetRole = await _roleManager.FindByNameAsync(role);
            if (targetRole == null)
            {
                // Role not found, handle accordingly (e.g., return an error view)
                return NotFound();
            }

            // Retrieve the users in the specified role
            var users = await _userManager.GetUsersInRoleAsync(role);

            // Create XML serializer
            var xmlSerializer = new XmlSerializer(typeof(List<IdentityUser>));

            using (var memoryStream = new MemoryStream())
            {
                // Serialize users to XML
                xmlSerializer.Serialize(memoryStream, users);
                var xmlBytes = memoryStream.ToArray();
                var xmlString = Encoding.UTF8.GetString(xmlBytes);

                var fileName = $"{role}_users_{DateTime.Now:yyyyMMddHHmmss}.xml";
                return File(xmlBytes, "text/xml", fileName);
            }
        }

       



    }
}
