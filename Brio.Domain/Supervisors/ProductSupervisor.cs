using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Brio.Domain.Converters;
using Brio.Domain.Entities;
using Brio.Domain.Repositories;
using Brio.Domain.ViewModel;

namespace Brio.Domain.Supervisors
{
    public class ProductSupervisor : IProductSupervisor
    {
        public readonly IProductRepository _repository;
        public readonly IBrandRepository _brandRepository;
        public ProductSupervisor(IProductRepository repository, IBrandRepository brandRepository)
        {
            _repository = repository;
            _brandRepository = brandRepository;
        }

        public async Task<ProductViewModel> AddProductAsync(ProductViewModel newProductViewModel, CancellationToken ct = default(CancellationToken))
        {
            var product = new Product
            {
                Name = newProductViewModel.Name,
                Price = newProductViewModel.Price,
                BrandId = newProductViewModel.BrandId
            };

            product = await _repository.AddAsync(product, ct);
            newProductViewModel.BrandId = product.BrandId;
            return newProductViewModel;
        }

        public async Task<bool> DeleteProductAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _repository.DeleteAsync(id, ct);
        }

        public async Task<List<ProductViewModel>> GetAllProductAsync(CancellationToken ct = default(CancellationToken))
        {
            var products = await _repository.GetAllAsync(ct);
            var productsVM = ProductConverter.ConvertList(products);
            foreach (var product in productsVM)
            {
                product.BrandName = product.Brand.Name;
            }
            return productsVM;
        }

        public async Task<List<ProductViewModel>> GetProductByBrandIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var products = await _repository.GetByBrandIdAsync(id, ct);
            return ProductConverter.ConvertList(products);
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var product = await _repository.GetByIdAsync(id, ct);
            if (product == null) { return null; }
            var productVM = ProductConverter.Convert(product);
            productVM.BrandName = product.Brand.Name;
            return productVM;
        }

        public async Task<bool> UpdateProductAsync(ProductViewModel productViewModel, CancellationToken ct = default(CancellationToken))
        {
            var product = await _repository.GetByIdAsync(productViewModel.ProductId, ct);

            if (product == null) return false;
            product.ProductId = productViewModel.ProductId;
            product.Name = productViewModel.Name;
            product.Price = productViewModel.Price;
            product.BrandId = productViewModel.BrandId;

            return await _repository.UpdateAsync(product, ct);
        }
    }
}
