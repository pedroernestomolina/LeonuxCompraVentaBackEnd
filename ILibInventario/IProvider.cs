using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{
    
    public interface IProvider: IProducto, IDeposito, IDepartamento, IGrupo, ITasaImpuesto
    {

    }

}
