using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCajaBanco
{

    public interface IProvider: ISucursal, IUsuario, IReporteMovimiento
    {

        DtoLib.ResultadoEntidad<DateTime> FechaServidor();

    }

}