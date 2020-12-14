using Biz.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Web.Script.Services;
using System.Web.Services;

namespace WebServiceFirst
{
	[WebService(Namespace = "http://ws.first.local/")]
	[ScriptService]
	public partial class MathService : WebService
	{
		public MathService()
		{
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
		}

		public JObject Json { get; set; }
        public string Token { get; set; }

        [WebMethod]
		public object Call(object data)
		{
            try
            {
                Json = JObject.FromObject(data);

                object retval = null;
                using (var entry = new ServiceEntry())
                {
                    retval = entry.Execute(Json);
                }
             
                return retval;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
	}
}
