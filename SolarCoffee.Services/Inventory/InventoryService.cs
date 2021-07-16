using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(SolarDbContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Create Snapshot record using the provided ProductInventory instance
        /// </summary>
        /// <param name="inventory"></param>
        public void CreateSnapshot(ProductsInventory inventory)
        {
            var now = DateTime.UtcNow;
            var snapshot = new ProductsInventorySnapshot
            {
                SnapshootTime = now,
                Products = inventory.Products,
                QuantityOnHand = inventory.QuantityOnHand
            };

            _db.Add(snapshot);
        }

        /// <summary>
        /// Returns all current inventory from the database
        /// </summary>
        /// <returns></returns>
        public List<ProductsInventory> GetCurrentInventory()
        {
            return _db.ProductsInventories
                .Include(pi => pi.Products)
                .Where(pi => !pi.Products.IsArchived)
                .ToList();
        }

        /// <summary>
        /// Gets a ProductInventory instance by Product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductsInventory GetProductsId(int productId)
        {
            return _db.ProductsInventories
                .Include(pi => pi.Products)
                .FirstOrDefault(pi => pi.Products.Id == productId);
        }

        /// <summary>
        /// Return Snapshot history for the previous 6 hours
        /// </summary>
        /// <returns></returns>
        public List<ProductsInventorySnapshot> GetSnapshotHistory() 
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);
            return _db.ProductsInventorySnapshots
                .Include(snap => snap.Products)
                .Where(snap => snap.SnapshootTime > earliest && !snap.Products.IsArchived)
                .ToList();
        }

        /// <summary>
        /// Updates number of units available of the provided product id
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added / removed from inventory</param>
        /// <returns></returns>
        public ServiceResponse<ProductsInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            var now = DateTime.UtcNow;
            try
            {
                var inventory = _db.ProductsInventories
                    .Include(inv => inv.Products)
                    .First(inv => inv.Products.Id == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot(inventory);
                }

                catch(Exception ex)
                {
                    _logger.LogError("Error creating inventory snapshot");
                }

                _db.SaveChanges();

                return new ServiceResponse<ProductsInventory>
                {
                    IsSuccess = true,
                    Message = $"Product {id} inventory adjusted",
                    Time = now,
                    Data = inventory
                };
            }

            catch(Exception ex)
            {
                return new ServiceResponse<ProductsInventory>
                {
                    IsSuccess = false,
                    Message = ex.StackTrace,
                    Time = now,
                    Data = null
                };
            }
        }
    }
}
