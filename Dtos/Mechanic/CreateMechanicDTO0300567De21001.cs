using System.ComponentModel.DataAnnotations;

namespace ToTienDung0300567.Dtos.Mechanic;

public class CreateMechanicDTO0300567De21001
{
    private string _matho;
    private string _tentho;
    private string _cccd;

    [Required(ErrorMessage = "Mã thợ là bắt buộc")]
    public string maTho
    {
        get => _matho;
        set => _matho = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Tên thợ là bắt buộc")]
    public string tenTho
    {
        get => _tentho;
        set => _tentho = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "CCCD là bắt buộc")]
    public string cCCD
    {
        get => _cccd;
        set => _cccd = value?.Trim() ?? string.Empty;
    }
}