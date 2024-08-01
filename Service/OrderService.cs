using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Settimana_3_Manuel.Context;
using Settimana_3_Manuel.Models;

namespace Settimana_3_Manuel.Service
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }


        // ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task AddToOrder(int userId, int productId, int quantity, string address, string extraNote)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);
            var product = await _context.Products.FindAsync(productId);
            if (user == null || product == null)
                throw new Exception("User o prodotto non trovati");

            var order = await _context.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.User.Id == userId && !o.Processed);
            if (string.IsNullOrEmpty(extraNote))
            {
                extraNote = "Nessuna nota aggiuntiva";
            
            }



            if (order == null)
            {
                order = new Order
                {
                    User = user,
                    Date = DateTime.Now,
                    Address = address,
                    ExtraNote = extraNote,
                    OrderProducts = new List<OrderProduct>()
                };
                _context.Orders.Add(order);
            }

            var orderProduct = order.OrderProducts.FirstOrDefault(op => op.Product.Id == productId);
            if (orderProduct == null)
            {
                orderProduct = new OrderProduct { Order = order, Product = product, Quantity = quantity };
                order.OrderProducts.Add(orderProduct);
            }
            else
            {
                orderProduct.Quantity += quantity;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetUserOrders(int userId)
        {
            return await _context.Orders.Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                                        .Where(o => o.User.Id == userId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                                        .Include(o => o.User).ToListAsync();
        }

     
       

        public async Task ProcessOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                throw new Exception("Ordine non trovato");

            order.Processed = true;
            await _context.SaveChangesAsync();
        }


        public async Task DeleteOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<int> GetTotalProcessedOrders()
        {
            return await _context.Orders.CountAsync(o => o.Processed);
        }


        public async Task<decimal> GetTotalEarningsForDay()
        {
            return await _context.Orders
                .Where(o => o.Processed && o.Date.Date == DateTime.Now.Date)
                .SelectMany(o => o.OrderProducts)
                .SumAsync(op => op.Product.Price * op.Quantity);
        }



















    }








}

