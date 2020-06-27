using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.VentaDocumento.Lista
{
    
    public class Resumen
    {

        public int Id { get; set; }
        public string Documento { get; set; }
        public string Control { get; set; }
        public DateTime FechaEmision { get; set; }
        public string HoraEmision { get; set; }
        public string NombreRazonSocial { get; set; }
        public string CiRif { get; set; }
        public decimal Monto { get; set; }
        public Enumerados.EnumTipoDocumento TipoDocumento { get; set; }
        public bool IsActivo { get; set; }
        public int Signo { get; set; }
        public int Renglones { get; set; }
        public string Serie { get; set; }

    }

}
