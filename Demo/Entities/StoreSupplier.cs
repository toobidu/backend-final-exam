namespace Demo.Entities;

public class StoreSupplier
{
    public int StoreId { get; set; }
    public Store Store { get; set; } = null!;
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
    public float friendlyLevel { get; set; }
}