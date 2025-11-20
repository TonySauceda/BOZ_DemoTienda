using DemoTienda.Application.Services;
using DemoTienda.Domain.Entities;
using DemoTiendaApiController.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DemoTiendaApiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoSettings _productoSettings;
        private readonly ProductoService _productoService;

        public ProductosController(ProductoService productoService, IOptions<ProductoSettings> productoSettings)
        {
            _productoService = productoService;
            _productoSettings = productoSettings.Value;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return Ok(await _productoService.ListAsync());
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);

            return producto is null ? NotFound() : Ok(producto);
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id) return BadRequest();

            await _productoService.UpdateAsync(producto);

            return NoContent();
        }

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            var newProducto = await _productoService.AddAsync(producto);

            return CreatedAtAction("GetProducto", new { id = newProducto.Id }, newProducto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            await _productoService.DeleteAsync(id);

            return NoContent();
        }
    }
}
