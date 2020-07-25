using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCajaBanco
{

    public interface IReporteMovimiento
    {

        DtoLib.ResultadoLista<DtoLibCajaBanco.Reporte.Movimiento.ArqueoCajaPos.Ficha> CajaBanco_ArqueoCajaPos(DtoLibCajaBanco.Reporte.Movimiento.Filtro filtro);

    }

}