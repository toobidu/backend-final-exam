using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToTienDung0300567.Entities;

[Table("Mechanic")]
public class Mechanic0300567De20945
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string maTho { get; set; }
    
    [Required]
    [StringLength(100)]
    public string tenTho { get; set; }
    
    [Required]
    [StringLength(12)]
    public string cCCD { get; set; }
    
    public DateTime ngayNhanViec { get; set; }
    
    public ICollection<RepairRecord0300567De20952> RepairRecords { get; set; }
}
