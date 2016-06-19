using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using System.Data;

namespace Anatoli.Rastak.DataAccess.Helpers.Entity
{
    public class RastakGetIdEntity
    {
        public string TableName { get; set; }
        [OutputParameterAttribute(DbType.Int32, 0)]
        public int Id { get; set; }
    }
}
