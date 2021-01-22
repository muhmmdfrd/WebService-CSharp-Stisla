using Biz.Extension.IntExtension;
using Biz.Manager.GroupMenuManager;
using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class GroupMenuService : AppServiceBase
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public GroupMenuService(JObject json) : base(json, "GroupMenu")
		{
			// contructor
		}

		public object GetGroupMenu()
		{
			try
			{
				using (var query = new GroupMenuQuery(db, CurrentUser))
				{
					var filter = Json.ToObject<GroupMenuFilter>();
					var result = query.Get(filter);

					return ServiceResponse.Success(MessageResponse.Success(), result);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object GetListGroupMenu()
		{
			try
			{
				using (var query = new GroupMenuQuery(db, CurrentUser))
				{
					var result = query.GetList();

					return ServiceResponse.Success(MessageResponse.Success(), result);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object CreateGroupMenu()
		{
			try
			{
				using (var creator = new GroupMenuCreator(db))
				{
					var data = Json.ToObject<GroupMenu>();
					creator.Save(data);

					return ServiceResponse.Success(MessageResponse.Created(), null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object UpdateGroupMenu()
		{
			try
			{
				using (var updater = new GroupMenuUpdater(db))
				{
					var data = Json.ToObject<GroupMenu>();
					updater.Update(data);

					return ServiceResponse.Success(MessageResponse.Updated(), null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object DeleteGroupMenu()
		{
			try
			{
				using (var deleter = new GroupMenuDeleter(db))
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
