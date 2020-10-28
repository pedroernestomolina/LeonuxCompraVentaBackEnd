using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleCompra
{

    class Program
    {

        static void Main(string[] args)
        {
            ILibCompras.IProvider compraPrv = new ProvLibCompra.Provider("localhost", "pita");
            //var r01 = compraPrv.Sucursal_GetLista();
            //var r01 = compraPrv.Sucursal_GetFicha("0000000001");
            //var r01 = compraPrv.Deposito_GetLista();
            //var r01 = compraPrv.Deposito_GetFicha("0000000001");
            //var filtro = new DtoLibCompra.Proveedor.Lista.Filtro()
            //{
            //    autoEstado= "0000000001",
            //};
            //var r01 = compraPrv.Proveedor_GetLista(filtro);
        }

    }

}