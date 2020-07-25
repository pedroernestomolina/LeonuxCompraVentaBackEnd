using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{

    public interface IReporteMovimientos
    {

        DtoLib.ResultadoLista<DtoLibInventario.Reportes.Movimientos.ArqueoCajaPos.Ficha> CajaBanco_ArqueoVentaPos(DtoLibInventario.Reportes.Movimientos.Filtro filtro);

    }

}