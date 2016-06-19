using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.Rastak.ViewModels;

namespace Anatoli.Rastak.ViewModels.BaseModels
{
    public class RastakDBQueryViewModel : RastakBaseViewModel
    {
        public Guid UniqueId { get; set; }
        public string QueryName { get; set; }
        public string QueryTSQL { get; set; }
    }
}
