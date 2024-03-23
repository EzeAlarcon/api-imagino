using imaginemos_tecnica.Data;
using imaginemos_tecnica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace imaginemos_tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly ImaginamosDb _context;

        public VentasController(ImaginamosDb context)
        {
            _context = context;
        }

        // Productos

        [HttpGet("productos")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        [HttpGet("productos/{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        [HttpPost("productos")]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        [HttpPut("productos/{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("productos/{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Ventas

        [HttpGet("ventas")]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        {
            return await _context.Ventas.ToListAsync();
        }

        [HttpGet("ventas/{id}")]
        public async Task<ActionResult<Venta>> GetVenta(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        [HttpPost("ventas")]
        public async Task<ActionResult<Venta>> PostVenta(Venta venta)
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVenta), new { id = venta.Id }, venta);
        }

        [HttpPut("ventas/{id}")]
        public async Task<IActionResult> PutVenta(int id, Venta venta)
        {
            if (id != venta.Id)
            {
                return BadRequest();
            }

            _context.Entry(venta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaExixtente(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool VentaExixtente(int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("ventas/{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // Usuarios

        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("usuarios/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost("usuarios")]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpPut("usuarios/{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("usuarios/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Detalles de Venta

        [HttpGet("ventas/{ventaId}/detalles")]
        public async Task<ActionResult<IEnumerable<DetalleVenta>>> GetDetallesVenta(int ventaId)
        {
            var detallesVenta = await _context.DetallesVentas
                .Where(d => d.VentaId == ventaId)
                .ToListAsync();

            return detallesVenta;
        }

        [HttpGet("ventas/{ventaId}/detalles/{id}")]
        public async Task<ActionResult<DetalleVenta>> GetDetalleVenta(int ventaId, int id)
        {
            var detalleVenta = await _context.DetallesVentas.FindAsync(id);

            if (detalleVenta == null || detalleVenta.VentaId != ventaId)
            {
                return NotFound();
            }

            return detalleVenta;
        }

        [HttpPost("ventas/{ventaId}/detalles")]
        public async Task<ActionResult<DetalleVenta>> PostDetalleVenta(int ventaId, DetalleVenta detalleVenta)
        {
            // Asignamos la venta al detalle de venta
            detalleVenta.VentaId = ventaId;

            _context.DetallesVentas.Add(detalleVenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalleVenta), new { ventaId = ventaId, id = detalleVenta.Id }, detalleVenta);
        }

        [HttpPut("ventas/{ventaId}/detalles/{id}")]
        public async Task<IActionResult> PutDetalleVenta(int ventaId, int id, DetalleVenta detalleVenta)
        {
            if (id != detalleVenta.Id || ventaId != detalleVenta.VentaId)
            {
                return BadRequest();
            }

            _context.Entry(detalleVenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleVentaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool DetalleVentaExists(int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("ventas/{ventaId}/detalles/{id}")]
        public async Task<IActionResult> DeleteDetalleVenta(int ventaId, int id)
        {
            var detalleVenta = await _context.DetallesVentas.FindAsync(id);

            if (detalleVenta == null || detalleVenta.VentaId != ventaId)
            {
                return NotFound();
            }

            _context.DetallesVentas.Remove(detalleVenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
