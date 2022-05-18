using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LogSuite.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Server.Services.IServices;
using LogSuite.DataAccess.References;

namespace LogSuite.Server.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (_db.Roles.Any(x => x.Name == SD.Role_Admin)) return;

            var department = new Department()
            {
                Code = 10000002,
                Name = "Департамент логистики и хранения газа",
                ShortName = "ДЛиХГ"
            };
            var newDepartment = _db.Departments.Add(department);
            _db.SaveChanges();


            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Power_User)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Front_Office)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Security)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "lostuser@lostuser.ru",
                Name = "lostuser@lostuser.ru",
                Email = "lostuser@lostuser.ru",
                CustomClaim = "AdminClaim",
                DepartmentId = newDepartment.Entity.Id,
                EmailConfirmed = true
            }, "basi1isk").GetAwaiter().GetResult();

            var user = _userManager.FindByEmailAsync("lostuser@lostuser.ru").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }
    }
}
