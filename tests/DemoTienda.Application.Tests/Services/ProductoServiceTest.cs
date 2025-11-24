using DemoTienda.Application.DTOs.Producto;
using DemoTienda.Application.Extensions;
using DemoTienda.Application.Services;
using DemoTienda.Infraestructure.Context;
using DemoTienda.Infraestructure.Repository;
using DemoTienda.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace DemoTienda.Application.Tests.Services;

public class ProductoServiceTest : IClassFixture<SqliteInMemoryFixture>
{
    private readonly Mock<ILogger<ProductoService>> _logger;
    private SqliteInMemoryFixture _fixture;

    public ProductoServiceTest(SqliteInMemoryFixture fixture)
    {
        _fixture = fixture;
        _logger = new Mock<ILogger<ProductoService>>();
    }

    private ProductoService GetProductoService(DemoTiendaContext demoTiendaContext)
    {
        var productoRepository = new ProductoRepository(demoTiendaContext);
        var categoriaRepository = new CategoriaRepository(demoTiendaContext);

        return new ProductoService(productoRepository, categoriaRepository);
    }

    [Fact]
    public async Task ListAsync_ShouldReturnListOfProducts()
    {
        // Arrange
        using var dbContext = _fixture.CreateDemoTiendaContext();
        var productoService = GetProductoService(dbContext);
        var products = await dbContext.Productos.ToListAsync();

        // Act
        var result = await productoService.ListAsync();

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count().ShouldBe(products.Count);
        result.ShouldBeEquivalentTo(products.ToDto());
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_ProductNotFound()
    {
        // Arrange
        using var dbContext = _fixture.CreateDemoTiendaContext();
        var productoService = GetProductoService(dbContext);

        // Act
        var result = await productoService.GetByIdAsync(int.MaxValue);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        result.Error.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetById_ShouldReturnProduct_ProductExists()
    {
        // Arrange
        using var dbContext = _fixture.CreateDemoTiendaContext();
        var productoService = GetProductoService(dbContext);
        int productId = 1;

        // Act
        var result = await productoService.GetByIdAsync(productId);

        // Assert
        result.ShouldNotBeNull();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(productId);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnProduct_ProductCreated()
    {
        // Arrange
        using var transaction = _fixture.DemoTiendaConnection.BeginTransaction();// Maintaining database state
        using var dbContext = _fixture.CreateDemoTiendaContext(transaction);
        var productoService = GetProductoService(dbContext);

        var newProduct = new CreateProductoRequest { Nombre = "Bocinas", Descripcion = "Bocinas 50w", Precio = 100m, IdCategoria = 5, EsActivo = true };

        // Act
        var result = await productoService.AddAsync(newProduct);
        var createdProduct = await dbContext.Productos.FindAsync(result.Value?.Id);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        createdProduct.ShouldNotBeNull();
        result.Value.ShouldBeEquivalentTo(createdProduct.ToDto());

    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProduct()
    {
        // Arrange
        using var transaction = _fixture.DemoTiendaConnection.BeginTransaction();// Maintaining database state
        using var dbContext = _fixture.CreateDemoTiendaContext(transaction);
        var productoService = GetProductoService(dbContext);

        var productToUpdate = new UpdateProductoRequest
        {
            Id = 1,
            Nombre = "Teclado",
            Descripcion = "Teclado en español",
            Precio = 55.5m,
            IdCategoria = 2,
            EsActivo = true
        };

        // Act
        await productoService.UpdateAsync(productToUpdate.Id, productToUpdate);

        // Assert
        var updatedProduct = await dbContext.Productos.FirstAsync(x => x.Id == productToUpdate.Id);
        productToUpdate.Nombre.ShouldBe(updatedProduct.Nombre);
        productToUpdate.Descripcion.ShouldBe(updatedProduct.Descripcion);
        productToUpdate.Precio.ShouldBe(updatedProduct.Precio);
        productToUpdate.IdCategoria.ShouldBe(updatedProduct.IdCategoria);
        productToUpdate.EsActivo.ShouldBe(updatedProduct.EsActivo);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteProduct()
    {
        // Arrange
        using var transaction = _fixture.DemoTiendaConnection.BeginTransaction();// Maintaining database state
        using var dbContext = _fixture.CreateDemoTiendaContext(transaction);
        var productoService = GetProductoService(dbContext);
        int productIdToDelete = 1;

        // Act
        await productoService.DeleteAsync(productIdToDelete);

        // Assert
        var deletedProduct = await dbContext.Productos.FindAsync(productIdToDelete);
        deletedProduct.ShouldBeNull();
    }
}