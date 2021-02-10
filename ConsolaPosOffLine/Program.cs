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
            IPosOffLine.IProvider _offLine = new ProvSqLitePosOffLine.Provider(@"C:\POS\DATA\LeonuxPosOffLine.db");
            _offLine.setServidorRemoto("localhost", "00000001");

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
        }

    }

}