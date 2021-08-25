using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsolePos
{

    class Program
    {
        static void Main(string[] args)
        {

            IPos.IProvider posProv = new ProvPos.Provider("localhost", "pita");
            //var r01= posProv.Producto_GetLista();

            //var filtro = new DtoLibPos.Transporte.Lista.Filtro();
            //var r01 = posProv.Transporte_GetLista (filtro);

            //var filtro = new DtoLibPos.Cobrador.Lista.Filtro();
            //var r01 = posProv.Cobrador_GetLista(filtro);

            //var filtro = new DtoLibPos.Vendedor.Lista.Filtro();
            //var r01 = posProv.Vendedor_GetLista(filtro);

            //var filtro = new DtoLibPos.MedioPago.Lista.Filtro();
            //var r01 = posProv.MedioPago_GetLista(filtro);

            //var filtro = new DtoLibPos.Concepto.Lista.Filtro();
            //var r01 = posProv.Concepto_GetLista(filtro);

            //var filtro = new DtoLibPos.Sucursal.Lista.Filtro();
            //var r01 = posProv.Sucursal_GetLista(filtro);

            //var filtro = new DtoLibPos.Deposito.Lista.Filtro();
            //var r01 = posProv.Deposito_GetLista(filtro);

            //var filtro = new DtoLibPos.Cliente.Lista.Filtro()
            //{
            //    cadena = "NO CON",
            //    preferenciaBusqueda = DtoLibPos.Cliente.Lista.Enumerados.enumPreferenciaBusqueda.Nombre,
            //};
            //var r01 = posProv.Cliente_GetLista(filtro);

            //var r01 = posProv.Cliente_GetFichaById("0000000001");

            //var r01 = posProv.Cliente_GetFichaByCiRif("NA");

            //var ficha = new DtoLibPos.Usuario.Identificar.Ficha()
            //{
            //    codigo = "CARAGUITA",
            //    clave = "123",
            //};
            //var r01 = posProv.Usuario_Identificar(ficha);
            //var r02 = posProv.Permiso_IngresarPos(r01.Entidad.idGrupo);

            //var filtro = new DtoLibPos.Fiscal.Lista.Filtro();
            //var r01 = posProv.Fiscal_GetTasas(filtro);

            //var r01 = posProv.Configuracion_FactorDivisa();


            //var r01 = posProv.Jornada_Verificar_Cerrer(3);
            //var fichaCierre = new DtoLibPos.Pos.Cerrar.Ficha()
            //{
            //    idOperador = 3,
            //    estatus = "C",
            //    arqueoCerrar = new DtoLibPos.Pos.Cerrar.Arqueo()
            //    {
            //        autoArqueo = "0110000001",
            //    },
            //};
            //var r02 = posProv.Jornada_Cerrar(fichaCierre);

            //var r01 = posProv.Jornada_Verificar_Abrir("1");
            //var ficha = new DtoLibPos.Pos.Abrir.Ficha()
            //{
            //    idEquipo = "1",
            //    idSucursal = "01",
            //    operadorAbrir = new DtoLibPos.Pos.Abrir.Operador()
            //    {
            //        estatus = "A",
            //        idEquipo = "1",
            //        idUsuario = "0000000002",
            //    },
            //    arqueoAbrir = new DtoLibPos.Pos.Abrir.Arqueo()
            //    {
            //        idUsuario = "0000000002",
            //        codUsuario = "C1",
            //        nombreUsuario = "PEDRO",
            //    },
            //    resumenAbrir = new DtoLibPos.Pos.Abrir.Resumen(),
            //};
            //var r02 = posProv.Jornada_Abrir(ficha);

            //var filtro = new DtoLibPos.Producto.Lista.Filtro()
            //{
            //    Cadena = "ACEITE",
            //    AutoDeposito = "0000000004",
            //    IdPrecioManejar = "5",
            //};
            //var r01 = posProv.Producto_GetLista(filtro);

            //var auto="0000000001";
            //var ficha = new DtoLibPos.Configuracion.Actualizar.Ficha()
            //{
            //    idClaveUsar = "1",
            //    idCobrador = auto,
            //    idConceptoDevVenta = "0000000003",
            //    idConceptoSalida = "0000000013",
            //    idConceptoVenta = "0000000001",
            //    idDeposito = "0000000004",
            //    idMedioPagoDivisa = "0000000002",
            //    idMedioPagoEfectivo = "0000000001",
            //    idMedioPagoElectronico = "0000000003",
            //    idMedioPagoOtros = "0000000009",
            //    idPrecioManejar = "1",
            //    idSucursal = "0000000004",
            //    idTransporte = auto,
            //    idVendedor = auto,
            //};
            //var r02 = posProv.Configuracion_Pos_Actualizar(ficha);
            //var r01 = posProv.Configuracion_Pos_GetFicha();

            //var r01 = posProv.Jornada_EnUso_GetByIdEquipo("1");

            //var ficha = new DtoLibPos.Venta.Anular.Ficha();
            //ficha.items = new List<DtoLibPos.Venta.Anular.FichaItem>();
            //ficha.items.Add(new DtoLibPos.Venta.Anular.FichaItem() { idOperador = 5, idItem = 7, });
            //ficha.items.Add(new DtoLibPos.Venta.Anular.FichaItem() { idOperador = 5, idItem = 8, });
            //ficha.itemDeposito = new List<DtoLibPos.Venta.Anular.FichaDeposito>();
            //ficha.itemDeposito.Add(new DtoLibPos.Venta.Anular.FichaDeposito(){ autoProducto="0000000073", autoDeposito="0000000004", cantUndBloq=1} );
            //ficha.itemDeposito.Add(new DtoLibPos.Venta.Anular.FichaDeposito(){ autoProducto="0000000040", autoDeposito="0000000004", cantUndBloq=1} );
            //var r01 = posProv.Venta_Anular(ficha);
            //var r01 = posProv.Sistema_ClaveAcceso_GetByIdNivel (4);

            //var filtro = new DtoLibPos.Documento.Lista.Filtro() { idArqueo = "0420000001" };
            //var r01 = posProv.Documento_Get_Lista (filtro);
            //var r01 = posProv.Documento_GetById("0420000028");

            //var r01 = posProv.Documento_Anular_Verificar("0430000059");
            //var r01 = posProv.Jornada_Resumen_GetByIdResumen(8);

            //var r01 = posProv.Documento_Get_MetodosPago("0440000059");
            //var r01 = posProv.Sistema_TipoDocumento_GetLista();
            //var r02 = posProv.Sistema_Serie_GetLista();

            //var filtro = new DtoLibPos.Deposito.Lista.Filtro() { PorCodigoSuc = "01", };
            //var r01 = posProv.Deposito_GetLista (filtro);

            //var r01 = posProv.Sistema_Empresa_GetFicha();

            //var filtro = new DtoLibPos.ClienteGrupo.Lista.Filtro();
            //var r01 = posProv.ClienteGrupo_GetLista(filtro);

            //var r02 = posProv.ClienteGrupo_GetFichaById ("0000000001");

            //var ficha = new DtoLibPos.ClienteGrupo.Editar.Ficha()
            //{
            //    auto = "0000000002",
            //    codigo = "01",
            //    nombre = "PRUEBA 1",
            //};
            //var r03 = posProv.ClienteGrupo_Editar(ficha);

            //var filtro = new DtoLibPos.ClienteZona.Lista.Filtro();
            //var r01 = posProv.ClienteZona_GetLista (filtro);

            //var r02 = posProv.ClienteZona_GetFichaById("0000000001");

            //var ficha = new DtoLibPos.ClienteZona.Agregar.Ficha()
            //{
            //    codigo = "01",
            //    nombre = "ZONA 2",
            //};
            //var r03 = posProv.ClienteZona_Agregar(ficha);

            //var ficha = new DtoLibPos.ClienteZona.Editar .Ficha()
            //{
            //    auto="0000000002",
            //    codigo = "02",
            //    nombre = "ZONA 02",
            //};
            //var r03 = posProv.ClienteZona_Editar(ficha);

            //var filtro = new DtoLibPos.Reportes.VentaAdministrativa.GeneralPorDepartamento.Filtro()
            //{
            //    codSucursal = "08",
            //    desde = new DateTime(2021, 5, 1),
            //    hasta = new DateTime(2021, 5, 1),
            //};
            //var r01 = posProv.ReportesAdm_GeneralPorDepartamento(filtro);

            //var r01 = posProv.Configuracion_BusquedaCliente();

            //var filtro = new DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumentoDetalle.Filtro()
            //{
            //    desdeFecha = new DateTime(2021, 05, 01),
            //    hastaFecha = new DateTime(2021, 06, 11),
            //    codigoSucursal = "",
            //    tipoDocNtCredito = true,
            //    tipoDocNtEntrega = true,
            //};
            //var r01 = posProv.Reporte_GenrealDocumentoDetalle(filtro);

            //var r01 = posProv.Sistema_Estado_GetLista();

            //var filtro = new DtoLibPos.Reportes.Clientes.Maestro.Filtro() 
            //{
            //    estatus="Inactivo"
            //};
            //var r01 = posProv.ReportesCli_Maestro (filtro);

            //var filtro = new DtoLibPos.Reportes.VentaAdministrativa.Consolidado.Filtro()
            //{
            //    desde = new DateTime(2021, 06, 01),
            //    hasta = new DateTime(2021, 06, 05),
            //    codSucursal = "",
            //};
            //var r01 = posProv.Reporte_Consolidado(filtro);

            //var r01 = posProv.Deposito_GetFicha_ByCodigo("08");
            //var r01 = posProv.Sucursal_GetFicha_ByCodigo("08");
            //var r01 = posProv.Jornada_EnUso_GetBy_EquipoSucursal ("1","08");

            //var filtro = new DtoLibPos.Reportes.POS.Filtro();
            //filtro.IdCierre = "0801001004";
            //var r01 = posProv.ReportePos_PagoDetalle(filtro);

        }

    }

}