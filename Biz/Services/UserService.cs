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
					var result = query.Get(data);

					return ServiceResponse.Success(result);
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
					var person = Json.ToObject<Person>();
					var user = Json.ToObject<User>();

					var result = creator.Save(person, user);

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
					var user = Json.ToObject<User>();
					var person = Json.ToObject<Person>();

					var result = update.Update(person, user);
					
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
					deleter.Delete(Json["id"].ToLong());

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
