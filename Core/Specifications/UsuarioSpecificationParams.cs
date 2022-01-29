

namespace Core.Specifications
{
    public class UsuarioSpecificationParams 
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int MaxPageSize = 50;
        private int _pageSize = 3;
        public int PageSize 
        { 
            get => _pageSize ; 
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; 
        }
        public string Search { get; set; }
    }
}
