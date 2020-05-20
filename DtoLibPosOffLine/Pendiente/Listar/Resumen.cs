using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Pendiente.Listar
{
    
    public class Resumen
    {

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public String Cliente { get; set; }
        public Decimal Monto { get; set; }
        public int Renglones { get; set; }

    }

}