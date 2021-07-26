using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/camilin/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Crear la inyección para que trabaje con ProductoRepository
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        //Vamos a crear un metodo controller de tipo get para obtener toda la lista de productos
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }
        //Vamos a crear un metodo controller de tipo get para obtener el producto por id
        //http://localhost:26992/api/camilin/v1/Products/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

    }
}
