using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsolaPosOffLine
{

    class Program
    {

        static void Main(string[] args)
        {
            //IPosOffLine.IProvider _offLine = new ProvSqLitePosOffLine.Provider(@"C:\Modulos Leonux\POS\Data\Leonux.db");
            //IPosOffLine.IProvider _offLine = new ProvSqLitePosOffLine.Provider(@"D:\Proyectos FoxSystem\CompraVenta\LeonuxPosOffLine.db");
            IPosOffLine.IProvider _offLine = new ProvSqLitePosOffLine.Provider(@"C:\Modulos Leonux\POS\Data\LeonuxPosOffLine.db");
            _offLine.setServidorRemoto("localhost", "bodaraguita");

            //var fechaActual = _offLine.FechaServidor();
            //var r01 = _offLine.Producto("0000000005");
            //var r01 = _offLine.Servidor_Test();
            //var r02 = _offLine.Servidor_Principal_CrearBoletin("/var/lib/mysql-files/");
            //var r03 = _offLine.Servidor_Principal_InsertarCierre("/var/lib/mysql-files/");
            //var r01 = _offLine.Operador_Movimientos(62);
            //var r01 = _offLine.Servidor_Principal_PreprararCierre("02","c:/leonux/bandeja","c:/leonuxFtp/bandeja/data");
            //var r01 = _offLine.Servidor_Principal_InsertarBoletin("02", @"c:/leonux/bandeja/temp");
            //var r02 = _offLine.Servidor_Principal_InsertarCierre("/var/lib/mysql-files/");
            //var filtro = new DtoLibPosOffLine.Reporte.Pago.Filtro()
            //{
            //    IdOperador = 78,
            //};
            //var r01 = _offLine.Reporte_Pago_Detalle(filtro);

            //var r02 = _offLine.Servidor_Principal_InsertarCierre("/var/lib/mysql-files/");

            //var r01 = _offLine.Configuracion_ActualizarClaveAcceso_Clave_1();
            //if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            //    Console.WriteLine(r01.Mensaje);
            //else
            //    Console.WriteLine("TODO OK");
            //Console.ReadKey();

            //var filtro= new DtoLibPosOffLine.Cliente.ExportarData.Filtro ();
            //var r01 = _offLine.Cliente_ExportarData (filtro);

            //var r01 = _offLine.Servidor_AgregarCampoTabla_Sistema_Habilitar_Precio_VentaMayor();
            //if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            //{
            //    Console.WriteLine(r01.Mensaje);
            //}
            //else
            //{
            //    Console.WriteLine("CAMPO CREADO SATISFACTORIAMENTE");
            //}
            //Console.ReadKey();

            
            //var xfiltro = new DtoLibPosOffLine.Monitor.ResumenDia.Filtro(){ equipo="01", idOperador=367};
            //var rx1 = _offLine.Monitor_Resumen_Dia(xfiltro);

            //var xfiltro2 = new DtoLibPosOffLine.Monitor.ResumenDia.Filtro() { equipo = "02", idOperador = 366 };
            //var rx2 = _offLine.Monitor_Resumen_Dia(xfiltro2);

            //var xr = _offLine.TestBD_Local();

            //var r00 = _offLine.Monitor_ListaResumen();
            //r00.Lista.Add(new DtoLibPosOffLine.Monitor.ListaResumen.Ficha());

            //foreach (DtoLibPosOffLine.Monitor.ListaResumen.Ficha rCierre in r00.Lista)
            //{
            //    var xcierre = rCierre.cierreGenerar;
            //    var filtro = new DtoLibPosOffLine.Monitor.GenerarResumen.Filtro()
            //    {
            //        cierre = xcierre,
            //    };
            //    var r01 = _offLine.Monitor_GenerarResumen(filtro);

            //    var list = r01.Lista.Select(s =>
            //    {
            //        var rg = new DtoLibPosOffLine.Monitor.SubirResumen.Detalle()
            //        {
            //            autoProducto = s.autoProducto,
            //            cnt = s.cnt,
            //        };
            //        return rg;
            //    }).ToList();
            //    var ficha = new DtoLibPosOffLine.Monitor.SubirResumen.Ficha()
            //    {
            //        codSucursal = "08",
            //        cierre = xcierre,
            //        Lista = list,
            //    };
            //    var r02 = _offLine.Monitor_SubirResumen(ficha);

            //    var cierre = new DtoLibPosOffLine.Monitor.InsertarCierre.Ficha()
            //    {
            //        cierre = xcierre,
            //        estatus = "T",
            //    };
            //    var r03 = _offLine.Monitor_InsertarCierre(cierre);
            //}


            //var r01 = _offLine.Gestion_AgregarCampos();
            //if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            //{
            //    Console.WriteLine(r01.Mensaje);
            //}
            //else
            //{
            //    Console.WriteLine("CAMPO CREADO SATISFACTORIAMENTE");
            //}
            //Console.ReadKey();

            //var ficha = new DtoLibPosOffLine.Cliente.Editar.Ficha()
            //{
            //    Id = 6647,
            //    NombreRazaonSocial = "PEDRO ERNESTO MOLINA",
            //    DirFiscal = "CONJUNTO RESIDENCIAL EL PARQUE, SAN DIEGO",
            //    Telefono = "04144322860",
            //};
            //var r01 = _offLine.Cliente_Editar (ficha);

            //var ficha = new DtoLibPosOffLine.Cliente.Agregar()
            //{
            //    CiRif="V11351098",
            //    NombreRazaonSocial = "PEDRO MOLINA",
            //    DirFiscal = "SAN DIEGO",
            //    Telefono = "",
            //};
            //var r01 = _offLine.Cliente_Agregar(ficha);

        }

    }

}