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
					var result = query.Login(Json.ToObject<User>());
					return ServiceResponse.Success("login successfull", result);
				}

			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}
	}
}
