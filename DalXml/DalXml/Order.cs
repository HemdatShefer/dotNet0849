using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalXml.xml.DalXml
{
    internal class Order : IOrder
    {
        public string filePath = "C:\\Users\\hemda\\OneDrive\\Desktop\\dotnet\\DalXml\\xml\\orders.xml";
        const string s_orders = "orders"; //Linq to XML

        public int Add(DO.Order order)
        {
            return AddOrderToXml(order, filePath);
        }
        public IEnumerable<DO.Order> GetAll()
        {
            return GetOrdersFromXml(filePath);
        }
        public DO.Order GetById(int id)
        {
            return GetOrder(id, filePath);
        }

        public void Delete(int id)
        {
            XElement studentsRootElem = XmlTools.LoadListFromXMLElement(s_orders);

            (studentsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new Exception("missing id")).Remove();

            XmlTools.SaveListToXMLElement(studentsRootElem, s_orders);
        }

        public void Update(DO.Order order)
        {
            Delete(order.ID);
            Add(order);
        }

        public IEnumerable<DO.Order> GetOrdersFromXml(string FilePath)
        {
            var xdoc = XDocument.Load(FilePath);
            IEnumerable<DO.Order> orders = from order in xdoc.Descendants("Order")
                         select new DO.Order
                         {
                             ID = int.Parse(order.Element("ID")!.Value),
                             CustomerName = order.Element("CustomerName")!.Value,
                             CustomerEmail = order.Element("CustomerEmail")!.Value,
                             CustomerAddress = order.Element("CustomerAddress")!.Value,
                             OrderDate = DateTime.Parse(order.Element("OrderDate")!.Value),
                             ShipDate = DateTime.Parse(order.Element("ShipDate")!.Value),
                             DeliveryDate = DateTime.Parse(order.Element("DeliveryDate")!.Value)
                         };
            return orders;
        }

        public DO.Order GetOrder(int id, string FilePath)
        {
            var xdoc = XDocument.Load(FilePath);
            return xdoc.Descendants("Order")
                .Where(o => (int)o.Element("ID")! == id)
                .Select(o => new DO.Order
                {
                    ID = (int)o.Element("ID")!,
                    CustomerName = (string)o.Element("CustomerName")!,
                    CustomerEmail = (string)o.Element("CustomerEmail")!,
                    CustomerAddress = (string)o.Element("CustomerAddress")!,
                    OrderDate = DateTime.Parse(o.Element("OrderDate")!.Value)!,
                    ShipDate = DateTime.Parse(o.Element("ShipDate")!.Value)!,
                    DeliveryDate = DateTime.Parse(o.Element("DeliveryDate")!.Value)
                })
                .FirstOrDefault();

                throw new Exception($"Order with id {id} not found.");

        }


        public static int AddOrderToXml(DO.Order order, string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            var idExist = (from p in doc.Descendants("Order")
                           where (int)p.Element("ID")! == order.ID
                           select p).FirstOrDefault();
            if (idExist != null)
            {
                throw new Exception("id already exist");
            }
            XElement newOrder = new XElement("Order",
                new XElement("ID", order.ID),
                new XElement("CustomerName", order.CustomerName),
                new XElement("CustomerEmail", order.CustomerEmail),
                new XElement("CustomerAddress", order.CustomerAddress),
                new XElement("OrderDate", order.OrderDate),
                new XElement("ShipDate", order.ShipDate),
                new XElement("DeliveryDate", order.DeliveryDate)
            );
            doc.Root.Add(newOrder);
            return order.ID;
        }
    }
}
