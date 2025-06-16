namespace ToTienDung0300567.Dtos.Mechanic;

public class MechanicFilterDTO0300567De21012
{
    private string _keyword;
    
    public string Keyword
    {
        get => _keyword;
        set => _keyword = value?.Trim() ?? string.Empty;
    }
    
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}