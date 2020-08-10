using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleCajaBanco
{

    class Program
    {
        static void Main(string[] args)
        {
            ILibCajaBanco.IProvider cajaBancoPrv = new ProvLibCajaBanco.Provider("localhost","panda");
            //var filtro = new DtoLibCajaBanco.Reporte.Movimiento.Filtro();
            //filtro.desdeFecha = new DateTime(2020, 07, 01);
            //filtro.hastaFecha = new DateTime(2020, 07, 20);
            //var r01 = cajaBancoPrv.CajaBanco_ArqueoCajaPos(filtro);
            //var r02 = cajaBancoPrv.Usuario_Principal();
            var fecha = new DateTime(2020, 08, 05);
            cajaBancoPrv.Reporte(fecha);
        }
    }

}