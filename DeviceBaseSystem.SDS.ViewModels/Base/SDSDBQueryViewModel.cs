using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.SDS.ViewModels;

namespace Anatoli.SDS.ViewModels.BaseModels
{
    public class SDSDBQueryViewModel : SDSBaseViewModel
    {
        public Guid UniqueId { get; set; }
        public string QueryName { get; set; }
        public string QueryTSQL { get; set; }
    }
}
