using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Inventory
{
    public interface InventoryService
    {
        public List<ProductsInventory> GetCurrentInventory();
        public ServiceResponse<ProductsInventory> UpdateUnitsAvailable(int id, int adjustment);
        public ProductsInventory GetProductId(int productId);
        public void CreateSnapshot();
        public List<ProductsInventorySnapshot> GetSnapshotHistory();
    }
}
