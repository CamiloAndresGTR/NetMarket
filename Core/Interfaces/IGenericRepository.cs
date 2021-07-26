using Core.Entities;
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


    }
}
