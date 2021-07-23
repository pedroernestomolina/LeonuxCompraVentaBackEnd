using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{

    public interface IProvider: IDeposito, ISucursal, IProveedor, IProducto, IUsuario, IEmpresa, 
        IPermiso, IConfiguracion, IDocumento, IConcepto, IReportes, IGrupo, IEstado, IReporteProv
    {

        DtoLib.ResultadoEntidad<DateTime> FechaServidor();

    }

}