using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IReportesAdm
    {

        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Ficha> ReportesAdm_GeneralDocumento(DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Filtro filtro);

    }

}