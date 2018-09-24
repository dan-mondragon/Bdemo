using System;
using System.Collections.Generic;
using System.Text;

namespace Brio.Domain.Entities
{
    public sealed class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
