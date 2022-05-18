using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.Business.Repositories.IRepository.ICredRisk.IReferences;
using LogSuite.Business.Repositories.IRepository.References;
using LogSuite.Business.Repositories.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models.CredRisk;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.CredRisk.References
{
    public class CommitteeStatusRepository : ICommitteeStatusRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CommitteeStatusRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CommitteeStatusDTO> Create(CommitteeStatusDTO dto)
        {
            CommitteeStatus toDb = _mapper.Map<CommitteeStatusDTO, CommitteeStatus>(dto);
            var result = await _db.CommitteeStatuses.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<CommitteeStatus, CommitteeStatusDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.CommitteeStatuses.FindAsync(id);
            if (fromDb == null) return 0;
            var parent = await _db.Committees
                .Where(x => x.CommitteeStatusId == id)
                .AnyAsync();
            if (parent)
            {
                return -1;
            }

            _db.CommitteeStatuses.Remove(fromDb);
            return await _db.SaveChangesAsync();
        }

        public async Task<CommitteeStatusDTO> Get(int id)
        {
            var fromDb = await _db.CommitteeStatuses.FindAsync(id);
            var dto = _mapper.Map<CommitteeStatus, CommitteeStatusDTO>(fromDb);
            return dto;
        }

        public Task<IEnumerable<CommitteeStatusDTO>> GetAll()
        {
            try
            {
                var entities = _db.CommitteeStatuses;
                var dtos = _mapper.Map<IEnumerable<CommitteeStatus>, IEnumerable<CommitteeStatusDTO>>(entities);
                return Task.FromResult(dtos);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IEnumerable<CommitteeStatusDTO>>(null);
            }
        }

        public async Task<CommitteeStatusDTO> IsUnique(CommitteeStatusDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.CommitteeStatuses
                        .FirstOrDefaultAsync(x => string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase));
                    var result = _mapper.Map<CommitteeStatus, CommitteeStatusDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.CommitteeStatuses
                        .FirstOrDefaultAsync(x => (string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase))
                                                  && x.Id != id);
                    var result = _mapper.Map<CommitteeStatus, CommitteeStatusDTO>(fromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PagedList<CommitteeStatusDTO>> GetPaged(Params parameters)
        {
            var source = _db.CommitteeStatuses
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result =
                await PagedList<CommitteeStatus>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<CommitteeStatusDTO>>(result);
            return new PagedList<CommitteeStatusDTO>(entities, result.MetaData);
        }

        public async Task<CommitteeStatusDTO> Update(CommitteeStatusDTO dto)
        {
            var fromDb = await _db.CommitteeStatuses.FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.CommitteeStatuses.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<CommitteeStatus, CommitteeStatusDTO>(updated.Entity);
            return result;
        }
    }
}