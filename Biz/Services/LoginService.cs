using Biz.Extension.NullCheckerExtension;
using Biz.Manager.UserManager;
using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;
using System.Linq;
using System.Transactions;
using static Biz.Manager.UserManager.UserQuery;

namespace Biz.Services
{
	internal class LoginService
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();
		private readonly JObject Json;

		public LoginService(JObject Json)
		{
			this.Json = Json;
		}

		public object Login()
		{
			try
			{
				using (var query = new UserQuery(db))
				{
					var data = Json.ToObject<User>();
					var result = query.Login(data);

					InsertUserSession(result);

					return ServiceResponse.Success(MessageResponse.LoginSuccess(), result);
				}

			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object Logout()
		{
			try
			{
				using (var query = new UserQuery(db))
				{
					var token = Json["token"].ToString();
					query.Logout(token);

					return ServiceResponse.Success(null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		private void InsertUserSession(LoginInfo info)
		{
			using (var transac = new TransactionScope())
			{
				var now = DateTime.Now;
				var user = db.Users.FirstOrDefault(x => x.Username.Equals(info.Username));

				if (user.IsNull())
					throw new Exception("User cannot get the session.");

				var existUserSession = db.UserSessions.FirstOrDefault(x => x.UserId == user.Id);
				if (existUserSession.IsNotNull())
				{
					existUserSession.UserId = user.Id;
					existUserSession.Username = user.Username;
					existUserSession.Token = info.Token;
					existUserSession.LastActivity = now;
				}
				else
				{
					var newSession = new UserSession
					{
						UserId = user.Id,
						Username = user.Username,
						Token = info.Token,
						LastActivity = now
					};

					db.UserSessions.Add(newSession);
				}

				db.SaveChanges();

				transac.Complete();
			}
		}
	}
}
