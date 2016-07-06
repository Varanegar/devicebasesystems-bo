using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class PurchaseOrderPayment : DeviceBaseModel
    {
        public DateTime PayDate { get; set; }
        [StringLength(10)]
        public string PayPDate { get; set; }
        public TimeSpan PayTime { get; set; }
        public decimal PayAmount { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }

    }
}
