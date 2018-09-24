using System;
using System.Collections.Generic;
using System.Text;

namespace Brio.Domain.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public BrandViewModel Brand { get; set; }
    }
}
