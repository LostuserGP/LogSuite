using AutoMapper;
using Business.Repositories.IRepository;
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
    public class ReferenceRepository<TEntity> : IReferenceRepository<TEntity> where TEntity : class, IReferenceName
    {
        private readonly ApplicationDbContext _db;

        public ReferenceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var result = await _db.Set<TEntity>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.Set<TEntity>().FindAsync(id);
            if (fromDb != null)
            {
                _db.Set<TEntity>().Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<TEntity> Get(int id)
        {
            var fromDb = await _db.Set<TEntity>().FindAsync(id);
            return fromDb;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var entities = await _db.Set<TEntity>().ToListAsync();
            return entities;
        }

        public async Task<TEntity> IsUnique(TEntity dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.Set<TEntity>()
                    .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                return fromDb;
            }
            else
            {
                var fromDb = await _db.Set<TEntity>()
                    .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower())
                    && x.Id != id);
                return fromDb;
            }
        }

        public async Task<TEntity> Update(TEntity dto)
        {
            var fromDb = await _db.Set<TEntity>().FindAsync(dto.Id);
            fromDb.Name = dto.Name;
            var updated = _db.Set<TEntity>().Update(fromDb);
            await _db.SaveChangesAsync();
            return updated.Entity;
        }
    }
}