using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericSeguridadRepository<T> where T : IdentityUser
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
        //Método para contar elementos de forma asincrona
        Task<int> CountAsync(ISpecification<T> spec);

        //Método para añadir elementos
        Task<int> AddAsync(T entity);
        // Método para actualizar elementos
        Task<int> UpdateAsync(T entity);
    }
}
