using Biz.Extension.NullCheckerExtension;
using Biz.Manager.PermissionManager;
using Biz.Manager.SessionManager;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class AppServiceBase : IDisposable
    {
        public PermissionDTO CurrentPermission { get; private set; }
        public JObject Json { get; set; }
        public User CurrentUser { get; private set; }
        public string Token { get; private set; }

        public AppServiceBase(JObject json, string moduleId, bool tokenCheck = true)
        {
            Json = json;
            if (!tokenCheck) return;

            Token = json["token"].ToString();

            if (string.IsNullOrEmpty(Token)) 
                throw new Exception("Empty token is not allowed.");

            var session = SessionQuery.GetByToken(Token);    
            if (session.IsNull())
                throw new Exception("Invalid token.");
            
            var user = SessionQuery.GetById(session.UserId);
            if (user.IsNull())
                throw new Exception("Invalid user data from token.");
            
            CurrentUser = user;

			var permission = PermissionQuery.GetByUserAndModule(CurrentUser.Id, moduleId);
            if (permission.IsNull())
                throw new Exception("Cannot get information access data from token.");

            CurrentPermission = permission;
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
