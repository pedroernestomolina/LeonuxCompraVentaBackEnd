using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.ControlAcceso.Actualizar
{
    
    public class Ficha
    {

        public List<ItemAcceso> ItemsAcceso { get; set; }


        public Ficha() 
        {
            ItemsAcceso = new List<ItemAcceso>();
        }

    }

}