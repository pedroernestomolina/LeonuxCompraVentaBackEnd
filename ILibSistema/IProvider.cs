using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface IProvider: IDeposito, ISucursal, ISucursalGrupo, IUsuario, IPrecio
    {

        DtoLib.ResultadoEntidad<DateTime> FechaServidor();

    }

}