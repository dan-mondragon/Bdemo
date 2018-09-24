using System;
using System.Collections.Generic;
using System.Text;

namespace Brio.Domain.Entities
{
    public sealed class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
