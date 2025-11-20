using DemoTienda.Domain.Entities;
using DemoTienda.Infraestructure.Context;
using DemoTienda.Mocks.Mocks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DemoTienda.Mocks;

public class SqliteInMemoryFixture : IDisposable
{
    public DbConnection DemoTiendaConnection { get; }

    public SqliteInMemoryFixture()
    {
        DemoTiendaConnection = new SqliteConnection("DataSource=file::memory:");
        DemoTiendaConnection.Open();
        DemoTiendaSeed();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    private void DemoTiendaSeed()
    {
        using var context = CreateDemoTiendaContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Categorias.AddRange(CategoriaMock.DataSet);
        context.Productos.AddRange(ProductoMock.DataSet);

        context.SaveChanges();
    }

    public DemoTiendaContext CreateDemoTiendaContext(DbTransaction? transaction = null)
    {
        var context = new DemoTiendaContext(new DbContextOptionsBuilder<DemoTiendaContext>()
            .UseSqlite(DemoTiendaConnection)
            .ConfigureWarnings(x => x.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.AmbientTransactionWarning))
            .Options, 
            unitTestContext: true);

        if (transaction != null)
        {
            context.Database.UseTransaction(transaction);
        }

        return context;
    }
}