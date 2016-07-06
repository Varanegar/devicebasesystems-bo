using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class PurchaseOrder : DeviceBaseModel
    {
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("TrackingProduct")]
        public Guid TrackingProductId { get; set; }
        public virtual TrackingProduct TrackingProduct { get; set; }

        [ForeignKey("ValidityDuration")]
        public Guid ValidityDurationId { get; set; }
        public virtual ValidityDuration ValidityDuration { get; set; }

        public DateTime OrderDate { get; set; }
        public string OrderPDate { get; set; }
        public TimeSpan OrderTime { get; set; }

        public int PurchaseOrderStatus { get; set; }

        [ForeignKey("PurchaseOrderPayment")]
        public Guid PurchaseOrderPaymentId { get; set; }
        public virtual PurchaseOrderPayment PurchaseOrderPayment { get; set; }

        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }


    }
}
