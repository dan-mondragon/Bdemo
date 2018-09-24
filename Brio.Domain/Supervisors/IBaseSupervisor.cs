using Brio.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brio.Domain.Supervisors
{
    public interface IBaseSupervisor
    {
        Task<List<BrandViewModel>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<BrandViewModel> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<BrandViewModel> AddAsync(BrandViewModel newBrandViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(BrandViewModel brandtViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));
    }
}
