using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Inventario.Producto
{
    
    public class Resumen
    {

        public string Auto { get; set; }
        public string CodigoPrd { get; set; }
        public string NombrePrd { get; set; }
        public string ReferenciaPrd { get; set; }
        public string DescripcionPrd { get; set; }
        public bool IsActivo { get; set; }

    }

}