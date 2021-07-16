using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        public List<ProductsInventory> GetCurrentInventory();
        public ServiceResponse<ProductsInventory> UpdateUnitsAvailable(int id, int adjustment);
        public ProductsInventory GetProductsId(int productId);
        public void CreateSnapshot(ProductsInventory inventory);
        public List<ProductsInventorySnapshot> GetSnapshotHistory();
    }
}
