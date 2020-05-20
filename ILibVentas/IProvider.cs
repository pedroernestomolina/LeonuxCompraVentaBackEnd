using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibVentas
{

    public interface IProvider: IVenta, IProducto, IVendedor, ICobrador, ICliente,
        IDeposito, ISucursal, ITransporte, IMedioCobro, IAgencia, IUsuario, IConfiguracion,
        IExistencia, IPrecio, IPermiso, IMovInvConcepto, ISeries, IFiscal
    {

        DtoLib.ResultadoEntidad<DateTime> FechaServidor();

    }

}
