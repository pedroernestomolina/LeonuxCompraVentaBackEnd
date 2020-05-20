using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibVentas
{

    public interface ICliente
    {

        DtoLib.ResultadoLista<DtoLibVenta.Cliente.Resumen> ClienteLista(DtoLibVenta.Cliente.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibVenta.Cliente.DetalleResumen> ClienteDetalleResumen(string auto);
        DtoLib.ResultadoLista<DtoLibVenta.Cliente.PendientePorCobrar> ClienteDocPendientePorCobrar(string auto);
        DtoLib.ResultadoAuto ClienteAgregarEventual(DtoLibVenta.Cliente.AgregarEventual ficha);

    }

}