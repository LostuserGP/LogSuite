using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;

namespace LogSuite.Business.Repositories;

public interface IRepositoryBase<T>
{
    public Task<T> Create(T dto);
    public Task<T> Update(T dto);
    public Task<T> Get(int id);
    public Task<int> Delete(int id);
    public Task<IEnumerable<T>> GetAll();
    public Task<T> IsUnique(T dto, int id = 0);
    public Task<PagedList<T>> GetPaged(Params parameters);
}