using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{

    public class ProductRepository : IProductRepository
    {
        //Inyectamos El DbContext para que pueda ser usado
        private readonly MarketDbContext _context;
        public ProductRepository(MarketDbContext context)
        {
            _context = context;
        }
        //Método asincrono para obtener los productos por id
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Product
                                 .Include(p => p.TradeMark)
                                 .Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        //Método asincrono para listar todos los productos
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Product
                                 .Include(p => p.TradeMark)
                                 .Include(p => p.Category)
                                 .ToListAsync();
        }
    }
}
