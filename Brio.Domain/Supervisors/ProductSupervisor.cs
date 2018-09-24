using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Brio.Domain.ViewModel;

namespace Brio.Domain.Supervisors
{
    public class ProductSupervisor : IProductSupervisor
    {
        public Task<ProductViewModel> AddProductAsync(ProductViewModel newProductViewModel, CancellationToken ct = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAllProductAsync(CancellationToken ct = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetProductByBrandIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetProductByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(ProductViewModel productViewModel, CancellationToken ct = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
