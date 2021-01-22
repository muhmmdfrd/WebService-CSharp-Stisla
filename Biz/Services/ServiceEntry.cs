using Biz.Extension.NullCheckerExtension;
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

		public object HttpContext { get; private set; }

		public Status<object> Execute(JObject param)
        {
            var retval = new Status<object>();

            try
            {
                Json = param;
                if (param["token"].IsNull())
                    throw new Exception("Empty token is not allowed.");

                Token = param["token"].ToString();

                if (param["method"].IsNull())
                    throw new Exception("Specify method name.");

                Method = param["method"].ToString();

                dynamic response = GetType().GetMethod(Method).Invoke(this, null);

                retval.Success = response.success;
                retval.Message = response.message;
                retval.Values = response.values;
            }
            catch (Exception ex)
            {
                retval.Success = false;
                retval.Message = ex.InnerException?.Message ?? ex.Message;
                retval.Values = null;
            }

            return retval;
        }

        public void Dispose()
        {
            Json = null;
            Token = null;
            Method = null;
        }
    }
}
