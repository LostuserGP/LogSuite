using RiskSuite.Client.Helpers;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Client.Services.IServices
{
    public interface IReferenceService
    {
        public Task<IEnumerable<ReferenceName>> Getall();
        public Task<ReferenceName> Get(int id);
        public Task<ReferenceName> Create(ReferenceName dto);
        public Task<PagingResponse<ReferenceName>> Getall(Params parameters);
        Task<ReferenceName> Update(ReferenceName dto);
        Task<bool> Delete(int id);
    }
}
