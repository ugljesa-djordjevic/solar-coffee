using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data.Models
{
    public class ProductsInventorySnapshot
    {
        public int Id { get; set; }
        public DateTime SnapshootTime { get; set; }
        public int QuantityOnHand { get; set; }
        public Products Products { get; set; }
    }
}
