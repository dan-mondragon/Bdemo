using Brio.Domain.Entities;
using Brio.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brio.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BrioContext _context;

        public ProductRepository(BrioContext context)
        {
            _context = context;
        }

        private async Task<bool> ProductExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public async Task<Product> AddAsync(Product newProduct, CancellationToken ct = default(CancellationToken))
        {
            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync(ct);
            return newProduct;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await ProductExists(id, ct))
                return false;
            var toRemove = _context.Product.Find(id);
            _context.Product.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Product.Include(p => p.Brand).ToListAsync();
        }

        public async Task<List<Product>> GetByBrandIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Product.Where(p => p.BrandId.Equals(id)).ToListAsync(ct);
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Product.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Product product, CancellationToken ct = default(CancellationToken))
        {
            if (!await ProductExists(product.ProductId))
                return false;

            _context.Product.Update(product);
            _context.Update(product);
            await _context.SaveChangesAsync(ct);

            return true;

        }
    }
}
