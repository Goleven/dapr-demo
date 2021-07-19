using System;

namespace ProductService.Dto
{
    [Serializable]
    public class ProductDto
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string SupplerName { get; set; }
    }
}