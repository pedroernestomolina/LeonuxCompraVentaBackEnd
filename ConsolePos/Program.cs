﻿using System;
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

        }
    }

}