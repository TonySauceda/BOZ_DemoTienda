namespace DemoTienda.Application.DTOs.Producto;

public class ProductoResponse
{
    public int Id { get; set; }

    public required string Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int IdCategoria { get; set; }
    public string? NombreCategoria { get; set; }

    public bool EsActivo { get; set; }

    public DateTime FechaCreacion { get; set; }
}