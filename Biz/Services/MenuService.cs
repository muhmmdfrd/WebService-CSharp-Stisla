using Biz.Extension.IntExtension;
using Biz.Manager.MenuManager;
using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class MenuService : AppServiceBase
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public MenuService(JObject json) : base(json, "Menu")
		{
			// constructor
		}

		public object GetAll()
		{
			if (!CurrentPermission.IsCanRead)
				throw new Exception(MessageResponse.Unauthorize("read"));

			try
			{ 
				using (var query = new MenuQuery(db))
				{
					var data = Json.ToObject<MenuFilter>();
					var result = query.Get(data);

					return ServiceResponse.Success(MessageResponse.Success(), result);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object GetMenu()
		{
			if (!CurrentPermission.IsCanRead)
				throw new Exception(MessageResponse.Unauthorize("read"));

			try
			{
				using (var query = new MenuQuery(db))
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

		public object CreateMenu()
		{
			if (!CurrentPermission.IsCanCreate)
				throw new Exception(MessageResponse.Unauthorize("create"));

			try
			{
				using (var creator = new MenuCreator(db))
				{
					var data = Json.ToObject<Menu>();
					creator.Save(data);

					return ServiceResponse.Success(MessageResponse.Created(), null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object UpdateMenu()
		{

			if (!CurrentPermission.IsCanUpdate)
				throw new Exception(MessageResponse.Unauthorize("update"));

			try
			{
				using (var updater = new MenuUpdater(db))
				{
					var data = Json.ToObject<Menu>();
					updater.Update(data);

					return ServiceResponse.Success(MessageResponse.Updated(), null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object DeleteMenu()
		{
			if (!CurrentPermission.IsCanDelete)
				throw new Exception(MessageResponse.Unauthorize("delete"));

			try
			{
				using (var deleter = new MenuDeleter(db))
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
