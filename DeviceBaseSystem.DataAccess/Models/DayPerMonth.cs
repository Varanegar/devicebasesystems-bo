using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceBaseSystem.DataAccess.Models
{
    public class DayPerMonth : DeviceBaseModel
    {
        [StringLength(200)]
        public string Title { get; set; }

    }
}
