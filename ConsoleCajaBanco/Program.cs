using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleCajaBanco
{

    class Program
    {
        static void Main(string[] args)
        {
            ILibCajaBanco.IProvider cajaBancoPrv = new ProvLibCajaBanco.Provider("localhost","pita");
            //var filtro = new DtoLibCajaBanco.Reporte.Movimiento.Filtro();
            //filtro.desdeFecha = new DateTime(2020, 07, 01);
            //filtro.hastaFecha = new DateTime(2020, 07, 20);
            //var r01 = cajaBancoPrv.CajaBanco_ArqueoCajaPos(filtro);
            //var r02 = cajaBancoPrv.Usuario_Principal();
            //var fecha = new DateTime(2020, 08, 05);
            //cajaBancoPrv.Reporte(fecha);

            //var filtro = new DtoLibCajaBanco.Reporte.Movimiento.Inventario.Filtro();
            //filtro.desdeFecha = new DateTime(2020, 10, 25);
            //filtro.hastaFecha = new DateTime(2020, 10, 28);
            //filtro.autoDeposito = "0000000004";
            //var r01 = cajaBancoPrv.Reporte_InventarioResumen(filtro);
            //var r01 =  cajaBancoPrv.Deposito_GetLista();
            //var r01 =  cajaBancoPrv.Deposito_GetPrincipal();

            //var filtro = new DtoLibCajaBanco.Reporte.Movimiento.ResumenVenta.Filtro();
            //filtro.desdeFecha = new DateTime(2020, 10, 01);
            //filtro.hastaFecha = new DateTime(2020, 10, 28);
            //filtro.codigoSucursal = "04";
            //var r01 = cajaBancoPrv.Reporte_VentaResumen(filtro);

            //var r01 = cajaBancoPrv.EmpresaGrupo_GetFicha("0000000002");
            //var filtro = new DtoLibCajaBanco.Reporte.Habladores.Filtro();
            //var r01 = cajaBancoPrv.Reporte_Habladores(filtro);

            //var filtro = new DtoLibCajaBanco.Reporte.Movimiento.FacturaDetalle.Filtro();
            //filtro.desdeFecha = new DateTime(2020, 11, 15);
            //filtro.hastaFecha = new DateTime(2020, 11, 15);
            //filtro.codigoSucursal = "04";
            //var r01 = cajaBancoPrv.Reporte_VentaDetalle(filtro);

            //var filtro = new DtoLibCajaBanco.Reporte.Movimiento.VentasPorProducto.Filtro();
            //filtro.desdeFecha = new DateTime(2020, 11, 7);
            //filtro.hastaFecha = new DateTime(2020, 11, 7);
            //filtro.codigoSucursal = "04";
            //var r01 = cajaBancoPrv.Reporte_VentaPorProducto(filtro);
            //var r01 = cajaBancoPrv.Deposito_GetFicha("0000000004");

            //var filtro = new DtoLibCajaBanco.Reporte.Movimiento.VentasPorProducto.Filtro();
            //filtro.desdeFecha = new DateTime(2020, 12, 1);
            //filtro.hastaFecha = new DateTime(2020, 12, 7);
            //filtro.codigoSucursal = "";
            //var r01 = cajaBancoPrv.Reporte_VentaPorProducto(filtro);

            //var filtro = new DtoLibCajaBanco.Reporte.Movimiento.ResumenVentaSucursal.Filtro();
            //filtro.desdeFecha = new DateTime(2020, 12, 1);
            //filtro.hastaFecha = new DateTime(2020, 12, 7);
            //filtro.codigoSucursal = "";
            //var r01 = cajaBancoPrv.Reporte_ResumenVentaSucursal(filtro);

            //var filtro = new DtoLibCajaBanco.Reporte.Movimiento.VentasPorProductoSucursal.Filtro();
            //filtro.desdeFecha = new DateTime(2020, 12, 1);
            //filtro.hastaFecha = new DateTime(2020, 12, 7);
            //filtro.codigoSucursal = "";
            //var r01 = cajaBancoPrv.Reporte_VentaPorProductoSucursal(filtro);

        }
    }

}