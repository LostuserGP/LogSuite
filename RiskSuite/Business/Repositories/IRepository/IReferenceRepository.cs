using RiskSuite.Business;
using RiskSuite.DataAccess;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IReferenceRepository<TEntity> where TEntity : IReferenceName
    {
        public Task<TEntity> Create(TEntity dto);
        public Task<TEntity> Update(TEntity dto);
        public Task<TEntity> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<TEntity> IsUnique(TEntity dto, int id = 0);
    }
}
