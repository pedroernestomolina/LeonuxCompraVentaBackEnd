using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Servidor.EnviarData
{
    
    public class CxCDocumento
    {

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public decimal Importe { get; set; }
        public string Operacion { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public int Dias { get; set; }
        public decimal CastigoP { get; set; }
        public decimal ComisionP { get; set; }

    }

}