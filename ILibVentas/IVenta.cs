using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibVentas
{

    public interface IVenta
    {

        DtoLib.ResultadoLista<DtoLibVenta.Venta.Resumen> VentaLista(DtoLibVenta.Venta.Filtro filtro);
        DtoLib.ResultadoAuto VentaAgregar (DtoLibVenta.Venta.Agregar ficha );

    }

}