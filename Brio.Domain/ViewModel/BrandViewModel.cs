using System;
using System.Collections.Generic;
using System.Text;

namespace Brio.Domain.ViewModel
{
    public class BrandViewModel
    {
        public int BrandId { get; set; }
        public string Name { get; set; }

        public IList<ProductViewModel> Products { get; set; }
    }
}
