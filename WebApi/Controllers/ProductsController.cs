﻿using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs;
using WebApi.Errors;

namespace WebApi.Controllers
{

    public class ProductsController : BaseApiController
    {
        //Inyectar Mapper
        private readonly IMapper _mapper;

        //Crear la inyección para que trabaje con ProductoRepository
        //private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<Product> _genericRepository;
        public ProductsController(IGenericRepository<Product> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        //Vamos a crear un metodo controller de tipo get para obtener toda la lista de productos
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(string sort, int? marca, int? categoria)
        {
            var spec = new ProductWithCategoryAndTradeMarkSpecification(sort, marca, categoria);
            var products = await _genericRepository.GetAllWithSpec(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products));
        }
        //Vamos a crear un metodo controller de tipo get para obtener el producto por id
        //http://localhost:26992/api/camilin/v1/Products/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            //spec debe incluir la lógica de la condicion de la consulta y las relaciones entre las entidades
            //En este caso la relación entre producto y marca, categoria
            var spec = new ProductWithCategoryAndTradeMarkSpecification(id);
            var product = await _genericRepository.GetByIdWithSpec(spec);
            //Crear un condicional para definir un mensade de error personalizado de acuerdo a lo creado en
            //la clase CodeErrorResponse, puedo enviarlo solamente con el codigo o puedo definir el mensaje personalizado
            if (product == null)
            {
                return NotFound(new CodeErrorResponse(404, "El producto no existe"));
            }
            return _mapper.Map<Product, ProductDTO>(product);
        }

    }
}
