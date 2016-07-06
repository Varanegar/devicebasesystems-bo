using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class ValidityDuration : DeviceBaseModel
    {
        [StringLength(20)]
        public string DurationCode { get; set; }

        [StringLength(200)]
        public string DurationTitle { get; set; }

        public int DurationAmount { get; set; }
    }
}
