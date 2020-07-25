using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibEntityCajaBanco
{

    public partial class cajaBancoEntities : DbContext
    {

        public cajaBancoEntities(string cn)
            : base(cn)
        {
        }

    }

}