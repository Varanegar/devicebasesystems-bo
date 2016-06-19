using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.Helpers.Entity
{
    public class PMCStoreConfigEntity
    {
        public int AppUserId { get; set; }
        public int SLId { get; set; }
        public int SalesmanId { get; set; }
        public int StockId { get; set; }
        public int CustomerId { get; set; }
        public int CenterId { get; set; }
        public string SaleFinalDate { get; set; }
        public string TreasuryFinalDate { get; set; }
        public string ConnectionString { get; set; }
        public string UniqueId { get; set; }
        [Ignore]
        public int FiscalYearId { get; set; }
        public int CustomerGroupId { get; set; }
        public int CustomerTypeId { get; set; }
    }
}
