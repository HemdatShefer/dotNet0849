namespace DalApi;

using DO;
using System.Reflection;
using static DalApi.DalConfig;

/// <summary>
/// Provides a factory method to dynamically create instances of data access layer (DAL) based on configuration.
/// </summary>
public static class Factory
{
    /// <summary>
    /// Dynamically gets an instance of the configured DAL type.
    /// </summary>
    /// <returns>An instance of IDal if successful; otherwise, throws an exception indicating what went wrong.</returns>
    /// <exception cref="DalConfigException">Thrown if any step of the configuration retrieval or instance creation fails.</exception>
    public static IDal? Get()
    {
        // Retrieve the DAL type from configuration
        string dalType = s_dalName
            ?? throw new DalConfigException($"DAL name is not extracted from the configuration");

        // Retrieve the package name for the DAL type from configuration
        string dal = s_dalPackages[dalType]
           ?? throw new DalConfigException($"Package for {dalType} is not found in packages list");

        try
        {
            // Attempt to load the assembly containing the DAL
            Assembly.Load(dal ?? throw new DalConfigException($"Package {dal} is null"));
        }
        catch (Exception)
        {
            throw new DalConfigException("Failed to load {dal}.dll package");
        }

        // Attempt to resolve the DAL type from the loaded assembly
        Type? type = Type.GetType($"Dal.{dal}, {dal}")
            ?? throw new DalConfigException($"Class Dal.{dal} was not found in {dal}.dll");

        // Retrieve the singleton instance of the DAL
        return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?
                   .GetValue(null) as IDal
            ?? throw new DalConfigException($"Class {dal} is not singleton or Instance property not found");
    }
}
