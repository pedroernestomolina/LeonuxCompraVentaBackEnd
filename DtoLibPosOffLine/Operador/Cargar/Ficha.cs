using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Operador.Cargar
{
    
    public class Ficha
    {

        public int Id { get; set; }
        public int IdJornada { get; set; }
        public string AutoUsuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaApertura { get; set; }
        public string HoraApertura { get; set; }
        public Enumerado.EnumEstatus Estatus { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string HoraCierre { get; set; }

    }

}