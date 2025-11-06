namespace DemoTienda.Domain.Views;

public partial class ProductosCategoria
{
    public int Id { get; set; }

    public string NombreProducto { get; set; } = null!;

    public decimal Precio { get; set; }

    public bool EsActivo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;
}
