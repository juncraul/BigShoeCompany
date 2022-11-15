using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace BigShoeCompany.DataAccess
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int Quantity { get; set; }
        [Precision(10, 2)]
        public decimal Size { get; set; }
        public DateTime DateRequired { get; set; }
        public string Notes { get; set; }
    }
}
