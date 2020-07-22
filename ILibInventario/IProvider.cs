using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IProvider: IProducto, IDeposito, IDepartamento, IGrupo, ITasaImpuesto, IProveedor,
        ICosto, IPrecio, IKardex, IExistencia, IConcepto, ISucursal, IMovimiento, IUsuario, IReporteMovimientos, 
        ITool
    {

        DtoLib.ResultadoEntidad<DateTime> FechaServidor();
        //DtoLib.ResultadoEntidad<DtoLibPosOffLine.Sistema.InformacionBD.Ficha> InformacionBD();

    }

}