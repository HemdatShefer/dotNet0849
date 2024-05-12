using Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

//short implementation with XMLTools functions
internal class DalProduct : IProduct
{
    string path = @"products.xml";
    string configPath = @"..\config.xml";
    string dir = @"..\xml\";
    private string dir_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "xml");


    XElement productRoot;

    public DalProduct()
    {
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            path = Path.Combine(dir_path, "products.xml");
            if (!Directory.Exists(dir_path))
                Directory.CreateDirectory(dir_path);

            productRoot = XElement.Load(path);

        }
        catch (Exception ex)
        {
            throw new Exception("product File upload problem" + ex.Message);
        }
    }

    public int Add(Product product)
    {

        List<Product> ordrList = XmlTools.LoadListFromXMLSerializer<Product>(path);

        if (ordrList.Exists(x => x.ID == product.ID))
            throw new ObjectNotFoundException("product");

        XElement Id = new XElement("ID", product.ID);
        XElement name = new XElement("Name", product.Name);
        XElement category = new XElement("Categories", product.Categories);
        XElement inStock = new XElement("InStock", product.InStock);
        XElement price = new XElement("Price", product.Price);
        XElement newProduct = new XElement("Product", Id, name, category, price, inStock);

        productRoot.Add(newProduct);
        productRoot.Save(dir + path);

        return product.ID;
    }

    public void Delete(int id)
    {
        var newList = XmlTools.LoadListFromXMLSerializer<Product>(path);
        int index = newList.FindIndex(x => x.ID == id);
        if (index == -1)
            throw new ObjectNotFoundException("the order is't exsit\n");

        newList.RemoveAt(index);
        XmlTools.SaveListToXMLSerializer(newList, path);
    }

    public Product GetByCondition(Func<Product?, bool>? cond)
    {
        return (from item in XmlTools.LoadListFromXMLSerializer<Product>(path)
                where cond(item)
                select item).FirstOrDefault();
    }

    public IEnumerable<Product> GetAll()
    {
        List<DO.Product> prodList = XmlTools.LoadListFromXMLSerializer<DO.Product>(path);


        return prodList.Select(p => p);

    }

    public Product GetById(int id)
    {
        return (from item in XmlTools.LoadListFromXMLSerializer<Product>(path)
                where item.ID == id
                select item).FirstOrDefault();
    }

    public void Update(Product Pr)
    {

        var newList = XmlTools.LoadListFromXMLSerializer<Product>(path);
        int index = newList.FindIndex(x => x.ID == Pr.ID);
        if (index == -1)
            throw new ObjectNotFoundException("the order is't exsit\n");

        newList[index] = Pr;
        XmlTools.SaveListToXMLSerializer(newList, path);
    }
}
