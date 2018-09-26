using Brio.Domain.Entities;
using Brio.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Brio.Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly BrioContext _context;

        public BrandRepository(BrioContext context)
        {
            _context = context;
        }

        private async Task<bool>BrandExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public async Task<Brand> AddAsync(Brand newBrand, CancellationToken ct = default(CancellationToken))
        {
            _context.Brand.Add(newBrand);
            await _context.SaveChangesAsync(ct);
            return newBrand;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await BrandExists(id, ct))
                return false;
            var toRemove = _context.Brand.Find(id);
            _context.Brand.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IQueryable<Brand> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {            
            return (from b in _context.Brand select b).AsQueryable();
        }

        public async Task<Brand> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Brand.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Brand brand, CancellationToken ct = default(CancellationToken))
        {
            if (!await BrandExists(brand.BrandId, ct))
                return false;
            _context.Brand.Update(brand);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
