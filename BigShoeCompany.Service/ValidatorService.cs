using BigShoeCompany.Service.Contracts;
using BigShoeCopmany.Model;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace BigShoeCompany.Service
{
    public class ValidatorService : IValidatorService
    {
        public async Task<bool> IsOrderFileValidAsync(Stream stream)
        {
            stream.Position = 0;
            var path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)).LocalPath;
            XmlSchemaSet schema = new();
            schema.Add("", path + "\\XsdValidators\\OrderImport.xsd");
            XmlReader rd = XmlReader.Create(stream);
            XDocument doc = XDocument.Load(rd);
            doc.Validate(schema, ValidationEventHandler);

            return true;
        }

        public bool IsValidOrders(List<OrderModel> orders)
        {
            foreach (var order in orders)
            {
                if (string.IsNullOrEmpty(order.CustomerName)) 
                    throw new Exception("Customer Name must be provided");

                if (string.IsNullOrEmpty(order.CustomerEmail)) 
                    throw new Exception("Customer Email must be provided");

                if (DateTime.Now < order.DateRequired &&
                    order.DateRequired < DateTime.Now.AddDays(10)) 
                    throw new Exception("Date must be valid and at least 10 working days into the future");

                if (11.5m < order.Size &&
                    order.Size < 15m) 
                    throw new Exception("Size must be 11.5 to 15 including half sizes");

                if (order.Quantity % 1000 != 0) 
                    throw new Exception("Quantity must be in multiples of 1000");
            }
            return true;
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (Enum.TryParse("Error", out XmlSeverityType type))
            {
                if (type == XmlSeverityType.Error) throw new Exception(e.Message);
            }
        }
    }
}
