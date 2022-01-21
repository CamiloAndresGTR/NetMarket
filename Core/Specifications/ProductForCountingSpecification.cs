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
            : base(x => (!productParams.Marca.HasValue || x.TradeMarkId == productParams.Marca) &&
            (!productParams.Categoria.HasValue || x.CategoryId == productParams.Categoria))
        {

        }
    }
}
