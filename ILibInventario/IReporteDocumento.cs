using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{

    public interface IReporteDocumento
    {

        DtoLib.ResultadoEntidad<DtoLibInventario.Reportes.Documentos.MovimientoInventario.Ficha> ReporteDocumento_Movimiento(string autoDoc);

    }

}
