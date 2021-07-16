using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Services.Order
{
    public interface IOrderService
    {
        public List<SalesOrder> GetOrders();
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order);
        public ServiceResponse<bool> MarkFulfilled(int id);

    }
}
