using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShipmentService;

namespace ShipmentAPI.Controllers
{

    public class ValuesController : ApiController
    {

        ShipmentRepository order = new ShipmentRepository();

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Route("api/GetDetailsByCompanyName/{CompanyName}")]
        public List<OrderInfo> GetDetailsByCompanyName(string CompanyName)
        {
            return order.GetOrderDetailsByCompanyName(CompanyName);
        }

        [Route("api/GetDetailsByUserName/{username}")]
        public List<OrderInfo> GetDetailsByUserName(string UserName)
        {
            return order.GetOrderDetailsByUserName(UserName);
        }
        

        // POST api/values

        public string Post([FromBody]OrderInfo value)
        {
            return order.AddOrderDetails(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [Route("api/deleteorders/{CompanyName}")]
        public string Delete(string companyname)
        { 
            return order.DeleteOrder(companyname);
        }

        [Route("api/deleteproducts/{upccode}")]
        public string DeleteProducts(string upccode)
        {
            return order.DeleteProducts(upccode);
        }
    }
}
