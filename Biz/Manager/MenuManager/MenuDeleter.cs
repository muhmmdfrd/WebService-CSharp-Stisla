using Biz.Extension.JavascriptExtension;
using Biz.Extension.NullCheckerExtension;
using Biz.Manager.PermissionManager;
using Biz.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Biz.Manager.MenuManager
{
	public class MenuDeleter : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public MenuDeleter(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Menus.Find(id);

				if (exist.IsNull())
					throw new Exception(MessageResponse.NotFound("Menu"));

				var existRole = db.Roles;
				
				foreach (var role in existRole)
				{
					var listMenu = role.Access.Deserialize<List<PermissionDTO>>();
					var newListAccess = new List<PermissionDTO>();

					foreach (var menu in listMenu)
					{
						if (menu.MenuId != id)
							newListAccess.Add(menu);
					}

					role.Access = newListAccess.Serialize();
				}

				db.Menus.Remove(exist);
				db.SaveChanges();

				transac.Complete();
			}
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
