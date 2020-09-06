using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IProvider: IProducto, IDeposito, IDepartamento, IGrupo, ITasaImpuesto, IProveedor, IMarca,
        ICosto, IPrecio, IKardex, IConcepto, ISucursal, IMovimiento, IUsuario, IReporteMovimientos, 
        ITool, IEmpaqueMedida, IConfiguracion
    {

        DtoLib.ResultadoEntidad<DateTime> FechaServidor();
        //DtoLib.ResultadoEntidad<DtoLibPosOffLine.Sistema.InformacionBD.Ficha> InformacionBD();

    }

}