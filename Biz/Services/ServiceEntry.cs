using Biz.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Biz.Services
{
    public partial class ServiceEntry : IDisposable
    {
        [JsonProperty("json")]
        private JObject Json;

        [JsonProperty("token")]
        private string Token = "";
        
        [JsonProperty("method")]
        private string Method = "";

        public void Dispose()
        {
            Json = null;
            Token = null;
            Method = null;
        }

        public Status<object> Execute(JObject param)
        {
            Json = param;
            if (param["token"] == null)
                throw new Exception("Empty token is not allowed.");

            Token = Convert.ToString(param["token"]);

            if (param["method"] == null)
                throw new Exception("Specify method name.");

            Method = Convert.ToString(param["method"]);

            Status<object> retval = new Status<object>();
            dynamic response = GetType().GetMethod(Method).Invoke(this, null);

            try
            {
                retval.Success = response.success;
                retval.Message = response.message;
                retval.Values = response.values;
            }
            catch (Exception ex)
            {
                retval.Success = false;
                retval.Message = ex.Message;
                retval.Values = null;
            }

            return retval;
        }
    }
}
