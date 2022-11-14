using System.Xml.Serialization;

namespace BigShoeCopmany.Model
{
    public class BigShoeDataImport
    {
        [XmlElement(ElementName = "Order")]
        public List<OrderModel> OrderModel { get; set; }
    }
}
