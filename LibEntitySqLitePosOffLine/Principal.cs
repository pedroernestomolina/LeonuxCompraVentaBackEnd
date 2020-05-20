using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibEntitySqLitePosOffLine
{

    public partial class LeonuxPosOffLineEntities : DbContext
    {

        public LeonuxPosOffLineEntities(string cn)
            : base(cn)
        {
        }

    }

}