using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    
    public class CarritoCompraController : BaseApiController
    {
        private readonly ICarritoCompraRepository _carritoCompra;

        public CarritoCompraController(ICarritoCompraRepository carritoCompra)
        {
            _carritoCompra = carritoCompra;
        }

        [HttpGet]
        public async Task<ActionResult<CarritoCompra>> GetCarritoCompraById(string id)
        {
            return Ok(await _carritoCompra.GetCarritoCompraAsync(id) ?? new CarritoCompra(id));
        }

        [HttpPost]
        public async Task<ActionResult<CarritoCompra>> UpdateCarritoCompra(CarritoCompra carritoParam)
        { 
            return Ok(await _carritoCompra.UpdateCarritoCompraAsync(carritoParam));
        }

        [HttpDelete]
        public async Task DeleteCarritoCompra(string id)
        {
            await _carritoCompra.DeleteCarritoCompraAsync(id);
        }
    }
}
