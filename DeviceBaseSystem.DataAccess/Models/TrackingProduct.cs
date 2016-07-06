using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class TrackingProduct : DeviceBaseModel
    {
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        
        [StringLength(200)]
        public string ProductDescription { get; set; }

        public virtual Guid MinuteId { get; set; }
        public virtual Minute Minute { get; set; }

        public virtual Guid HourPerDayId { get; set; }
        public virtual HourPerDay HourPerDay { get; set; }

        public virtual Guid DayPerMonthId { get; set; }
        public virtual DayPerMonth DayPerMonth { get; set; }

        public decimal Price { get; set; }
        public bool Active { get; set; }
    }
}
