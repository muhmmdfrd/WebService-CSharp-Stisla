using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class AppServiceBase : IDisposable
    {
        public Permission CurrentPermission { get; private set; }
        public JObject Json { get; set; }
        public User CurrentUser { get; private set; }
        public string Token { get; private set; }

        public AppServiceBase(JObject json, string moduleId, bool tokenCheck = true)
        {
            Json = json;
            if (!tokenCheck) return;

            Token = Convert.ToString(json["token"]);

            if (string.IsNullOrEmpty(Token)) throw new Exception("Empty token is not allowed.");
        }

        public void Dispose()
        {
            CurrentPermission = null;
            Json = null;
            CurrentUser = null;
            Token = null;
        }
    }
}
