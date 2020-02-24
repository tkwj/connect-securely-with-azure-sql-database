using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightQuoteCloud
{
    public partial class ufcdataEntities : DbContext
    {
        public ufcdataEntities(EntityConnection connection)
            : base(connection, true)
        {
            Database.SetInitializer<ufcdataEntities>(null);
        }

    }
}
