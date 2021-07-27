﻿using System;
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

            //var r01 = compraPrv.Empresa_GetTasas();
            //var r01 = compraPrv.Concepto_GetFicha("0000000002");
            //var r01 = compraPrv.Producto_GetUtilidadPrecio("0000000027");
            //var r01 = compraPrv.Configuracion_MetodoCalculoUtilidad();
            //var r02 = compraPrv.Configuracion_ForzarRedondeoPrecioVenta();
            //var r01 = compraPrv.Configuracion_PreferenciaRegistroPrecio();
            //var ficha= new DtoLibCompra.Producto.VerificarDepositoAsignado.Ficha(){ autoPrd="0000000432", autoDeposito="0000000001"};
            //var r01 = compraPrv.Producto_VerificaDepositoAsignado (ficha);
            //var r01 = compraPrv.Compra_DocumentoVisualizar("0000000009");

            //var r01 = compraPrv.Permiso_Registrar_Factura("0000000006");
            //var r01 = compraPrv.Permiso_AdmDoc("0000000006");
            //var r02 = compraPrv.Permiso_AdmDoc_Anular("0000000006");
            //var r03 = compraPrv.Permiso_AdmDoc_Visualizar("0000000006");
            //var r04 = compraPrv.Permiso_AdmDoc_Reporte("0000000006");

            //var filtro = new DtoLibCompra.Documento.Lista.Filtro()
            //{
            //    Desde = new DateTime(2021, 01, 23),
            //    Hasta = new DateTime(2021, 01, 23),
            //};
            //var r01 = compraPrv.Compra_DocumentoGetLista(filtro);

            //var filtro = new DtoLibCompra.Reportes.CompraPorDepartamento.Filtro()
            //{
            //    desde = new DateTime(2021, 03, 01),
            //    hasta = new DateTime(2021, 04, 05),
            //};
            //var r01 = compraPrv.Reportes_ComprasPorDepartamento(filtro);

            //var filtro = new DtoLibCompra.Reportes.CompraDocumento.Filtro()
            //{
            //    desde = new DateTime(2020, 11, 01),
            //    hasta = new DateTime(2020, 11, 30),
            //};
            //var r01 = compraPrv.Reportes_ComprasDocumento(filtro);

            //var filtro = new DtoLibCompra.Reportes.CompraPorProductoDetalle.Filtro()
            //{
            //    desde = new DateTime(2020, 11, 01),
            //    hasta = new DateTime(2020, 11, 30),
            //};
            //var r01 = compraPrv.Reportes_CompraPorProductoDetalle(filtro);

            //var filtro = new DtoLibCompra.Reportes.CompraDocumento.Filtro()
            //{
            //    desde = new DateTime(2020, 11, 01),
            //    hasta = new DateTime(2020, 11, 30),
            //    codSucursal = "02",
            //    estatus = DtoLibCompra.Reportes.Enumerados.EnumEstatus.Anulado,
            //};
            //var r01 = compraPrv.Reportes_ComprasDocumento (filtro);

            //var filtro = new DtoLibCompra.Documento.Lista.Filtro()
            //{
            //     Desde= new DateTime(2021,01,01),
            //      Hasta= new DateTime(2021,01,26),
            //};
            //var r01 = compraPrv.Compra_DocumentoGetLista (filtro);

            //var r01 = compraPrv.Estado_GetLista();

            //var filtro = new DtoLibCompra.Proveedor.Documento.Filtro()
            //{
            //    autoProv = "0000000002",
            //    desde = new DateTime(2021, 01, 01),
            //    hasta = new DateTime(2021, 02, 22),
            //};
            //var r01 = compraPrv.Proveedor_Documento_GetLista(filtro);

            //var filtro = new DtoLibCompra.Proveedor.Articulos.Filtro()
            //{
            //    autoProv = "0000000002",
            //    desde = new DateTime(2021, 01, 01),
            //    hasta = new DateTime(2021, 02, 22),
            //};
            //var r01 = compraPrv.Proveedor_CompraArticulos_GetLista(filtro);

            //var filtro = new DtoLibCompra.Reportes.Proveedor.Maestro.Filtro ()
            //{
            //};
            //var r01 = compraPrv.ReportesProv_Maestro (filtro);

            //var r01 = compraPrv.Compra_Documento_ItemImportar_GetLista("0000000419");

        }

    }

}