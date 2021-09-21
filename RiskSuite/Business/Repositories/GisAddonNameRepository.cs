using AutoMapper;
using Business.Repositories.IRepository;
using LogSuite.Business;
using LogSuite.Business.Repositories;
using LogSuite.Business.Repositories.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class GisAddonNameRepository : IGisAddonNameRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisAddonNameRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<GisAddonNameDTO> Create(GisAddonNameDTO dto)
        {
            GisAddonName toDb = _mapper.Map<GisAddonNameDTO, GisAddonName>(dto);
            var result = await _db.GisAddonNames.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisAddonName, GisAddonNameDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GisAddonNames
                .FindAsync(id);
            if (fromDb != null)
            {
                _db.GisAddonNames.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<GisAddonNameDTO> Get(int id)
        {
            var fromDb = await _db.GisAddonNames
                .FindAsync(id);
            var dto = _mapper.Map<GisAddonName, GisAddonNameDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GisAddonNameDTO>> GetAll()
        {
            var entities = _db.GisAddonNames;
            IEnumerable<GisAddonNameDTO> dtos = _mapper.Map<IEnumerable<GisAddonName>, IEnumerable<GisAddonNameDTO>>(entities);
            return dtos;
        }

        public async Task<IEnumerable<GisAddonNameDTO>> GetAllByGisAddonId(int gisAddonId)
        {
            var entities = _db.GisAddonNames.Where(x => x.GisAddonId == gisAddonId);
            IEnumerable<GisAddonNameDTO> dtos = _mapper.Map<IEnumerable<GisAddonName>, IEnumerable<GisAddonNameDTO>>(entities);
            return dtos;
        }

        public async Task<PagedList<GisAddonNameDTO>> GetPagedByGisAddonId(int gisAddonId, Params parameters)
        {
            var source = _db.GisAddonNames
                    .Where(x => x.GisAddonId == gisAddonId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisAddonName>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisAddonNameDTO>>(result);
            return new PagedList<GisAddonNameDTO>(entities, result.MetaData);
        }

        public async Task<PagedList<GisAddonNameDTO>> GetPaged(Params parameters)
        {
            var source = _db.GisAddonNames
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisAddonName>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisAddonNameDTO>>(result);
            return new PagedList<GisAddonNameDTO>(entities, result.MetaData);
        }

        public async Task<GisAddonNameDTO> IsUnique(GisAddonNameDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisAddonNames
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()))
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisAddonName, GisAddonNameDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisAddonNames
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()))
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisAddonName, GisAddonNameDTO>(fromDb);
                return result;
            }
        }

        public async Task<GisAddonNameDTO> Update(GisAddonNameDTO dto)
        {
            var fromDb = await _db.GisAddonNames
                .FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            var updated = _db.GisAddonNames.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisAddonName, GisAddonNameDTO>(updated.Entity);
            return result;
        }
    }
}