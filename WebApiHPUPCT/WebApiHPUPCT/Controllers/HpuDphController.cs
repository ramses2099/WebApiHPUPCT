using com.hit.webapi.hpu.dph.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Text;
using Newtonsoft.Json;

namespace com.hit.webapi.hpu.dph.Controllers
{
    public class HpuDphController : ApiController
    {

        // POST: api/HpuDph
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Post([FromBody]DataParam param)
        {
            String result = String.Empty;
            ResponseMsg response = null;
            try
            {
                NavisConnect service = new NavisConnect();
                result = service.executeGenericInvokeDPH_PERMISSON(param.UnitNbr, param.Action, param.Nota);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
            String[] arg = result.Split('|');

            if (arg.Length == 2)
            {

                //
                if (arg[0].Equals("OK"))
                {
                    response = new ResponseMsg() { Status = "OK", Codigo = "0", Message = arg[1] };
                }
                else
                {
                    response = new ResponseMsg() { Status = "OK", Codigo = "1", Message = arg[1] };
                }

            }
            return Ok(response);
        }


    }
}
