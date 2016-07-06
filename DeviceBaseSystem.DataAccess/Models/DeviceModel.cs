using System.ComponentModel.DataAnnotations;
using Anatoli.Common.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class DeviceModel : DeviceBaseModel
    {
        [StringLength(20)]
        public string DeviceCode { get; set; }

        [StringLength(200)]
        public string DeviceName { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }

    }
}
