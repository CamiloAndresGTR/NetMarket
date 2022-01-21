using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductForCountingSpecification : BaseSpecification<Product>
    {
        public ProductForCountingSpecification(ProductSpecificationParams productParams)
            : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.Contains(productParams.Search)) &&
            (!productParams.Marca.HasValue || x.TradeMarkId == productParams.Marca) &&
            (!productParams.Categoria.HasValue || x.CategoryId == productParams.Categoria))
        {

        }
    }
}
