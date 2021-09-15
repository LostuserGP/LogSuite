using AutoMapper;
using Business.Repositories.IRepository.References;
using Microsoft.EntityFrameworkCore;
using LogSuite.Business;
using LogSuite.Business.Repositories;
using LogSuite.DataAccess;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.References
{
    public class CommitteeLimitRepository : ICommitteeLimitRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CommitteeLimitRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<CommitteeLimitDTO> Create(CommitteeLimitDTO dto)
        {
            CommitteeLimit toDb = _mapper.Map<CommitteeLimitDTO, CommitteeLimit>(dto);
            var result = await _db.CommitteeLimits.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<CommitteeLimit, CommitteeLimitDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.CommitteeLimits.FindAsync(id);
            if (fromDb != null)
            {
                var parent = await _db.Committees
                    .Where(x => x.CommitteeLimitId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }
                else
                {
                    _db.CommitteeLimits.Remove(fromDb);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<CommitteeLimitDTO> Get(int id)
        {
            var fromDb = await _db.CommitteeLimits.FindAsync(id);
            var dto = _mapper.Map<CommitteeLimit, CommitteeLimitDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<CommitteeLimitDTO>> GetAll()
        {
            try
            {
                var entities = _db.CommitteeLimits;
                IEnumerable<CommitteeLimitDTO> dtos = _mapper.Map<IEnumerable<CommitteeLimit>, IEnumerable<CommitteeLimitDTO>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CommitteeLimitDTO> IsUnique(CommitteeLimitDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.CommitteeLimits
                        .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                    var result = _mapper.Map<CommitteeLimit, CommitteeLimitDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.CommitteeLimits
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower())
                        && x.Id != id);
                    var result = _mapper.Map<CommitteeLimit, CommitteeLimitDTO>(fromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CommitteeLimitDTO> Update(CommitteeLimitDTO dto)
        {
            try
            {
                var fromDb = await _db.CommitteeLimits.FindAsync(dto.Id);
                var toUpdate = _mapper.Map(dto, fromDb);
                var updated = _db.CommitteeLimits.Update(toUpdate);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<CommitteeLimit, CommitteeLimitDTO>(updated.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}