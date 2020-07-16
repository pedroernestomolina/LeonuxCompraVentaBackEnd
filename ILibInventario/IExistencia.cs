using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{

    public interface IExistencia
    {

        DtoLib.ResultadoEntidad<DtoLibInventario.Existencia.Deposito.Ficha> Producto_Existencia_Deposito(DtoLibInventario.Existencia.Deposito.Filtro filtro);

    }

}
