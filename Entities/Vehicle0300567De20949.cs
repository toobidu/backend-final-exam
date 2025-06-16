using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToTienDung0300567.Entities;

[Table("Vehicle")]
public class Vehicle0300567De20949
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    
    [Required]
    [StringLength(20)]
    public string bienSoXe { get; set; }
    
    [Required]
    [StringLength(50)]
    public string loaiXe { get; set; }
    
    public ICollection<RepairRecord0300567De20952> RepairRecords { get; set; }
}
