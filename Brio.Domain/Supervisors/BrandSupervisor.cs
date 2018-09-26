using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Brio.Domain.Converters;
using Brio.Domain.Entities;
using Brio.Domain.Repositories;
using Brio.Domain.Utils;
using Brio.Domain.ViewModel;
using Brio.Domain.Extensions;

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

        public async Task<Tuple<List<BrandViewModel>, PagedResult<Brand>>> GetAllBrandAsync(PagingParameter pagingParameter, CancellationToken ct = default(CancellationToken))
        {
            var source = _repository.GetAllAsync(ct).GetPaged(pagingParameter);
            var brands = BrandConverter.ConvertList(source.Results);
            foreach (var brand in brands)
            {
                brand.Products = ProductConverter.ConvertList(await _productRepository.GetByBrandIdAsync(brand.BrandId, ct));
            }
            source.Results = null;
            return new Tuple<List<BrandViewModel>, PagedResult<Brand>>(brands, source);
        }

        public async Task<BrandViewModel> GetBrandByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var brand = await _repository.GetByIdAsync(id, ct);
            if (brand == null) { return null; }
            var brandVM = BrandConverter.Convert(brand);
            brandVM.Products = ProductConverter.ConvertList(await _productRepository.GetByBrandIdAsync(brandVM.BrandId, ct));
            return brandVM;
        }

        public async Task<bool> UpdateBrandAsync(BrandViewModel brandtViewModel, CancellationToken ct = default(CancellationToken))
        {
            var brand = await _repository.GetByIdAsync(brandtViewModel.BrandId, ct);

            if (brand == null) return false;
            brand.BrandId = brandtViewModel.BrandId;
            brand.Name = brandtViewModel.Name;

            return await _repository.UpdateAsync(brand, ct);
        }

        public (object, List<Brand>) paginate(IQueryable<Brand> source, PagingParameter pagingParameter)
        {
            int count = source.Count();
            int CurrentPage = pagingParameter.pageNumber;
            int PageSize = pagingParameter.pageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            return (paginationMetadata, items);
        }
    }
}
