using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class MarketDbContextData
    {
        //Creo un me[todo asincrono para cargar la data a las tablas usando los archivos json de CargarData
        public static async Task LoadDataAsync(MarketDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                //Creo una condicion para que la insercion se haga si la tabla TradeMark no tiene contenido
                if (!context.TradeMark.Any())
                {
                    //Leo todo el texto que se encuentra dentro del archivo marca.json
                    var tradeMarkData = File.ReadAllText("../BusinessLogic/CargarData/marca.json");
                    //Ahora deserializo lo que trae la variable tradeMarkData y le doy formato de lista de la clase TradeMark
                    var tradeMarks = JsonSerializer.Deserialize<List<TradeMark>>(tradeMarkData);
                    //Recorro la lista que acabo de crear por medio de un bucle
                    foreach (var tradeMark in tradeMarks)
                    {
                        //Inserto cada uno de los valores en la tabla TradeMark
                        context.TradeMark.Add(tradeMark);
                    }
                    await context.SaveChangesAsync();
                }
                //Creo una condicion para que la insercion se haga si la tabla TradeMark no tiene contenido
                if (!context.Category.Any())
                {
                    //Leo todo el texto que se encuentra dentro del archivo categoria.json
                    var categoryData = File.ReadAllText("../BusinessLogic/CargarData/categoria.json");
                    //Ahora deserializo lo que trae la variable tradeMarkData y le doy formato de lista de la clase Category
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);
                    //Recorro la lista que acabo de crear por medio de un bucle
                    foreach (var category in categories)
                    {
                        //Inserto cada uno de los valores en la tabla Category
                        context.Category.Add(category);
                    }
                    await context.SaveChangesAsync();
                }
                //Creo una condicion para que la insercion se haga si la tabla TradeMark no tiene contenido
                if (!context.Product.Any())
                {
                    //Leo todo el texto que se encuentra dentro del archivo categoria.json
                    var productData = File.ReadAllText("../BusinessLogic/CargarData/producto.json");
                    //Ahora deserializo lo que trae la variable tradeMarkData y le doy formato de lista de la clase Category
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    //Recorro la lista que acabo de crear por medio de un bucle
                    foreach (var prd in products)
                    {
                        //Inserto cada uno de los valores en la tabla Category
                        context.Product.Add(prd);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                // Manejo de excepciones con LoggerFactory
                var logger = loggerFactory.CreateLogger<MarketDbContextData>();
                logger.LogError(e.Message, e.InnerException);
            }
        }
    }
}
