using AutoMapper;
using Business.Repositories.IRepository.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.References;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Business.Repositories.References
{
    public class FileTypeSettingRepository : IFileTypeSettingRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public FileTypeSettingRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<FileTypeSettingDTO> Create(FileTypeSettingDTO dto)
        {
            FileTypeSetting toDb = _mapper.Map<FileTypeSettingDTO, FileTypeSetting>(dto);
            var result = await _db.FileTypeSettings.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<FileTypeSetting, FileTypeSettingDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.FileTypeSettings.FindAsync(id);
            if (fromDb != null)
            {
                _db.FileTypeSettings.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<FileTypeSettingDTO> Get(int id)
        {
            var fromDb = await _db.FileTypeSettings.FindAsync(id);
            var dto = _mapper.Map<FileTypeSetting, FileTypeSettingDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<FileTypeSettingDTO>> GetAll()
        {
            var entities = _db.FileTypeSettings;
            IEnumerable<FileTypeSettingDTO> dtos = _mapper.Map<IEnumerable<FileTypeSetting>, IEnumerable<FileTypeSettingDTO>>(entities);
            return dtos;
        }

        public async Task<PagedList<FileTypeSettingDTO>> GetPaged(Params parameters)
        {
            var source = _db.FileTypeSettings
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<FileTypeSetting>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<FileTypeSettingDTO>>(result);

            return new PagedList<FileTypeSettingDTO>(entities, result.MetaData);
        }

        public async Task<FileTypeSettingDTO> Update(FileTypeSettingDTO dto)
        {
            var fromDb = await _db.FileTypeSettings.FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            var updated = _db.FileTypeSettings.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<FileTypeSetting, FileTypeSettingDTO>(updated.Entity);
            return result;
        }
    }
}
