﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dal;

/// <summary>
/// Provides static methods for serializing and deserializing lists to and from XML files.
/// Used to save and load persistent data, encapsulating XML handling complexities.
/// </summary>
public class XmlTools
{

    public static string dir = @"..\xml\";// Directory for storing XML files.
    static XmlTools()
    {
        // Ensure the directory for XML files exists, creating it if necessary.
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }


    #region SaveLoadWithXMLSerializer
    public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
    {
        try
        {
            FileStream file = new FileStream(dir + filePath, FileMode.OpenOrCreate);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
        }
    }
    public static List<T> LoadListFromXMLSerializer<T>(string filePath)
    {
        try
        {
            if (File.Exists(dir + filePath))
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream file = new FileStream(dir + filePath, FileMode.Open);
                list = (List<T>)x.Deserialize(file);
                file.Close();
                return list;
            }
            else
                return new List<T>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message); // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
        }
    }
    #endregion
}
