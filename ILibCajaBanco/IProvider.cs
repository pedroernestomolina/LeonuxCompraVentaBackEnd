using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCajaBanco
{

    public interface IProvider: ISucursal, IUsuario, IReporteMovimiento, IDeposito, IEmpresaGrupo
    {

        DtoLib.ResultadoEntidad<DateTime> FechaServidor();

    }

}