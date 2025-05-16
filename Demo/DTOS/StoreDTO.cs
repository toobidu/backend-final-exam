namespace Demo.DTOS;

public class StoreDTO
{
    public int storeId { get; set; }
    public string storeName { get; set; }
    public string storeAddress { get; set; }
    public string storePhone { get; set; }
    public TimeOnly openTime { get; set; }
    public TimeOnly closeTime { get; set; }
}