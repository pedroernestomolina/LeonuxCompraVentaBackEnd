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
            //var rt1 = invPrv.Producto_GetLista(filt);
            //var filtroPrv = new DtoLibInventario.Proveedor.Filtro() { cadena = "*comercializadora" };
            //var rt2 = invPrv.Proveedor_GetLista(filtroPrv);
            //var rt0 = invPrv.Producto_Estatus_Lista();
            //var rt1 = invPrv.Producto_Categoria_Lista();
            //var rt2 = invPrv.Producto_Origen_Lista();
            //var rt3 = invPrv.Producto_AdmDivisa_Lista();
            //var rt4 = invPrv.Producto_Pesado_Lista();
            //var rt1= invPrv.Producto_GetFicha("0000000308");
            //var filtroCosto = new DtoLibInventario.Costo.Historico.Filtro() { autoProducto = "0000000308" };
            //var rt2 = invPrv.Producto_Costo_Historico_Lista(filtroCosto);
            //var filtroPrecio = new DtoLibInventario.Precio.Historico.Filtro() { autoProducto = "0000000308" };
            //var rt1 = invPrv.Producto_Precio_Historico_Lista(filtroPrecio);
            //var filtroKardex = new DtoLibInventario.Kardex.Movimiento.Filtro () { autoProducto = "0000001141", 
            //    ultDias= DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._7Dias};
            //var rt1 = invPrv.Producto_Kardex_Movimiento_Lista(filtroKardex);
            //var filtroExistencia = new DtoLibInventario.Existencia.Deposito.Filtro() { autoProducto = "0000000308", autoDeposito = "0000000001" };
            //var rt1 = invPrv.Producto_Existencia_Deposito(filtroExistencia);
            //var filtroExistencia = new DtoLibInventario.Existencia.Deposito.Filtro() { autoProducto = "0000000308", autoDeposito = "0000000001" };

            var filtroMov = new DtoLibInventario.Movimiento.Traslado.Consultar.Filtro() {autoDeposito = "0000000002" };
            var rt1 = invPrv.Producto_Movimiento_Traslado_Consultar_ProductosPorDebajoNivelMinimo(filtroMov);

            var fichaIns = new DtoLibInventario.Movimiento.Traslado.Insertar.Ficha();
            var rt2 = invPrv.Producto_Movimiento_Traslado_Insertar(fichaIns);

        }
    }
}