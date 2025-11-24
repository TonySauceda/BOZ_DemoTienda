using DemoTienda.Application.DTOs.Categoria;
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

public class CategoriaServiceTest : IClassFixture<SqliteInMemoryFixture>
{
    private readonly Mock<ILogger<CategoriaService>> _logger;
    private SqliteInMemoryFixture _fixture;

    public CategoriaServiceTest(SqliteInMemoryFixture fixture)
    {
        _fixture = fixture;
        _logger = new Mock<ILogger<CategoriaService>>();
    }

    private CategoriaService GetCategoriaService(DemoTiendaContext demoTiendaContext)
    {
        var categoriaRepository = new CategoriaRepository(demoTiendaContext);

        return new CategoriaService(categoriaRepository, _logger.Object);
    }

    [Fact]
    public async Task ListAsync_ShouldReturnListOfCategories()
    {
        // Arrange
        using var dbContext = _fixture.CreateDemoTiendaContext();
        var categoriaService = GetCategoriaService(dbContext);
        var categories = await dbContext.Categorias.ToListAsync();

        // Act
        var result = await categoriaService.ListAsync();

        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result.Count().ShouldBe(categories.Count);
        result.ShouldBeEquivalentTo(categories.ToDto());
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_CategoryNotFound()
    {
        // Arrange
        using var dbContext = _fixture.CreateDemoTiendaContext();
        var categoriaService = GetCategoriaService(dbContext);

        // Act
        var result = await categoriaService.GetByIdAsync(int.MaxValue);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        result.Error.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetById_ShouldReturnCategory_CategoryExists()
    {
        // Arrange
        using var dbContext = _fixture.CreateDemoTiendaContext();
        var categoriaService = GetCategoriaService(dbContext);
        int categoryId = 1;

        // Act
        var result = await categoriaService.GetByIdAsync(categoryId);

        // Assert
        result.ShouldNotBeNull();
        result.Value.ShouldNotBeNull();
        result.Value.Id.ShouldBe(categoryId);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnCategory_CategoryCreated()
    {
        // Arrange
        using var transaction = _fixture.DemoTiendaConnection.BeginTransaction();// Maintaining database state
        using var dbContext = _fixture.CreateDemoTiendaContext(transaction);
        var categoriaService = GetCategoriaService(dbContext);

        var newCategory = new CreateCategoriaRequest { Nombre = "Ropa", Descripcion = "Ropa juvenil", EsActiva = true };

        // Act
        var result = await categoriaService.AddAsync(newCategory);
        var createdCategory = await dbContext.Categorias.FindAsync(result.Value?.Id);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        createdCategory.ShouldNotBeNull();
        result.Value.ShouldBeEquivalentTo(createdCategory.ToDto());
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCategory()
    {
        // Arrange
        using var transaction = _fixture.DemoTiendaConnection.BeginTransaction();// Maintaining database state
        using var dbContext = _fixture.CreateDemoTiendaContext(transaction);
        var categoriaService = GetCategoriaService(dbContext);

        var categoryToUpdate = new UpdateCategoriaRequest
        {
            Id = 1,
            Nombre = "Electrónica",
            Descripcion = "Dispositivos electrónicos y gadgets",
            EsActiva = true
        };

        // Act
        await categoriaService.UpdateAsync(categoryToUpdate.Id, categoryToUpdate);

        // Assert
        var updatedCategory = await dbContext.Categorias.FirstAsync(x => x.Id == categoryToUpdate.Id);
        categoryToUpdate.Nombre.ShouldBe(updatedCategory.Nombre);
        categoryToUpdate.Descripcion.ShouldBe(updatedCategory.Descripcion);
        categoryToUpdate.EsActiva.ShouldBe(updatedCategory.EsActiva);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteCategory()
    {
        // Arrange
        using var transaction = _fixture.DemoTiendaConnection.BeginTransaction();// Maintaining database state
        using var dbContext = _fixture.CreateDemoTiendaContext(transaction);
        var categoriaService = GetCategoriaService(dbContext);
        int categoryIdToDelete = 1;

        // Act
        await categoriaService.DeleteAsync(categoryIdToDelete);

        // Assert
        var deletedCategory = await dbContext.Categorias.FindAsync(categoryIdToDelete);
        deletedCategory.ShouldBeNull();
    }
}