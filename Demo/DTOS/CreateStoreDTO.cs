using System.ComponentModel.DataAnnotations;

namespace Demo.DTOS;

public class CreateStoreDTO
{
    [Required]
    //Validate name
    [StringLength(maximumLength: 150, ErrorMessage = "Store name is too long")]
    public string storeName { get; set; }
    
    [Required]
    //Validate address
    [StringLength(maximumLength: 150, ErrorMessage = "Store address is too long")]
    public string storeAddress { get; set; }
    
    [Required]
    [RegularExpression(@"^(0|\+84)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-5]|9[0-9])[0-9]{7}$", 
        ErrorMessage = "Số điện thoại không hợp lệ")]
    public string storePhone { get; set; }
    
    [Required]
    public TimeOnly  openTime { get; set; }

    [Required]
    public TimeOnly closeTime { get; set; }
}