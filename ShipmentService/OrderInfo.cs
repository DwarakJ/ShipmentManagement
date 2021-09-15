using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService
{
    public class OrderInfo
    {
        public string CompanyName { get; set; }

        public string UserName { get; set; }

        public string UPCCode { get; set; }

        public DateTime? PackingDate { get; set; }
    }
}
