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
    public class CategoriesController : BaseApiController
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        public CategoriesController(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
    }
}
