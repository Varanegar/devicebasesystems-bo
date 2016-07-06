using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class CompanyDevice : DeviceBaseModel
    {
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("DeviceModel")]
        public Guid DeviceModelId { get; set; }
        public virtual DeviceModel DeviceModel { get; set; }

        public string IMEI { get; set; }


    }
}
