using DemoTienda.Api.Extensions;
using DemoTienda.Application.DTOs.Producto;
using DemoTienda.Application.Services;
using DemoTienda.Domain.Entities;
using DemoTiendaApiController.Settings;
using Microsoft.AspNetCore.Mvc;
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
            var result = await _productoService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = result.Title,
                    Status = (int)result.StatusCode,
                    Instance = HttpContext.Request.Path
                };
                problemDetails.Extensions.Add("errors", result.Error?.ToDictionary());

                return StatusCode(problemDetails.Status.Value, problemDetails);
            }

            return Ok(result.Value);
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, UpdateProductoRequest updateProductoRequest)
        {
            var result = await _productoService.UpdateAsync(id, updateProductoRequest);

            if (!result.IsSuccess)
            {
                return result.ToObjectResult();
            }

            return NoContent();
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(CreateProductoRequest createProductoRequest)
        {
            var result = await _productoService.AddAsync(createProductoRequest);

            if (!result.IsSuccess)
            {
                return result.ToObjectResult();
            }

            return CreatedAtAction("GetProducto", new { id = result.Value?.Id }, result.Value);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var result = await _productoService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return result.ToObjectResult();
            }

            return NoContent();
        }
    }
}
