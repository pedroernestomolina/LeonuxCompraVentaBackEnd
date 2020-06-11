using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibPosOffLine.Servidor.EnviarData
{

    public class ProductoDeposito
    {

        public string AutoProducto { get; set; }
        public string AutoDeposito { get; set; }
        public decimal CantUnd { get; set; }


        public ProductoDeposito()
        {
            AutoProducto = "";
            AutoDeposito = "";
            CantUnd = 0.0m;
        }

    }

}