using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    // Hacer que la interfaz sea pública y que sea generica (Base T)
    public interface IGenericRepository<T> where T : ClaseBase
    {
        //Creamos un método generico para traer los elementos de una entindad o elemento que reemplaza a T
        //por id entero
        Task<T> GetByIdAsync(int id);
        //Creamos un método generico para traer una lista de todos los elementos de una entidad
        Task<IReadOnlyList<T>> GetAllAsync();
        //Creamos un método que traiga por Id pero cn especificaciones o includes y where
        Task<T> GetByIdWithSpec(ISpecification<T> spec);
        //Creamos un método generico para traer una lista de todos los elementos de una entidad
        //pero cn especificaciones o includes y where
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);

    }
}
