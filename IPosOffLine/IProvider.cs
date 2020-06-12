using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{

    public interface IProvider: IProducto, ICliente, IServidor, IItem, IVentaDocumento, IFiscal, IConfiguracion,
        IPendiente, IPermiso, IDeposito, ICobrador, IVendedor, ITransporte, IMedioCobro, ISerie,
        IJornada, IOperador, IUsuario
    {

        void setServidorRemoto(string instancia, string baseDatos);
        DtoLib.ResultadoEntidad<DateTime> FechaServidor();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Sistema.InformacionBD.Ficha > InformacionBD();
        DtoLib.Resultado Inicializar_BdLocal();

    }

}