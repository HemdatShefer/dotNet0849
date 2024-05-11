namespace DalApi;

using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

static class DalConfig
{
    internal static string? s_dalName;
    internal static Dictionary<string, string> s_dalPackages;

    static DalConfig()
    {
        // Load the DAL configuration XML file.
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml")
            ?? throw new DO.DalConfigException("dal-config.xml file is not found");

        // Retrieve the DAL type from the <dal> element.
        s_dalName = dalConfig?.Element("dal")?.Value
            ?? throw new DO.DalConfigException("<dal> element is missing");

        // Retrieve and parse the <dal-packages> elements into a dictionary.
        var packages = dalConfig?.Element("dal-packages")?.Elements()
            ?? throw new DO.DalConfigException("<dal-packages> element is missing");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value);
    }
}



