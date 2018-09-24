using Brio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brio.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<Product> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<List<Product>> GetByBrandIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<Product> AddAsync(Product newProduct, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(Product product, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));
    }
}
