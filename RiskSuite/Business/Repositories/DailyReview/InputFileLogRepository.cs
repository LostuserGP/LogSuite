using AutoMapper;
using Business.Repositories.IRepository;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Business.Repositories.IRepository;

namespace LogSuite.Business.Repositories
{
    public class InputFileLogRepository : IInputFileLogRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public InputFileLogRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<InputFileLogDTO> Create(InputFileLogDTO dto)
        {
            //InputFileLog toDb = _mapper.Map<InputFileLogDTO, InputFileLog>(dto);
            InputFileLog toDb = new InputFileLog()
            {
                Id = dto.Id,
                Filename = dto.Filename,
                UserId = dto.UserId,
                TimeInput = dto.TimeInput,
                TimeFile = dto.TimeFile,
                DateFile = dto.DateFile
            };
            var result = await _db.InputFileLogs.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<InputFileLog, InputFileLogDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.InputFileLogs.FindAsync(id);
            if (fromDb != null)
            {
                _db.InputFileLogs.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<InputFileLogDTO> Get(int id)
        {
            var fromDb = await _db.InputFileLogs
                .Where(x => x.Id == id)
                .Include(x => x.User)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<InputFileLog, InputFileLogDTO>(fromDb);
            return dto;
        }

        public async Task<InputFileLogDTO> GetByFilename(string filename)
        {
            var fromDb = await _db.InputFileLogs
                .Where(x => x.Filename == filename)
                .Include(x => x.User)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<InputFileLog, InputFileLogDTO>(fromDb);
            return dto;
        }

        public async Task<InputFileLogDTO> IsUnique(InputFileLogDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.InputFileLogs.Include(x => x.User)
                    .Where(x => x.Filename.ToLower().Equals(dto.Filename.ToLower()))
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<InputFileLog, InputFileLogDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.InputFileLogs.Include(x => x.User)
                    .Where(x => x.Filename.ToLower().Equals(dto.Filename.ToLower())
                        && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<InputFileLog, InputFileLogDTO>(fromDb);
                return result;
            }
        }

        public async Task<object> Update(InputFileLogDTO dto)
        {
            var fromDb = await _db.InputFileLogs
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();
            //var toUpdate = _mapper.Map(dto, fromDb);
            fromDb.Filename = dto.Filename;
            fromDb.UserId = dto.UserId;
            fromDb.TimeInput = dto.TimeInput;
            fromDb.TimeFile = dto.TimeFile;
            fromDb.DateFile = dto.DateFile;
            var updated = _db.InputFileLogs.Update(fromDb);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<InputFileLog, InputFileLogDTO>(updated.Entity);
            return result;
        }
    }
}
