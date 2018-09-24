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
    public class BrandSupervisor : IBrandSupervisor
    {
        public readonly IBrandRepository _repository;
        public readonly IProductRepository _productRepository;

        public BrandSupervisor(IBrandRepository repository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public async Task<BrandViewModel> AddBrandAsync(BrandViewModel newBrandViewModel, CancellationToken ct = default(CancellationToken))
        {
            var brand = new Brand
            {
                Name = newBrandViewModel.Name
            };

            brand = await _repository.AddAsync(brand, ct);
            newBrandViewModel.BrandId = brand.BrandId;
            return newBrandViewModel;
        }

        public async Task<bool> DeleteBrandAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _repository.DeleteAsync(id, ct);
        }

        public async Task<List<BrandViewModel>> GetAllBrandAsync(CancellationToken ct = default(CancellationToken))
        {
            var brands = BrandConverter.ConvertList(await _repository.GetAllAsync(ct));
            foreach (var brand in brands)
            {
                brand.Products = ProductConverter.ConvertList(await _productRepository.GetByBrandIdAsync(brand.BrandId, ct));
            }
            return brands;
        }

        public async Task<BrandViewModel> GetBrandByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var brands = BrandConverter.Convert(await _repository.GetByIdAsync(id, ct));
            brands.Products = ProductConverter.ConvertList(await _productRepository.GetByBrandIdAsync(brands.BrandId, ct));
            return brands;
        }

        public async Task<bool> UpdateBrandAsync(BrandViewModel brandtViewModel, CancellationToken ct = default(CancellationToken))
        {
            var brand = await _repository.GetByIdAsync(brandtViewModel.BrandId, ct);

            if (brand == null) return false;
            brand.BrandId = brandtViewModel.BrandId;
            brand.Name = brandtViewModel.Name;

            return await _repository.UpdateAsync(brand, ct);
        }
    }
}
