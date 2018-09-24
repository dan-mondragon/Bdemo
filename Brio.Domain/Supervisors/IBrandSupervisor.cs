using Brio.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brio.Domain.Supervisors
{
    public interface IBrandSupervisor
    {
        Task<List<BrandViewModel>> GetAllBrandAsync(CancellationToken ct = default(CancellationToken));
        Task<BrandViewModel> GetBrandByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<BrandViewModel> AddBrandAsync(BrandViewModel newBrandViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateBrandAsync(BrandViewModel brandtViewModel, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteBrandAsync(int id, CancellationToken ct = default(CancellationToken));
    }
}
