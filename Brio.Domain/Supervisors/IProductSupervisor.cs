using Brio.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brio.Domain.Supervisors
{
    public interface IProductSupervisor
    {
        Task<List<ProductViewModel>> GetAllProductAsync(CancellationToken ct = default(CancellationToken));
        Task<ProductViewModel> GetProductByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<List<ProductViewModel>> GetProductByBrandIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<ProductViewModel> AddProductAsync(ProductViewModel newProductViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateProductAsync(ProductViewModel productViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteProductAsync(int id, CancellationToken ct = default(CancellationToken));
    }
}
