using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class Product : DeviceBaseModel
    {
        [StringLength(20)]
        public string ProductCode { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        public virtual ICollection<TrackingProduct> TrackingProducts { get; set; }

    }
}
