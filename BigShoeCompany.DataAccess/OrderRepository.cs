using BigShoeCopmany.Model;
using Microsoft.EntityFrameworkCore;

namespace BigShoeCompany.DataAccess
{
    public class OrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddNewOrderAsync(OrderModel orderModel)
        {
            try
            {
                var order = new Order()
                {
                    CustomerEmail = orderModel.CustomerEmail,
                    CustomerName = orderModel.CustomerName,
                    DateRequired = orderModel.DateRequired,
                    Notes = orderModel.Notes,
                    Quantity = orderModel.Quantity,
                    Size = orderModel.Size
                };
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return order.Id;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed adding entity in db", ex);
            }
        }

        public async Task<List<OrderModel>> GetAllOrdersAsync()
        {
            try
            {
                return await _context.Orders.Select(a => new OrderModel
                {
                    CustomerEmail = a.CustomerEmail,
                    CustomerName = a.CustomerName,
                    DateRequired = a.DateRequired,
                    Notes = a.Notes,
                    Quantity = a.Quantity,
                    Size = a.Size
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed retrieving entities from db", ex);
            }
        }
    }
}
