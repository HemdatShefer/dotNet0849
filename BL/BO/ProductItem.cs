
using OtherFunctions;

namespace BO;

public class ProductItem
{
    /// <summary>
    /// 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Enums.Category Categories { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool InStock { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int AmountInCart { get; set; }

    public override string ToString()
    {
        return this.toString();
    }
}
