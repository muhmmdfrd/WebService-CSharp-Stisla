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
					return ServiceResponse.Success(query.GetAll());
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}

		}

		public object GetUserById()
		{
			try
			{
				using (var query = new UserQuery(db))
				{
					return ServiceResponse.Success(query.GetById(Convert.ToInt32(Json["Id"])));
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}


		public object GetUserByKeyword()
		{
			try
			{
				using (var query = new UserQuery(db))
				{
					return ServiceResponse.Success(query.GetByKeyword(Json["Keyword"].ToString()));
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
					using (var query = new UserQuery(db))
					{
						return ServiceResponse.Success(query.GetById(result.Id));
					}

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
					using (var query = new UserQuery(db))
					{
						return ServiceResponse.Success(query.GetById(result.Id));
					}
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
					deleter.Delete(Convert.ToInt32(Json["Id"]));
					return ServiceResponse.Success("Data Deleted");
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}
	}
}
