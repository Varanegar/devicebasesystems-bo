using System;
using System.Linq;
using System.Collections.Generic;
using Thunderstruck;

namespace Anatoli.Rastak.ViewModels
{
    public class RastakBaseViewModel
    {
        [Ignore]
        public bool HasAttachment { get; set; }
        [Ignore]
        public bool HasNote { get; set; }
        [Ignore]
        public bool ReadOnly { get; set; }
        [Ignore]
        public bool IsAdded { get; set; }
        [Ignore]
        public bool IsDeleted { get; set; }
        [Ignore]
        public bool IsModified { get; set; }
        [Ignore]
        public bool IsSaveRequired { get; set; }
        [Ignore]
        public bool IsUnchanged { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int AppUserId { get; set; }
    }
}
