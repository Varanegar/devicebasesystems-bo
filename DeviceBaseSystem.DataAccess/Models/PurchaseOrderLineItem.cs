using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class PurchaseOrderLineItem : DeviceBaseModel
    {
        [ForeignKey("PurchaseOrder")]
        public virtual Guid PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        [ForeignKey("CompanyDevice")]
        public virtual Guid CompanyDeviceId { get; set; }
        public virtual CompanyDevice CompanyDevice { get; set; }


    }
}
