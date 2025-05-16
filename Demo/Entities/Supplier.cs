namespace Demo.Entities;

public class Supplier
{
    public int supplierId { get; set; }
    public string supplierName { get; set; } = null!;
    public string supplierAddress { get; set; } = null!;
    public string supplierPhone { get; set; } = null!;
    
    public ICollection<StoreSupplier> storeSuppliers { get; set; } = new List<StoreSupplier>();
}