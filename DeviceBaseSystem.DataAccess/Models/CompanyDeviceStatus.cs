using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class CompanyDeviceStatus : DeviceBaseModel
    {
        public string ValiityPDate { get; set; }

        [ForeignKey("TrackingProduct")]
        public Guid TrackingProductId { get; set; }
        public virtual TrackingProduct TrackingProduct { get; set; }

        [ForeignKey("CompanyDevice")]
        public Guid CompanyDeviceId { get; set; }
        public virtual CompanyDevice CompanyDevice { get; set; }

        public string LicenceCode { get; set; }
        
    }
}
