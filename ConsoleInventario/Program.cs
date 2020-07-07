using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleInventario
{
    class Program
    {
        static void Main(string[] args)
        {
            ILibInventario.IProvider invPrv = new ProvLibInventario.Provider();
            var filt = new DtoLibInventario.Producto.Filtro();
            filt.autoProveedor = "0000000104";
            filt.estatus= DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo;
            //filt.autoDeposito = "0000000002";
            //filt.autoGrupo = "0000000001";
            //filt.autoTasa = "0000000004";
            var rt1 = invPrv.Producto_GetLista(filt);
            var rt2= invPrv.Producto_GetFicha("0000001141");
        }
    }
}
