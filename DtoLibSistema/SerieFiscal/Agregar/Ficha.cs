using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.SerieFiscal.Agregar
{
    
    public class Ficha
    {

        public string serie { get; set; }
        public int correlativo { get; set; }
        public string control { get; set; }
        public string estatusFactura { get; set; }
        public string estatusNtDebito { get; set; }
        public string estatusNtCredito{ get; set; }
        public string estatusNtEntrega { get; set; }


        public Ficha()
        {
            serie = "";
            control = "";
            correlativo = 0;
            estatusFactura = "";
            estatusNtCredito = "";
            estatusNtDebito = "";
            estatusNtEntrega = "";
        }

    }

}