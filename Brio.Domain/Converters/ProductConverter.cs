using Brio.Domain.Entities;
using Brio.Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Brio.Domain.Converters
{
    public static class ProductConverter
    {
        public static ProductViewModel Convert(Product product)
        {
            var productViewModel = new ProductViewModel();
            productViewModel.ProductId = product.ProductId;
            productViewModel.BrandId = product.BrandId;
            productViewModel.Name = product.Name;
            productViewModel.Price = product.Price;

            return productViewModel;
        }

        public static List<ProductViewModel> ConvertList(IEnumerable<Product> products)
        {
            return products.Select(p =>
            {
                var model = new ProductViewModel();
                model.ProductId = p.ProductId;
                model.BrandId = p.BrandId;
                model.Name = p.Name;
                model.Price = p.Price;
                return model;
            }).ToList();
        }
    }
}
