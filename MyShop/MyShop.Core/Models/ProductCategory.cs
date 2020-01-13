using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory
    {
        public readonly string Id;
        public string Category { get; set; }

        public ProductCategory()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
