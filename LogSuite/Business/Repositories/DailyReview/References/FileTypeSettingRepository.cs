using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.Business.Repositories.IRepository.References;
using LogSuite.Business.Repositories.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;

namespace LogSuite.Business.Repositories.DailyReview.References
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
            var toDb = _mapper.Map<FileTypeSettingDTO, FileTypeSetting>(dto);
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

        public Task<IEnumerable<FileTypeSettingDTO>> GetAll()
        {
            var entities = _db.FileTypeSettings;
            var dtos = _mapper.Map<IEnumerable<FileTypeSetting>, IEnumerable<FileTypeSettingDTO>>(entities);
            return Task.FromResult(dtos);
        }

        public Task<FileTypeSettingDTO> IsUnique(FileTypeSettingDTO dto, int id = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<FileTypeSettingDTO>> GetPaged(Params parameters)
        {
            var source = _db.FileTypeSettings
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result =
                await PagedList<FileTypeSetting>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<FileTypeSettingDTO>>(result);

            return new PagedList<FileTypeSettingDTO>(entities, result.MetaData);
        }

        public async Task<FileTypeSettingDTO> Update(FileTypeSettingDTO dto)
        {
            var fromDb = await _db.FileTypeSettings.FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.FileTypeSettings.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<FileTypeSetting, FileTypeSettingDTO>(updated.Entity);
            return result;

        }
    }
}