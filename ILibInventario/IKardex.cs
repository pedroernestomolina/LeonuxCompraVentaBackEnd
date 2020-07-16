using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IKardex
    {

        DtoLib.ResultadoLista<DtoLibInventario.Kardex.Movimiento.Resumen> Producto_Kardex_Movimiento_Lista(DtoLibInventario.Kardex.Movimiento.Filtro filtro);

    }

}