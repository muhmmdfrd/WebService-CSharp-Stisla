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
			if (!CurrentPermission.IsCanRead)
				throw new Exception(MessageResponse.Unauthorize("read"));

			try
			{
				using (var query = new UserQuery(db))
				{
					var data = Json.ToObject<UserFilter>();
					var result = query.Get(data);

					return ServiceResponse.Success(MessageResponse.Success(), result);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}

		}

		public object CreateUser()
		{
			if (!CurrentPermission.IsCanRead)
				throw new Exception(MessageResponse.Unauthorize("write"));

			try
			{
				using (var creator = new UserCreator(db))
				{
					var person = Json.ToObject<Person>();
					var user = Json.ToObject<User>();

					creator.Save(person, user);

					return ServiceResponse.Success(MessageResponse.Created(), null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object UpdateUser()
		{
			if (!CurrentPermission.IsCanUpdate)
				throw new Exception(MessageResponse.Unauthorize("update"));

			try
			{
				using (var update = new UserUpdater(db))
				{
					var user = Json.ToObject<User>();
					var person = Json.ToObject<Person>();

					update.Update(person, user);
					
					return ServiceResponse.Success(MessageResponse.Updated(), null);
					
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object DeleteUser()
		{
			if (!CurrentPermission.IsCanDelete)
				throw new Exception(MessageResponse.Unauthorize("delete"));

			try
			{
				using (var deleter = new UserDeleter(db))
				{
					var id = Json["id"].ToLong();
					deleter.Delete(id);

					return ServiceResponse.Success(MessageResponse.Deleted(), null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}
	}
}
