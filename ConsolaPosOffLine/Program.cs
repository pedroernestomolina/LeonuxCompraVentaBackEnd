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
            IPosOffLine.IProvider _offLine = new ProvSqLitePosOffLine.Provider(@"C:\Modulos Leonux\POS\Data\LeonuxPosOffLine.db");
            //IPosOffLine.IProvider _offLine = new ProvSqLitePosOffLine.Provider(@"D:\Proyectos FoxSystem\CompraVenta\LeonuxPosOffLine.db");
            _offLine.setServidorRemoto("localhost", "bodega");

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

            //var filtro = new DtoLibPosOffLine.ResumenVentaPos.Generar.Filtro()
            //{
            //    idOperador = 319,
            //    sucEquipo = "0B01",
            //};
            //var r01 = _offLine.VentaDocumento_Resumen_Generar(filtro);

            //var filtro2 = new DtoLibPosOffLine.ResumenVentaPos.Generar.Filtro()
            //{
            //    idOperador = 329,
            //    sucEquipo = "0601",
            //};
            //var r02 = _offLine.VentaDocumento_Resumen_Generar(filtro2);

            var filtro3 = new DtoLibPosOffLine.ResumenVentaPos.Lista.Filtro
            {
                codSucursal = "08",
            };
            var r03 = _offLine.VentaDocumento_Resumen_GetLista(filtro3);

            var ficha = new DtoLibPosOffLine.ResumenVentaPos.Subir.Ficha()
            {
                codSucursal = "08",
                detalles = r03.Lista.Select(s =>
                {
                    var nr = new DtoLibPosOffLine.ResumenVentaPos.Subir.Detalle()
                    {
                        autoProducto = s.autoProducto,
                        cnt = s.cnt,
                    };
                    return nr;
                }).ToList(),
            };
            var r04 = _offLine.VentaDocumento_Resumen_Subir(ficha);
        }

    }

}