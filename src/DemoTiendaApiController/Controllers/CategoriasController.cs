using DemoTienda.Api.Extensions;
using DemoTienda.Application.DTOs.Categoria;
using DemoTienda.Application.Services;
using DemoTienda.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoTiendaApiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(CategoriaService categoriaService, ILogger<CategoriasController> logger)
        {
            _categoriaService = categoriaService;
            _logger = logger;
        }

        /// <summary>
        /// Returns a list of all products.
        /// </summary>
        /// <remarks>
        /// This endpoint retrieves all products from the store.
        /// </remarks>
        /// <returns>A list of product objects.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoriaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<CategoriaResponse>>> GetCategorias()
        {
            _logger.LogInformation("Request: {url}", HttpContext.Request.Path.Value);
            return Ok(await _categoriaService.ListAsync());
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var result = await _categoriaService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return result.ToObjectResult();
            }

            return Ok(result.Value);
        }

        // PUT: api/Categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, UpdateCategoriaRequest updateCategoriaRequest)
        {
            var result = await _categoriaService.UpdateAsync(id, updateCategoriaRequest);

            if (!result.IsSuccess)
            {
                return result.ToObjectResult();
            }

            return NoContent();
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(CreateCategoriaRequest createCategoriaRequest)
        {
            var result = await _categoriaService.AddAsync(createCategoriaRequest);

            if (!result.IsSuccess)
            {
                return result.ToObjectResult();
            }

            return CreatedAtAction("GetCategoria", new { id = result.Value?.Id }, result.Value);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var result = await _categoriaService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return result.ToObjectResult();
            }

            return NoContent();
        }
    }
}
