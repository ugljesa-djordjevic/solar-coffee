using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext _db;
        public ProductService(SolarDbContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Archives a Product by setting boolean IsArchived to true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<Products> ArchiveProduct(int id)
        {
            try
            {
                var product = _db.Products.Find(id);
                product.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<Products>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Archived product",
                    IsSuccess = true
                };
            }

            catch (Exception ex)
            {
                return new ServiceResponse<Products>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = ex.StackTrace,
                    IsSuccess = false
                };
            }
        }

        /// <summary>
        /// Adds new product to the database
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public ServiceResponse<Products> CreateProduct(Products products)
        {
            try
            {
                _db.Products.Add(products);

                var newInventory = new ProductsInventory
                {
                    Products = products,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                _db.ProductsInventories.Add(newInventory);
                _db.SaveChanges();

                return new ServiceResponse<Products>
                {
                    Data = products,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Products>
                {
                    Data = products,
                    Time = DateTime.UtcNow,
                    Message = ex.StackTrace,
                    IsSuccess = false
                };
            }
            
        }

        /// <summary>
        /// Retrieves all Products from the database
        /// </summary>
        /// <returns></returns>
        public List<Products> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        /// <summary>
        /// Retrieves a Product by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Products GetProductById(int id)
        {
            return _db.Products.Find(id);
        }
    }
}
