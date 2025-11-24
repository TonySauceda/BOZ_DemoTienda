using System.ComponentModel.DataAnnotations;

namespace DemoTienda.Application.DTOs.Producto;

public class UpdateProductoRequest
{
    public required int Id { get; set; }
    [MaxLength(150)]
    public required string Nombre { get; set; }
    [MaxLength(1000)]
    public string? Descripcion { get; set; }
    [Range(0, double.MaxValue)]
    public required decimal Precio { get; set; }

    public required int IdCategoria { get; set; }

    public bool EsActivo { get; set; }
}