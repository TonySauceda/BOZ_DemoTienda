using System.ComponentModel.DataAnnotations;

namespace DemoTienda.Application.DTOs.Categoria;

public class UpdateCategoriaRequest
{
    public required int Id { get; set; }
    [Required, MaxLength(100)]
    public required string Nombre { get; set; }

    [MaxLength(500)]
    public string? Descripcion { get; set; }

    public bool EsActiva { get; set; }
}