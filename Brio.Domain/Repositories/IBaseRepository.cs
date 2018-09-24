using Brio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brio.Domain.Repositories
{
    public interface IBaseRepository : IDisposable
    {
        Task<List<Base>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<Base> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<Base> AddAsync(Base newBrand, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(Base brand, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));
        string TableContext();
    }
}
