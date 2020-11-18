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
            //var filtro = new DtoLibCompra.Producto.Lista.Filtro()
            //{
            //    cadena = "*maiz",
            //    MetodoBusqueda = DtoLibCompra.Producto.Enumerados.EnumMetodoBusqueda.Nombre,
            //};
            //var r01 = compraPrv.Producto_GetLista(filtro);
            //var r01 = compraPrv.Empresa_Datos();
            //var r01 = compraPrv.Usuario_Principal();
            //var filtro = new DtoLibCompra.Usuario.Buscar.Ficha() { clave = "1188", codigo = "01" };
            //var r01 = compraPrv.Usuario_Buscar(filtro);
            //var r01 = compraPrv.Permiso_PedirClaveAcceso_NivelMinimo();
            //var r01 = compraPrv.Permiso_ToolCompra("0000000001");
            //var r01 = compraPrv.Configuracion_TasaCambioActual();
            //var r01 = compraPrv.Configuracion_PreferenciaBusquedaProveedor();
            //var r01 = compraPrv.Proveedor_GetFicha("0000000002");
            //var r01 = compraPrv.Configuracion_PreferenciaBusquedaProducto();
            //var r01 = compraPrv.Producto_GetFicha("0000000025");
            //var filtro = new DtoLibCompra.Producto.CodigoRefProveedor.Filtro() { autoPrd = "0000000042", autoPrv= "0000000002" };
            //var r01 = compraPrv.Producto_GetCodigoRefProveedor(filtro);
        }

    }

}