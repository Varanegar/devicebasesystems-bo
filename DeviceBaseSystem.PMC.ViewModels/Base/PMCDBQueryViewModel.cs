using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.PMC.ViewModels;

namespace Anatoli.PMC.ViewModels.BaseModels
{
    public class PMCDBQueryViewModel : PMCBaseViewModel
    {
        public Guid UniqueId { get; set; }
        public string AnatoliQueryName { get; set; }
        public string AnatoliQueryDataType { get; set; }
        public string QueryTSql { get; set; }
    }
}
