using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product
    {
        public readonly string Id;
        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Product Description")]
        [Range(0,20)]
        public string Description { get; set; }
        [Required]
        [DisplayName("Product Price")]
        public string Price { get; set; }
        [Required]
        [DisplayName("Product Category")]
        public string Category { get; set; }
        
        public string Image { get; set; }

        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
