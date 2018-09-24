using Brio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brio.Domain.Repositories
{
    public interface IBrandRepository : IDisposable
    {
        Task<List<Brand>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<Brand> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<Brand> AddAsync(Brand newBrand, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(Brand brand, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));
    }
}
