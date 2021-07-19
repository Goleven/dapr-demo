using System;

namespace OrderService.Dto
{
    public class OrderDto
    {
        public Guid OrderID { get; set; }

        public string Address { get; set; }

        public DateTime CreateTime { get; set; }

        public int ProductCount { get; set; }
    }
}