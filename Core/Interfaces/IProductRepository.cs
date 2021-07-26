using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        //Creo el método para traer los productos por id, es un método de tipo asincrono por lo tanto es un Task de tipo Product
        ////y por buena práctica al final del nombre le pongo la palabra Async
        Task<Product> GetProductByIdAsync(int id);

        //Creo el método para traer los productos, al ser solo una lista lo uso de tipo IReadOnlyList
        Task<IReadOnlyList<Product>> GetProductsAsync();
    }
}
