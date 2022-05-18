using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.Business.Repositories.IRepository;
using LogSuite.DataAccess;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models.CredRisk;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.CredRisk
{
    public class CommitteeRepository : ICommitteeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CommitteeRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<CommitteeDTO> Create(CommitteeDTO dtp)
        {
            Committee committee = _mapper.Map<CommitteeDTO, Committee>(dtp);
            var newCommittee = await _db.Committees.AddAsync(committee);
            await _db.SaveChangesAsync();
            return _mapper.Map<Committee, CommitteeDTO>(newCommittee.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var entity = await _db.Committees
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (entity != null)
            {
                _db.Committees.Remove(entity);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public Task<IEnumerable<CommitteeDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CommitteeDTO> Get(int id)
        {
            var entity = await _db.Committees
                .Include(x => x.CommitteeLimit)
                .Include(x => x.CommitteeStatus)
                .FirstOrDefaultAsync(x => x.Id == id);
            var dto = _mapper.Map<Committee, CommitteeDTO>(entity);
            return dto;
        }

        public async Task<IEnumerable<CommitteeDTO>> GetAll(int counterpartyId)
        {
            try
            {
                var entities = await _db.Committees
                    .Include(x => x.CommitteeLimit)
                    .Include(x => x.CommitteeStatus)
                    .Where(x => x.CounterpartyId == counterpartyId)
                    .OrderByDescending(x => x.DateStart)
                    .ToListAsync();
                IEnumerable<CommitteeDTO> dtos = _mapper.Map<IEnumerable<Committee>, IEnumerable<CommitteeDTO>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CommitteeDTO> IsUnique(CommitteeDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var entity = await _db.Committees
                        .FirstOrDefaultAsync(x => x.CounterpartyId == dto.CounterpartyId
                        && x.DateStart == dto.DateStart);
                    var result = _mapper.Map<Committee, CommitteeDTO>(entity);
                    return result;
                }
                else
                {
                    var entity = await _db.Committees
                        .FirstOrDefaultAsync(x => x.CounterpartyId == dto.CounterpartyId
                        && x.DateStart == dto.DateStart
                        && x.Id != id);
                    var result = _mapper.Map<Committee, CommitteeDTO>(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<PagedList<CommitteeDTO>> GetPaged(Params parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<CommitteeDTO> Update(CommitteeDTO dto)
        {
            try
            {
                Committee entity = await _db.Committees.FindAsync(dto.Id);
                entity.CommitteeStatusId = dto.CommitteeStatusId;
                entity.CommitteeLimitId = dto.CommitteeLimitId;
                entity.DateStart = dto.DateStart;
                entity.Comment = dto.Comment;
                var updated = _db.Committees.Update(entity);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<Committee, CommitteeDTO>(updated.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
