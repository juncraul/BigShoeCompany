using BigShoeCompany.Service.Contracts;
using BigShoeCopmany.Model;
using System.Xml.Serialization;

namespace BigShoeCompany.Service
{
    public class ProcessorService : IProcessorService
    {
        public async Task<List<OrderModel>> ProcessOrderFileAsync(Stream stream)
        {
            try
            {
                stream.Position = 0;
                XmlSerializer serializer = new XmlSerializer(typeof(BigShoeDataImport));
                BigShoeDataImport bigShoeDataImport = (BigShoeDataImport)serializer.Deserialize(stream);
                return bigShoeDataImport.OrderModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to Deserialize the List of Orders", ex);
            }

        }
    }
}
