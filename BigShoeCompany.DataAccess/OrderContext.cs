using Microsoft.EntityFrameworkCore;

namespace BigShoeCompany.DataAccess
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=BigShoeCompany;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}