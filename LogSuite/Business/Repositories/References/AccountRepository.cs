using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.Business.Repositories.IRepository;
using LogSuite.DataAccess;
using LogSuite.DataAccess.References;
using LogSuite.Shared;
using LogSuite.Shared.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.References
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

        public async Task<UserDTO> Create(UserDTO dto)
        {
            var generatedPassword = GenerateRandomPassword();
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name,
                DepartmentId = dto.Department.Id,
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

        public Task<IEnumerable<UserDTO>> GetAll()
        {
            try
            {
                var departments = _db.Departments;
                var departmentDtos = _mapper.Map<IEnumerable<Department>, IEnumerable<UserDTO>>(departments);
                return Task.FromResult(departmentDtos);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IEnumerable<UserDTO>>(null);
            }
        }

        public async Task<PagedList<UserDTO>> GetPaged(Params parameters)
        {
            var source = _db.Departments
                .Include(x => x.ApplicationUsers)
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result =
                await PagedList<Department>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var departments = _mapper.Map<List<UserDTO>>(result);

            return new PagedList<UserDTO>(departments, result.MetaData);
        }

        public Task<UserDTO> IsUnique(UserDTO dto, string id = "")
        {
            return Task.FromResult<UserDTO>(null);
        }

        public async Task<UserDTO> Update(UserDTO dto)
        {
            try
            {
                var userFromDb = await _userManager.FindByIdAsync(dto.Id);
                if (userFromDb == null) return null;
                userFromDb.Name = dto.Name;
                userFromDb.Email = dto.Email;
                userFromDb.DepartmentId = dto.Department.Id;
                await _db.SaveChangesAsync();
                var result = _mapper.Map<ApplicationUser, UserDTO>(userFromDb);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            opts ??= new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            var randomChars = new[]
            {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ", // uppercase 
                "abcdefghijkmnopqrstuvwxyz", // lowercase
                "0123456789", // digits
                "!@$?_-" // non-alphanumeric
            };

            var rand = new Random(Environment.TickCount);
            var chars = new List<char>();

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

            for (int i = chars.Count;
                 i < opts.RequiredLength
                 || chars.Distinct().Count() < opts.RequiredUniqueChars;
                 i++)
            {
                var rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}