using Biz.Extension.IntExtension;
using Biz.Manager.RoleManager;
using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class RoleService : AppServiceBase
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public RoleService(JObject json) : base(json, "Role")
		{
			// contructor
		}

		public object GetRole()
		{
			if (!CurrentPermission.IsCanRead)
				throw new Exception(MessageResponse.Unauthorize("read"));

			try
			{
				using (var query = new RoleQuery(db))
				{
					var data = Json.ToObject<RoleFilter>();
					var result = query.Get(data);

					return ServiceResponse.Success(MessageResponse.Success(), result);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object CreateRole()
		{
			if (!CurrentPermission.IsCanCreate)
				throw new Exception(MessageResponse.Unauthorize("write"));

			try
			{
				using (var creator = new RoleCreator(db))
				{
					var data = Json.ToObject<Role>();
					creator.Save(data);

					return ServiceResponse.Success(MessageResponse.Created(), null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object UpdateRole()
		{
			if (!CurrentPermission.IsCanUpdate)
				throw new Exception(MessageResponse.Unauthorize("update"));

			try
			{
				var updater = new RoleUpdater(db);
				var data = Json.ToObject<RoleDTO>();
				updater.Update(data);

				return ServiceResponse.Success(MessageResponse.Updated(), null);
				
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object DeleteRole()
		{
			if (!CurrentPermission.IsCanDelete)
				throw new Exception(MessageResponse.Unauthorize("delete"));

			try
			{
				using (var deleter = new RoleDeleter(db))
				{
					long roleId = Json["id"].ToLong();
					deleter.Delete(roleId);

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
