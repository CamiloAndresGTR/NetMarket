using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithCategoryAndTradeMarkSpecification : BaseSpecification<Product>
    {
        //Constructor para crear la consulta sin filtro (Select * from) en el controller
        public ProductWithCategoryAndTradeMarkSpecification(string sort, int? marca, int? categoria)
            : base(x => (!marca.HasValue || x.TradeMarkId == marca) && (!categoria.HasValue || x.CategoryId == categoria))
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.TradeMark);
            // AddOrderBy(p => p.Name);
            //switch con las opciones para ordenamiento
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    case "descriptionAsc":
                        AddOrderBy(p => p.Description);
                        break;
                    case "descriptionDesc":
                        AddOrderByDescending(p => p.Description);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }
        //Constructor para crear la consulta con filtro (Where)
        public ProductWithCategoryAndTradeMarkSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.TradeMark);
        }
    }
}
