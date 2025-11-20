using System.ComponentModel.DataAnnotations;

namespace DemoTiendaApiController.Settings;

public class ProductoSettings
{
    [MinLength(2)]
    public required string DefaultCurrency { get; set; }
    [Range(1, 500)]
    public int MaxResults { get; set; }
}
