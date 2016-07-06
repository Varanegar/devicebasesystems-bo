using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Common.DataAccess.Models;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class Brand : DeviceBaseModel
    {
        [StringLength(20)]
        public string BrandCode { get; set; }

        [StringLength(200)]
        public string BrandName { get; set; }

        public virtual ICollection<DeviceModel> DeviceModels { get; set; }


    }
}
