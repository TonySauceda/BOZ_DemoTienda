namespace DemoTienda.Application.DTOs.Categoria;

public class CategoriaResponse
{
    public int Id { get; set; }

    public required string Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool EsActiva { get; set; }

    public DateTime FechaCreacion { get; set; }
}