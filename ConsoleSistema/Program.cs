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
        }

    }

}