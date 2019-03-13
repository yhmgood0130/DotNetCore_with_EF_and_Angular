using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        Order GetOrderById(int id);
        IEnumerable<Order> GetAllOrders();
        bool SaveAll();
        void AddEntity(object model);
    }
}