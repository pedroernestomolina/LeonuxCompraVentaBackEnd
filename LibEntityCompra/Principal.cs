using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibEntityCompra
{

    public partial class compraEntities : DbContext
    {
        public compraEntities(string cn)
            : base(cn)
        {
        }

    }

}