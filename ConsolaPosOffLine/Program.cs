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

            IPosOffLine.IProvider _offLine = new ProvSqLitePosOffLine.Provider(@"D:\Proyectos FoxSystem\CompraVenta\LeonuxPosOffLine.db");
            _offLine.setServidorRemoto("localhost", "galpon2904");
            //var fechaActual = _offLine.FechaServidor();
            //var r01 = _offLine.Producto("0000000005");
            var r01 = _offLine.Servidor_Test();
            var r02 = _offLine.Servidor_ActualizarData();
        }
    }
}
