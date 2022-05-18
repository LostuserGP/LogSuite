using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;

namespace LogSuite.Business.Repositories;

public interface IRepositoryValueBase<T>
{
    public Task<T> Create(T dto);
    public Task<T> Update(T dto);
    public Task<T> Get(long id);
    public Task<int> Delete(long id);
    public Task<IEnumerable<T>> GetAll();
    public Task<T> IsUnique(T dto, long id = 0);
    public Task<PagedList<T>> GetPaged(Params parameters);
}