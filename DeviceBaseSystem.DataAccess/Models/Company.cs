using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class Company : DeviceBaseModel
    {
        public int CompanyCode { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        public virtual ICollection<CompanyDevice> CompanyDevices { get; set; }

    }
}
