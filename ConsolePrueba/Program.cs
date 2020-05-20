using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsolePrueba
{

    class Program
    {

        static void Main(string[] args)
        {

            IPosOffLine.IProvider _offLine = new ProvSqLitePosOffLine.Provider(@"D:\Proyectos FoxSystem\CompraVenta\LeonuxPosOffLine.db");
            var fechaActual= _offLine.FechaServidor();


            //ILibCompras.IProvider _compra= new ProvLibCompra.Provider();
            //var filtro = new DtoLibCompra.Compra.Filtro();
            //filtro.segun_FechaEmisionDesde = new DateTime(2020, 01, 01);
            //var r01= _compra.CompraLista(filtro);

            //ILibVentas.IProvider _venta= new ProvLibVenta.Provider();
            //var filtroV = new DtoLibVenta.Venta.Filtro();
            //filtroV.segun_FechaEmisionDesde = new DateTime(2020, 01, 01);
            //var r02 = _venta.VentaLista(filtroV);

            //var r0i = _venta.FactorCambioDivisa();
            //var r0j = _venta.FactorCambioDivisaParaRecibir();
            //var r0k = _venta.PreferenciaBusquedaProducto();
            //var r0l = _venta.PreferenciaBusquedaCliente();
            //var r0m = _venta.MetodoCalculoUtilidad();
            //var r0n = _venta.HabilitarRupturaPorExistencia();
            //var r0o = _venta.HabilitarAlertaPorExistenciaEnNegativa();
            //var r0p = _venta.Venta_DarDescuento_Item("0000000001");
            //var r0q = _venta.Venta_Eliminar_Item("0000000001");
            //var r0r = _venta.Venta_DescuentoGlobal("0000000002");


            //var filtroP = new DtoLibVenta.Inventario.Producto.Filtro();
            //filtroP.cadena="*100";
            //filtroP.preferenciaBusqueda = DtoLibVenta.Inventario.Enumerados.enumPreferenciaBusqueda.Codigo;
            //var r03 = _venta.ProductoLista(filtroP);
            //var r04 = _venta.ProductoExistencia("0000002976");
            //var r05 = _venta.ProductoPrecios("0000002976");
            //var r06 = _venta.ProductoDetalleResumen("0000002976");
            //var r07 = _venta.TransporteLista();
            //var r08 = _venta.VendedorLista();
            //var r09 = _venta.CobradorLista();
            //var r0a = _venta.DepositoLista();
            //var r0b = _venta.SucursalLista();
            //var r0c = _venta.Producto("0000002976");
            //var r0d = _venta.MedioCobroLista();
            //var r0e = _venta.AgenciaLista();

            //var filtroC = new DtoLibVenta.Cliente.Filtro();
            //filtroC.cadena = "*JHONATAN COL";
            //filtroC.preferenciaBusqueda = DtoLibVenta.Cliente.Enumerados.enumPreferenciaBusqueda.Nombre;
            //var r0f = _venta.ClienteLista(filtroC);
            //var r0g = _venta.ClienteDetalleResumen("0900000305");
            //var r0h = _venta.ClienteDocPendientePorCobrar ("0900000305");
                      
            //var ficha = new DtoLibVenta.Cliente.AgregarEventual() 
            //{
            //    CiRif="V33079951",
            //    DireccionFiscal="SAN DIEGO",
            //    NombreRazonSocial="ANGELA MOLINA",
            //    Telefono="",
            //};
            //var x1 = _venta.ClienteAgregarEventual(ficha);
        }

    }

}