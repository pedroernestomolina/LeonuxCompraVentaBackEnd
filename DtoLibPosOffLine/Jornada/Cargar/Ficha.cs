using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Jornada.Cargar
{

    public class Ficha
    {

        public DateTime FechaApertura { get; set; }
        public string HoraApertura { get; set; }
        public Enumerado.EnumEstatus Estatus { get; set; }
        public Enumerado.EnumEstatusTrasmicion EstatusTransmicion { get; set; }
        public DateTime FechaCierre { get; set; }
        public string HoraCierre { get; set; }

    }

}