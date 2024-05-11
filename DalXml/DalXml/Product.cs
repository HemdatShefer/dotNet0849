using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalXml.xml.DalXml
{
    internal class Product : IProduct
    {
        public string filePath = "C:\\Users\\hemda\\OneDrive\\Desktop\\dotnet\\DalXml\\xml\\products.xml";
        const string s_products = "products"; //Linq to XML

        public IEnumerable<DO.Product> GetAll()
        {
            return GetAllFromXml(filePath);
        }

        public IEnumerable<DO.Product> GetAllFromXml(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            var products = from product in doc.Descendants("Product")
                           select new DO.Product
                           {
                               ID = (int)product.Element("ID")!,
                               Name = (string)product.Element("Name")!,
                               Categories = (Enums.Category)Enum.Parse(typeof(Enums.Category), (string)product.Element("Categories")!),
                               Price = (double)product.Element("Price")!,
                               InStock = (int)product.Element("InStock")!,
                               Path = (string)product.Element("Path")!
                           };
            return products;
        }

        public DO.Product getProduct(XElement productElement)
        {
            return new DO.Product
            {
                ID = (int)productElement.Element("ID")!,
                Name = (string)productElement.Element("Name")!,
                Categories = (Enums.Category)Enum.Parse(typeof(Enums.Category), (string)productElement.Element("Categories")!),
                Price = (double)productElement.Element("Price")!,
                InStock = (int)productElement.Element("InStock")!,
                Path = (string)productElement.Element("Path")!
            };
        }

        public DO.Product GetById(int id )
        {
            return GetById(id, filePath);
        }
        public DO.Product GetById(int id, string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            var product = (from p in doc.Descendants("Product")
                           where (int)p.Element("ID")! == id
                           select new DO.Product
                           {
                               ID = (int)p.Element("ID")!,
                               Name = (string)p.Element("Name")!,
                               Categories = (Enums.Category)Enum.Parse(typeof(Enums.Category), (string)p.Element("Categories")!),
                               Price = (double)p.Element("Price")!,
                               InStock = (int)p.Element("InStock")!,
                               Path = (string)p.Element("Path")!
                           }).FirstOrDefault();
            return product;
        }



        public int Add(DO.Product product)
        {
            return Add(product, filePath);
        }
        public int Add(DO.Product product, string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            var idExist = (from p in doc.Descendants("Product")
                           where (int)p.Element("ID") == product.ID
                           select p).FirstOrDefault();
            if (idExist != null)
            {
                throw new Exception("id already exist");
            }
            XElement newProduct = new XElement("Product",
                new XElement("ID", product.ID),
                new XElement("Name", product.Name),
                new XElement("Categories", product.Categories.ToString()),
                new XElement("Price", product.Price),
                new XElement("InStock", product.InStock),
                new XElement("Path", product.Path)
            );
            doc.Root!.Add(newProduct);
            doc.Save(filePath);
            return product.ID;
        }


        public void Delete(int id)
        {
            XElement studentsRootElem = XmlTools.LoadListFromXMLElement(s_products);

            (studentsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new Exception("missing id")).Remove();

            XmlTools.SaveListToXMLElement(studentsRootElem, s_products);
        }

        public void Update(DO.Product product)
        {
            Delete(product.ID);
            Add(product);
        }
    }
}
