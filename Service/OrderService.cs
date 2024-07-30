using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Settimana_3_Manuel.Context;
using Settimana_3_Manuel.Models;

namespace Settimana_3_Manuel.Service
{
    public class OrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllProcessedOrders()
        {
            return await _context.Orders.Where(o => o.Processed).ToListAsync();
        }

        public async Task<int> GetTotalOrdersForDay()
        {
            return await _context.Orders.CountAsync(o => o.Date.Date == DateTime.Now.Date);
        }

        public async Task<decimal> GetTotalIncomeForDay()
        {
            return await _context.Orders.Where(o => o.Date.Date == DateTime.Now.Date)
                                        .SumAsync(o => o.OrderProducts.Sum(op => op.Product.Price * op.Quantity));
        }
    }
}
