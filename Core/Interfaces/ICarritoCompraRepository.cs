using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICarritoCompraRepository
    {
        Task<CarritoCompra> GetCarritoCompraAsync(string carritoId);
        Task<CarritoCompra> UpdateCarritoCompraAsync(CarritoCompra carritoCompra);
        Task<bool> DeleteCarritoCompraAsync(string carritoId);
    }
}
