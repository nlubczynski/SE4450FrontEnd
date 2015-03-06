using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace FrontEndApp.Controllers
{
    public class SensorDataController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return "Must provide a sensor id";
        }

        public string Get(int id)
        {
            // Get the (last updated) raw data
            var list = MvcApplication.Instance.SensorReadings;

            // Filter on id
            var filtered = list.Where(reading => reading.SensorId == id);

            // Convert DateTime to unix time
            var selected = filtered.Select(reading => new double[] { 
                    (reading.Time - new DateTime(1970, 1, 1)).TotalMilliseconds, reading.Value 
                })
            .ToArray();

            // Serialize and return
            return JsonConvert.SerializeObject(selected);
        }

        public string GetAfter(int id, double unixTimeStamp)
        {            
            // Get the (last updated) raw data
            var list = MvcApplication.Instance.SensorReadings;

            // Filter on id and timestamp
            var timeComparator = new DateTime(1970,1,1).AddMilliseconds(unixTimeStamp);
            var filtered = list.Where(reading => reading.SensorId == id && reading.Time >= timeComparator);

            // Convert DateTime to unix time
            var selected = filtered.Select(reading => new double[] { 
                    (reading.Time - new DateTime(1970, 1, 1)).TotalMilliseconds, reading.Value 
                })
            .ToArray();

            // Serialize and return
            return JsonConvert.SerializeObject(selected);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}