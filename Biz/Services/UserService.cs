using Biz.Extension.IntExtension;
using Biz.Manager.UserManager;
using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class UserService : AppServiceBase
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public UserService(JObject json) : base(json, "User")
		{
			// constructor
		}

		public object GetUser()
		{
			try
			{
				using (var query = new UserQuery(db))
				{
					var data = Json.ToObject<UserFilter>();

					return ServiceResponse.Success(query.Get(data));
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}

		}

		public object CreateUser()
		{
			try
			{
				using (var creator = new UserCreator(db))
				{
					var result = creator.Save(Json.ToObject<User>());

					return ServiceResponse.Success(result);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object UpdateUser()
		{
			try
			{
				using (var update = new UserUpdater(db))
				{
					var result = update.Update(Json.ToObject<User>());
					
					return ServiceResponse.Success(result);
					
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object DeleteUser()
		{
			try
			{
				using (var deleter = new UserDeleter(db))
				{
					deleter.Delete(Json["Id"].ToLong());

					return ServiceResponse.Success("data deleted");
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}
	}
}
