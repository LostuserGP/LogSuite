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
    public class RatingAgencyRepository : IRatingAgencyRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RatingAgencyRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<RatingAgencyDTO> Create(RatingAgencyDTO dto)
        {
            RatingAgency toDb = _mapper.Map<RatingAgencyDTO, RatingAgency>(dto);
            var result = await _db.RatingAgencies.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<RatingAgency, RatingAgencyDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.RatingAgencies.FindAsync(id);
            if (fromDb != null)
            {
                var parent = await _db.RatingExternals
                    .Where(x => x.RatingAgencyId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }
                else
                {
                    _db.RatingAgencies.Remove(fromDb);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<RatingAgencyDTO> Get(int id)
        {
            var fromDb = await _db.RatingAgencies.FindAsync(id);
            var dto = _mapper.Map<RatingAgency, RatingAgencyDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<RatingAgencyDTO>> GetAll()
        {
            try
            {
                var entities = _db.RatingAgencies;
                IEnumerable<RatingAgencyDTO> dtos = _mapper.Map<IEnumerable<RatingAgency>, IEnumerable<RatingAgencyDTO>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RatingAgencyDTO> IsUnique(RatingAgencyDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.RatingAgencies
                        .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                    var result = _mapper.Map<RatingAgency, RatingAgencyDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.RatingAgencies
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower())
                        && x.Id != id);
                    var result = _mapper.Map<RatingAgency, RatingAgencyDTO>(fromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RatingAgencyDTO> Update(RatingAgencyDTO dto)
        {
            try
            {
                var fromDb = await _db.RatingAgencies.FindAsync(dto.Id);
                var toUpdate = _mapper.Map(dto, fromDb);
                var updated = _db.RatingAgencies.Update(toUpdate);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<RatingAgency, RatingAgencyDTO>(updated.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}