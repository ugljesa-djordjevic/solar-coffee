using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Product
{
    public interface IProductService
    {
        List<Products> GetAllProducts();
        Products GetProductById(int id);
        ServiceResponse<Products> CreateProduct(Products products);
        ServiceResponse<Products> ArchiveProduct(int id);
    }
}
