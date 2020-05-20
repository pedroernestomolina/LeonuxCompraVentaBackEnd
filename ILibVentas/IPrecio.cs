using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibVentas
{
 
    public interface IPrecio
    {

        DtoLib.ResultadoEntidad<DtoLibVenta.Inventario.Precio.Resumen> Precio(string autoProducto, string idPrecio);

    }

}