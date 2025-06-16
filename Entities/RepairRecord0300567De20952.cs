using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToTienDung0300567.Entities;

[Table("RepairRecord")]
public class RepairRecord0300567De20952
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    public int mechanicId { get; set; }
    
    [ForeignKey("MechanicId")]
    public Mechanic0300567De20945 mechanic { get; set; }
    
    public int vehicleId { get; set; }
    
    [ForeignKey("VehicleId")]
    public Vehicle0300567De20949 vehicle { get; set; }
    
    [Range(0, int.MaxValue)]
    public int soLanSua { get; set; }
}