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
        //Constructor para crear la consulta sin filtro (Select * from)
        public ProductWithCategoryAndTradeMarkSpecification()
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.TradeMark);

        }
        //Constructor para crear la consulta con filtro (Where)
        public ProductWithCategoryAndTradeMarkSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.TradeMark);
        }
    }
}
