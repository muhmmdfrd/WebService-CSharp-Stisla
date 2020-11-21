using Biz.Manager.UserManager;
using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class LoginService : AppServiceBase
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();
		public LoginService(JObject json) : base(json, "User")
		{
			// constructor
		}

		public object Login()
		{
			try
			{
				using (var query = new UserQuery(db))
				{
					var message = query.Login(Json.ToObject<User>()) ? "login success" : throw new Exception("login failed");
					return ServiceResponse.Success(message, Guid.NewGuid().ToString());
				}

			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}
	}
}
