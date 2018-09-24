using Brio.Domain.Entities;
using Brio.Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Brio.Domain.Converters
{
    public class BrandConverter
    {
        public static BrandViewModel Convert(Brand brand)
        {
            var brandViewModel = new BrandViewModel();
            brandViewModel.BrandId = brand.BrandId;
            brandViewModel.Name = brand.Name;
            return brandViewModel;
        }

        public static List<BrandViewModel> ConvertList(IEnumerable<Brand> brands)
        {
            return brands.Select(b =>
            {
                var model = new BrandViewModel();
                model.BrandId = b.BrandId;
                model.Name = b.Name;
                return model;
            }).ToList();
        }
    }
}
