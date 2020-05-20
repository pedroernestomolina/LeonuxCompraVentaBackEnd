using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Pendiente.DejarEnPendiente
{

    public class Agregar
    {

        public int IdCliente { get; set; }
        public decimal Monto { get; set; }
        public int Renglones { get; set; }
        public List<AgregarItem> Items { get; set; }

    }

}