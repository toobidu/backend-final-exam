namespace Demo.Entities;

public class Store
{
    public int storeId { get; set; }
    public string storeName { get; set; } = null!;
    public string storePhone { get; set; } = null!;
    public string storeAddress { get; set; } = null!;
    public TimeOnly openTime { get; set; }
    public TimeOnly closeTime { get; set; }
    
    public ICollection<StoreSupplier> storeSuppeliers { get; set; } = new List<StoreSupplier>();
}