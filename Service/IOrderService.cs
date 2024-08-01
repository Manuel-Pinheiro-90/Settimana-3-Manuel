using Settimana_3_Manuel.Models;

namespace Settimana_3_Manuel.Service
{
    public interface IOrderService
    {
        Task AddToOrder(int userId, int productId, int quantity, string address, string extraNote);
        Task<IEnumerable<Order>> GetUserOrders( int userId);
        Task<IEnumerable<Order>> GetAllOrders();
        Task ProcessOrder(int OrderId);
        Task DeleteOrder(int orderId);

        Task<int> GetTotalProcessedOrders();
        Task<decimal> GetTotalEarningsForDay(DateTime date);



    }
}
