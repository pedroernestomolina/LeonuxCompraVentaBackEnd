using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibEntitySistema
{

    public partial class sistemaEntities : DbContext
    {

        public sistemaEntities(string cn)
            : base(cn)
        {
        }

    }

}