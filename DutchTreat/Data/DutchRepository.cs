using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            this._ctx = ctx;
            this._logger = logger;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
            }
            return _ctx.Products
                .OrderBy(p => p.Title)
                .ToList();
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                .Where(p => p.Category == category)
                .ToList();
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders
                .Where(o => o.Id == id)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault();
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }
    }
}
