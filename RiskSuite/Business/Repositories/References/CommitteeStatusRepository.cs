using AutoMapper;
using Business.Repositories.IRepository.References;
using Microsoft.EntityFrameworkCore;
using RiskSuite.Business;
using RiskSuite.Business.Repositories;
using RiskSuite.DataAccess;
using RiskSuite.DataAccess.CredRisk;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.References
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
            if (fromDb != null)
            {
                var parent = await _db.Committees
                    .Where(x => x.CommitteeStatusId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }
                else
                {
                    _db.CommitteeStatuses.Remove(fromDb);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<CommitteeStatusDTO> Get(int id)
        {
            var fromDb = await _db.CommitteeStatuses.FindAsync(id);
            var dto = _mapper.Map<CommitteeStatus, CommitteeStatusDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<CommitteeStatusDTO>> GetAll()
        {
            try
            {
                var entities = _db.CommitteeStatuses;
                IEnumerable<CommitteeStatusDTO> dtos = _mapper.Map<IEnumerable<CommitteeStatus>, IEnumerable<CommitteeStatusDTO>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CommitteeStatusDTO> IsUnique(CommitteeStatusDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.CommitteeStatuses
                        .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                    var result = _mapper.Map<CommitteeStatus, CommitteeStatusDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.CommitteeStatuses
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower())
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

        public async Task<CommitteeStatusDTO> Update(CommitteeStatusDTO dto)
        {
            try
            {
                var fromDb = await _db.CommitteeStatuses.FindAsync(dto.Id);
                var toUpdate = _mapper.Map(dto, fromDb);
                var updated = _db.CommitteeStatuses.Update(toUpdate);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<CommitteeStatus, CommitteeStatusDTO>(updated.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}