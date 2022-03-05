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
            ILibInventario.IProvider invPrv = new ProvLibInventario.Provider("localhost","pita");
            //var r01 = invPrv.Producto_GetFicha("0000000450");

            //var filt = new DtoLibInventario.Producto.Filtro();
            //filt.cadena = "MUÑECA";
            //filt.MetodoBusqueda = DtoLibInventario.Producto.Enumerados.EnumMetodoBusqueda.Nombre;
            //var rt1 = invPrv.Producto_GetLista(filt);

            //filt.estatus= DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo;
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
            //var filtroMov = new DtoLibInventario.Movimiento.Traslado.Consultar.Filtro() { autoDeposito = "0000000002" };
            //var rt1 = invPrv.Producto_Movimiento_Traslado_Consultar_ProductosPorDebajoNivelMinimo(filtroMov);
            //var fichaIns = new DtoLibInventario.Movimiento.Traslado.Insertar.Ficha();
            //var rt2 = invPrv.Producto_Movimiento_Traslado_Insertar(fichaIns);
            //var filtro = new DtoLibInventario.Reportes.Movimientos.Filtro() {};
            //var rt1 = invPrv.CajaBanco_ArqueoVentaPos(filtro);
            //var filtro = new DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Filtro();
            //filtro.autoDeposito = "0000000002";
            //var rt1 = invPrv.Deposito_GetFicha("0000000002");
            // var rt1= invPrv.Producto_GetExistencia("0000001141"); 
            //var rt1 = invPrv.Producto_GetPrecio("0000001141"); 

            //var filtroMov = new DtoLibInventario.Movimiento.Traslado.Consultar.Filtro() { autoDeposito = "0000000001" };
            //var rt1 = invPrv.Producto_Movimiento_Traslado_Consultar_ProductosPorDebajoNivelMinimo(filtroMov);

            //var filtroPrecio = new DtoLibInventario.Precio.Historico.Filtro() { autoProducto = "0000000231" };
            //var rt1 = invPrv.Producto_Precio_Historico_Lista(filtroPrecio);

            //var filtro= new DtoLibInventario.Costo.Historico.Filtro () { autoProducto = "0000000231" };
            //var rt1 = invPrv.Producto_Costo_Historico_Lista(filtro);
            //var rt1 = invPrv.Producto_GetDepositos("0000000231");
            //var rt1 = invPrv.Producto_Editar_GetFicha ("0000000231");
            //var rt1 = invPrv.Producto_Clasificacion_Lista ();
            //var rt1 = invPrv.Configuracion_ForzarRedondeoPrecioVenta ();
            //var rt1 = invPrv.Configuracion_PreferenciaRegistroPrecio();
            //var rt1 = invPrv.Producto_CambiarEstatusA_Activo("0000000013");
            //var rt1 = invPrv.Producto_Verificar_ExistenciaEnCero("0000000331");

            //var filtro = new DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Filtro() { autoDeposito="0000000001" };
            //var rt1 = invPrv.Tools_AjusteNivelMinimoMaximo_GetLista(filtro);

            //var filtroKardex = new DtoLibInventario.Kardex.Movimiento.Resumen.Filtro() 
            //{ 
            //    autoProducto = "0000001968",
            //    //autoConcepto = "0000000001",
            //    //autoDeposito = "0000000001",
            //    //modulo = "Ventas",
            //    ultDias = DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._45Dias,
            //};
            //var rt1 = invPrv.Producto_Kardex_Movimiento_Lista_Resumen(filtroKardex);

            //var filtro = new DtoLibInventario.Proveedor.Lista.Filtro()
            //{
            //    cadena = "*ALIMENTO",
            //    MetodoBusqueda = DtoLibInventario.Proveedor.Enumerados.EnumMetodoBusqueda.Nombre
            //};
            //var r01 = invPrv.Proveedor_GetLista(filtro);

            //var filtroKardex = new DtoLibInventario.Kardex.Movimiento.Resumen.Filtro() { autoProducto = "0000000017", ultDias = DtoLibInventario.Kardex.Enumerados.EnumMovUltDias._60Dias };
            //var rt1 = invPrv.Producto_Kardex_Movimiento_Lista_Resumen(filtroKardex);

            //var rt1 = invPrv.Producto_GetPrecio ("0000000317");

            //var filt = new DtoLibInventario.Visor.Existencia.Filtro();
            //filt.filtrarPor = DtoLibInventario.Visor.Existencia.Enumerados.enumFiltrarPor.ExistenciaPorDebajoNivelMinimo;
            //var rt1 = invPrv.Visor_Existencia(filt);

            //var filt = new DtoLibInventario.Visor.CostoEddad.Filtro();
            //var rt1 = invPrv.Visor_CostoEdad(filt);

            //var filt = new DtoLibInventario.Visor.Traslado.Filtro();
            //filt.ano = 2020;
            //filt.mes = 8;
            //var rt1 = invPrv.Visor_Traslado(filt);

            //var filt = new DtoLibInventario.Visor.Ajuste.Filtro();
            //filt.ano = 2020;
            //filt.mes = 10;
            //var rt1 = invPrv.Visor_Ajuste(filt);

            //var rt1 = invPrv.Configuracion_CostoEdadProducto ();

            //var filtro = new DtoLibInventario.Visor.Existencia.Filtro();
            //var rt1 = invPrv.Visor_Existencia (filtro);

            //var ficha = new DtoLibInventario.Movimiento.Anular.Cargo.Ficha()
            //{
            //    autoDocumento = "0000012344",
            //    autoSistemaDocumento = "0000000024",
            //    autoUsuario = "0000000008",
            //    codigo = "02",
            //    estacion = "DESARROLLOMOVIL",
            //    motivo = "PRUEBA",
            //    usuario = "FFF",
            //};
            //var rt1 = invPrv.Producto_Movimiento_Cargo_Anular(ficha);

            //var filt = new DtoLibInventario.Reportes.MaestroInventario.Filtro();
            //filt.estatus = DtoLibInventario.Reportes.enumerados.EnumEstatus.Activo;
            //var rt1 = invPrv.Reportes_MaestroInventario(filt);

            //var filt = new DtoLibInventario.Reportes.Top20.Filtro();
            //filt.Desde = new DateTime(2020, 08, 01).Date;
            //filt.Hasta = new DateTime(2020, 09, 30).Date;
            //filt.Modulo = DtoLibInventario.Reportes.enumerados.EnumModulo.Ventas ;
            //filt.autoDeposito = "0000000005";
            //var rt1 = invPrv.Reportes_Top20(filt);

            //var rt1 = invPrv.Permiso_AsignarDepositos("0000000001");
            //var rt1 = invPrv.Permiso_PedirClaveAcceso_NivelMaximo();
            //var rt2 = invPrv.Permiso_PedirClaveAcceso_NivelMedio();
            //var rt3 = invPrv.Permiso_PedirClaveAcceso_NivelMinimo();

            //var ficha = new DtoLibInventario.Usuario.Buscar.Ficha() { clave = "123", codigo = "c1" };
            //var rt1 = invPrv.Usuario_Buscar(ficha);

            //var filt = new DtoLibInventario.Reportes.MaestroExistencia.Filtro();
            //filt.autoDeposito = "0000000004";
            //filt.autoDepartamento = "0000000003";
            //var ficha = invPrv.Reportes_MaestroExistencia(filt);

            //var ficha = invPrv.Empresa_Datos ();

            //var filt = new DtoLibInventario.Reportes.MaestroInventario.Filtro();
            //filt.autoDeposito = "0000000004";
            //var ficha = invPrv.Reportes_MaestroInventario (filt);

            //var filt = new DtoLibInventario.Reportes.MaestroPrecio.Filtro();
            //var ficha = invPrv.Reportes_MaestroPrecio(filt);

            //var filt = new DtoLibInventario.Reportes.Kardex.Filtro();
            //filt.desde = new DateTime(2020, 11, 01);
            //filt.hasta = new DateTime(2020, 11, 05);
            //filt.autoProducto = "0000000027";
            //var ficha = invPrv.Reportes_Kardex (filt);

            //var filt = new DtoLibInventario.Reportes.CompraVentaAlmacen.Filtro();
            //filt.autoProducto = "0000000216";
            //var ficha = invPrv.Reportes_CompraVentaAlmacen(filt);
            //var ficha = invPrv.Reportes_DepositoResumen();

            //var filt = new DtoLibInventario.Reportes.MaestroExistencia.Filtro();
            //var ficha = invPrv.Reportes_MaestroExistencia(filt);

            //var filt = new DtoLibInventario.Reportes.MaestroInventario.Filtro();
            //filt.autoDeposito = "0000000004";
            //var ficha = invPrv.Reportes_MaestroInventario(filt);

            //var ficha = invPrv.Reportes_DepositoResumen();

            //var filt = new DtoLibInventario.Reportes.CompraVentaAlmacen.Filtro();
            //filt.autoProducto = "0000000154";
            //var ficha = invPrv.Reportes_CompraVentaAlmacen(filt);

            //var filt = new DtoLibInventario.Movimiento.Lista.Filtro();
            //filt.Desde = new DateTime(2020,11,01);
            //filt.Hasta = new DateTime(2020,11,30);
            //filt.IdDepDestino = "0000000004";
            //var ficha = invPrv.Producto_Movimiento_GetLista (filt);

            //var filt = new DtoLibInventario.Reportes.MaestroProducto.Filtro();
            //filt.autoDeposito = "0000000013";
            //var ficha = invPrv.Reportes_MaestroProducto(filt);

            //var filtro = new DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Filtro()
            //{
            //    autoDeposito = "0000000008",
            //    cadena = "PANT",
            //};
            //var rt1 = invPrv.Tools_AjusteNivelMinimoMaximo_GetLista(filtro);

            //var filtro = new DtoLibInventario.Reportes.MaestroNivelMinimo.Filtro ()
            //{
            //    autoDeposito = "0000000001",
            //};
            //var rt1 = invPrv.Reportes_NivelMinimo(filtro);

            //var filtro = new DtoLibInventario.Analisis.General.Filtro()
            //{
            //    autoDeposito = "0000000006",
            //    modulo = DtoLibInventario.Analisis.Enumerados.EnumModulo.Ventas,
            //    ultimosXDias = 15,
            //};
            //var rt1 = invPrv.Producto_Analisis_General(filtro);

            //var filtro = new DtoLibInventario.Analisis.Detallado.Filtro()
            //{
            //    autoDeposito = "0000000006",
            //    modulo = DtoLibInventario.Analisis.Enumerados.EnumModulo.Ventas,
            //    ultimosXDias = 15,
            //    autoProducto = "0000000025",
            //};
            //var rt1 = invPrv.Producto_Analisis_Detallado(filtro);

            //var filtro = new DtoLibInventario.Analisis.Existencia.Filtro()
            //{
            //    autoDeposito = "0000000006",
            //};
            //var rt1 = invPrv.Producto_Analisis_Existencia(filtro);

            //var rt1 = invPrv.Grupo_GetListaByDepartamento("0000000002");

            //var rt1 = invPrv.Configuracion_MetodoCalculoUtilidad_CapturarData();

            //var filtro = new DtoLibInventario.Reportes.Valorizacion.Filtro()
            //{
            //    hasta = new DateTime(2021, 02, 15),
            //    idDeposito="0000000004",
            //};
            //var rt1 = invPrv.Reportes_Valorizacion(filtro);

            //var ficha = new DtoLibInventario.Auditoria.Buscar.Ficha()
            //{
            //    autoDocumento = "0000000717",
            //    autoTipoDocumento = "0000000026",
            //};
            //var rt1 = invPrv.Auditoria_Documento_GetFichaBy(ficha);

            //var rt1 = invPrv.Sistema_TipoDocumento_GetFichaById("0000000026");

            //var rt1 = invPrv.Departamento_Eliminar("0000000010");
            //var rt1 = invPrv.Permiso_EliminarDepartamento("0000000010");

            //var rt1 = invPrv.Configuracion_HabilitarPrecio_5_ParaVentaMayorPos();

            //var filtro = new DtoLibInventario.Visor.Precio.Filtro();
            //var rt1 = invPrv.Visor_Precio(filtro);

            //var filtro = new DtoLibInventario.MonitorPos.Lista.Filtro() { codSucursal = "08" };
            //var rt1 = invPrv.MonitorPos_VentaResumen_GetLista(filtro);

            //var ficha = new DtoLibInventario.Departamento.Editar() { codigo ="02", nombre="VIVERES", auto="0000000003"};
            //var rt1 = invPrv.Departamento_Editar(ficha);

            //var ficha = new DtoLibInventario.Grupo.Editar() { codigo = "AZUCAR", nombre = "AZUCAR", auto = "0000000005" };
            //var rt1 = invPrv.Grupo_Editar(ficha);

            //var filtro0 = new DtoLibInventario.Movimiento.Traslado.Consultar.Filtro() { autoDeposito = "0000000008", autoDepartamento = "" };
            //var rt0 = invPrv.Producto_Movimiento_Traslado_Consultar_ProductosPorDebajoNivelMinimo (filtro0);

            //var filtro1 = new DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo.Filtro() { autoDepositoVerificarNivel = "0000000008", autoDepartamento = "", autoDepositoOrigen = "0000000001" };
            //var rt1 = invPrv.Capturar_ProductosPorDebajoNivelMinimo(filtro1);

            //var filt = new DtoLibInventario.Producto.Filtro();
            //filt.cadena = "HARINA";
            //filt.MetodoBusqueda = DtoLibInventario.Producto.Enumerados.EnumMetodoBusqueda.Nombre;
            //var rt1 = invPrv.Producto_GetLista(filt);

            //var filt = new DtoLibInventario.Reportes.Kardex.Filtro ();
            //filt.desde= new DateTime(2021,10,01).Date;
            //filt.hasta = DateTime.Now.Date;
            //filt.autoProducto = "0000000649";
            //filt.autoDeposito = "0000000008";
            //var rt1 = invPrv.Reportes_KardexResumen (filt);

            //var rt1 = invPrv.Deposito_GetListaBySucursal("01");
            //var filt = new DtoLibInventario.Visor.CostoEddad.Filtro() { autoDeposito = "0000000001", };
            //var rt1 = invPrv.Visor_CostoEdad (filt);

            //var rt1 = invPrv.Configuracion_VisualizarProductosInactivos();

            //var filt = new DtoLibInventario.Reportes.Kardex.Filtro()
            //{
            //    desde = new DateTime(2022, 02, 01),
            //    hasta = new DateTime(2022, 02, 28),
            //    autoDeposito="0000000001",
            //    autoProducto="0000000481",
            //};
            //var rt1 = invPrv.Reportes_Kardex(filt);

            //var filt = new DtoLibInventario.Movimiento.AjusteInvCero.Capture.Filtro()
            //{
            //    idDeposito = "0000000004",
            //};
            //var rt1 = invPrv.Producto_Movimiento_AjusteInventarioCero_Capture(filt);

            //var rt1 = invPrv.Usuario_GetClave_ById("");

        }

    }

}