using AutoMapper;
using Business.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LogSuite.Business;
using LogSuite.Business.Repositories;
using LogSuite.DataAccess;
using LogSuite.Shared;
using LogSuite.Shared.Authorization;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogSuite.DataAccess.References;

namespace Business.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public AccountRepository(ApplicationDbContext db, IMapper mapper, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _mapper = mapper;
        }
        public async Task<UserDTO> Create(UserDTO accountDTO)
        {
            var generatedPassword = GenerateRandomPassword();
            var user = new ApplicationUser
            {
                UserName = accountDTO.Email,
                Email = accountDTO.Email,
                Name = accountDTO.Name,
                DepartmentId = accountDTO.Department.Id,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, generatedPassword);

            return null;
        }

        public Task<int> Delete(string departmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> Get(string accountId)
        {
            var account = await _userManager.FindByIdAsync(accountId);
            return null;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            try
            {
                var departments = _db.Departments;
                IEnumerable<UserDTO> departmentDTOs = _mapper.Map<IEnumerable<Department>, IEnumerable<UserDTO>>(departments);
                return departmentDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PagedList<UserDTO>> GetPaged(Params parameters)
        {
            var source = _db.Departments
                    .Include(x => x.ApplicationUsers)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<Department>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var departments = _mapper.Map<List<UserDTO>>(result);

            return new PagedList<UserDTO>(departments, result.MetaData);
        }

        public async Task<UserDTO> IsUnique(UserDTO departmentDTO, string departmentId = "")
        {
            return null;
        }

        public async Task<UserDTO> Update(UserDTO departmentDTO)
        {
            try
            {
                Department departmentFromDb = await _db.Departments.FindAsync(departmentDTO.Id);
                Department departmentToUpdate = _mapper.Map(departmentDTO, departmentFromDb);
                var updatedDepartment = _db.Departments.Update(departmentToUpdate);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<Department, UserDTO>(updatedDepartment.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}