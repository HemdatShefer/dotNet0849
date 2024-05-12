using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;
internal class DalOrderItem : IOrderItem
{
    string path = @"ordersItems.xml";
    string configPath = @"config.xml";
    string dir = @"..\xml\";

    XElement ordersItemsRoot;

    public DalOrderItem()
    {
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            if (File.Exists(dir + path))
                ordersItemsRoot = XElement.Load(dir + path);
            else
            {
                ordersItemsRoot = new XElement("ordersItems");
                ordersItemsRoot.Save(dir + path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("product File upload problem" + ex.Message);
        }
    }

    public int Add(OrderItem Or)
    {
        XElement configRoot = XElement.Load(dir + configPath);
        var v = configRoot.Element("orderItemSeq");
        int nextSeqNum = Convert.ToInt32(configRoot.Element("orderItemSeq")!.Value);
        nextSeqNum++;
        Or.ID = nextSeqNum;
        //update config file
        configRoot.Element("orderItemSeq")!.SetValue(nextSeqNum);
        configRoot.Save(dir + configPath);

        List<OrderItem> orderItemList = XmlTools.LoadListFromXMLSerializer<OrderItem>(path);

        if (orderItemList.Exists(x => x.ID == Or.ID))
            throw new ObjectNotFoundException("OrderItem");

        XElement Id = new XElement("Id", Or.ID);
        XElement productID = new XElement("productID", Or.ProductID);
        XElement orderID = new XElement("orderID", Or.OrderID);
        XElement price = new XElement("price", Or.Price);
        XElement amount = new XElement("amount", Or.Amount);


        ordersItemsRoot.Add(new XElement("OrderItem", Id, productID, orderID, price, amount));
        ordersItemsRoot.Save(dir + path);

        return Or.ID;
    }

    public void Delete(int id)
    {
        List<OrderItem> prodLst = XmlTools.LoadListFromXMLSerializer<OrderItem>(path);
        int index = prodLst.FindIndex(x => x.ID== id);
        if (index == -1)
            throw new ObjectNotFoundException("OrderItem");

        prodLst.RemoveAt(index);

        XmlTools.SaveListToXMLSerializer(prodLst, path);
    }

    public OrderItem GetByCondition(Func<OrderItem?, bool>? cond)
    {

        return (from item in XmlTools.LoadListFromXMLSerializer<OrderItem>(path)
                where cond(item)
                select item).FirstOrDefault();
    }

    public IEnumerable<OrderItem> GetAll()
    {
        List<DO.OrderItem> prodList = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(path);

            return prodList.Select(p => p);

    }

    public OrderItem GetById(int id)
    {
        List<OrderItem> prodLst = XmlTools.LoadListFromXMLSerializer<OrderItem>(path);

        if (prodLst.Exists(x => x.ID == id))
            throw new ObjectNotFoundException("OrderItem");

        return (from item in XmlTools.LoadListFromXMLSerializer<OrderItem>(path)
                where item.ID == id
                select item).FirstOrDefault();
    }

    public void Update(OrderItem Or)
    {
        var newList = XmlTools.LoadListFromXMLSerializer<OrderItem>(path);
        int index = newList.FindIndex(x => x.ID == Or.ID);
        if (index == -1)
            throw new ObjectNotFoundException("the OrderItem is't exsit\n");

        newList[index] = Or;
        XmlTools.SaveListToXMLSerializer(newList, path);
    }
}
