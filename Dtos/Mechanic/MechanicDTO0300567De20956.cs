using System.ComponentModel.DataAnnotations;

namespace ToTienDung0300567.Dtos.Mechanic;

public class MechanicDTO0300567De20956
{
    public int id { get; set; }
    
    [Required(ErrorMessage = "Mã thợ không được để trống")]
    public string maTho { get; set; }
    
    [Required(ErrorMessage = "Tên thợ không được để trống")]
    public string tenTho { get; set; }
    
    [Required(ErrorMessage = "CCCD không được để trống")]
    [StringLength(12, MinimumLength = 12, ErrorMessage = "CCCD phải có 12 ký tự")]
    public string cCCD { get; set; }
    
    public DateTime ngayNhanViec { get; set; }
}