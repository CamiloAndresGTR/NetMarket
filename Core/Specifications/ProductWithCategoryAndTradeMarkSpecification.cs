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
        public ProductWithCategoryAndTradeMarkSpecification(ProductSpecificationParams productParams)
            : base(x => 
            (string.IsNullOrEmpty(productParams.Search) || x.Name.Contains(productParams.Search)) &&
            (!productParams.Marca.HasValue || x.TradeMarkId == productParams.Marca) &&
            (!productParams.Categoria.HasValue || x.CategoryId == productParams.Categoria))
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.TradeMark);
            // AddOrderBy(p => p.Name);
            //switch con las opciones para ordenamiento
            // ApplyPaging(0, 5)
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "nombreAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "nombreDesc":
                        AddOrderByDescending(p => p.Name);
                        break;
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
