using System.Xml.Serialization;

namespace BigShoeCopmany.Model
{
    public class OrderModel
    {
        [XmlAttribute("CustomerName")]
        public string CustomerName { get; set; }

        [XmlAttribute("CustomerEmail")]
        public string CustomerEmail { get; set; }

        [XmlAttribute("Quantity")]
        public int Quantity { get; set; }

        [XmlAttribute("Size")]
        public decimal Size { get; set; }

        [XmlAttribute("DateRequired")]
        public DateTime DateRequired { get; set; }

        [XmlAttribute("Notes")]
        public string Notes { get; set; }
    }
}