using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleSistema
{

    class Program
    {

        static void Main(string[] args)
        {
            ILibSistema.IProvider sistPrv = new ProvLibSistema.Provider("localhost","pita");
            //var r01 = sistPrv.SucursalGrupo_GetLista();
            //var ficha = new DtoLibSistema.GrupoSucursal.Editar()
            //{
            //    auto="0000000003",
            //    nombre = "MAYOR",
            //    precioId = "4",
            //};
            //var r01 = sistPrv.SucursalGrupo_Editar(ficha);
            //var r01 = sistPrv.Sucursal_GetLista();
            //var r01 = sistPrv.Sucursal_GetFicha("0000000002");
            //var ficha = new DtoLibSistema.Sucursal.Editar()
            //{
            //    auto= "0000000004",
            //    autoGrupo = "0000000003",
            //    codigo = "MY-01",
            //    nombre = "MAYOR 01",
            //};
            //var r01 = sistPrv.Sucursal_Editar (ficha);
            //var ficha = new DtoLibSistema.Sucursal.AsignarDepositoPrincipal()
            //{
            //    auto = "0000000004",
            //    autoDepositoPrincipal = "0000000003",
            //};
            //var r01 = sistPrv.Sucursal_AsignarDepositoPrincipal(ficha);

            //var ficha = new DtoLibSistema.Deposito.Agregar()
            //{
            //    nombre = "VTA MAYOR TOCUYITO",
            //    codigo = "MY-TOCUYIT",
            //    autoSucursal = "0000000004",
            //    codigoSucursal = "MY-01",
            //};
            //var r01 = sistPrv.Deposito_Agregar(ficha);
            //var ficha = new DtoLibSistema.Precio.Etiquetar.Actualizar()
            //{
            //    descripcion_1 = "Pv1",
            //    descripcion_2 = "Pv2",
            //    descripcion_3 = "Pv3",
            //    descripcion_4 = "Pv4",
            //    descripcion_5 = "Pv5",
            //};
            //var r01 = sistPrv.Precio_Etiquetar_Actualizar(ficha);
            //var r01 = sistPrv.Precio_Etiquetar_GetFicha();
            //var r01 = sistPrv.Sucursal_GetLista();
            //var r00= sistPrv.Funcion_GetLista();
            //var list = new List<DtoLibSistema.GrupoUsuario.Permiso>();
            //foreach(var it in r00.Lista.OrderBy(o=>o.codigo).ToList())
            //{
            //    var p= new DtoLibSistema.GrupoUsuario.Permiso()
            //    {
            //         codigoFuncion=it.codigo,
            //         estatus="0",
            //         seguridad="Ninguna",
            //    };
            //    list.Add(p);
            //}
            //var ficha = new DtoLibSistema.GrupoUsuario.Agregar()
            //{
            //    nombre = "INVENTARIO",
            //    permisos = list,
            //};
            //var r01 = sistPrv.GrupoUsuario_Agregar(ficha);
            //var ficha = new DtoLibSistema.Configuracion.InicializarBD.Ficha()
            //{
            //    CodSucursal = "01",
            //    IdEquipo = "01",
            //};
            //var r01 = sistPrv.Inicializar_BD(ficha);
            //var r01 = sistPrv.Sucursal_GeneraCodigoAutomatico();
            //var r01 = sistPrv.Deposito_GeneraCodigoAutomatico ();

            //var r01 = sistPrv.Empresa_Datos();
            //var ficha = new DtoLibSistema.Usuario.Buscar.Ficha() { codigo = "01", clave = "" };
            //var r01 = sistPrv.Usuario_Buscar(ficha);
            //var r01 = sistPrv.Permiso_ToolSistema("0000000004");
            //var r01 = sistPrv.Configuracion_TasaRecepcionPos();
            //var ficha = new DtoLibSistema.Configuracion.ActualizarTasaRecepcionPos.Ficha() { ValorNuevo = 520000 };
            //var r01 = sistPrv.Configuracion_Actualizar_TasaRecepcionPos (ficha);
            //var r01 = sistPrv.Configuracion_Actualizar_TasaDivisa_CapturarData();

            //var filtro1 = new DtoLibSistema.Vendedor.Lista.Filtro();
            //var r01 = sistPrv.Vendedor_GetLista(filtro1);

            //var filtro2 = new DtoLibSistema.Cobrador.Lista.Filtro();
            //var r02 = sistPrv.Cobrador_GetLista(filtro2);

            //var filtro = new DtoLibSistema.SerieFiscal.Lista.Filtro();
            //var r02 = sistPrv.SerieFiscal_GetLista(filtro);

            //var r01 = sistPrv.SerieFiscal_GetFicha_ById("0000000001");

            //var r01 = sistPrv.ReconversionMonetaria_GetData();
            //var r01 = sistPrv.ReconversionMonetaria_GetCount();
        }

    }

}