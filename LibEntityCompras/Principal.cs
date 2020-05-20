using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibEntityCompras
{

    public partial class libComprasEntities : DbContext
    {
        public libComprasEntities (string cn)
            : base(cn)
        {
        }
    }

}