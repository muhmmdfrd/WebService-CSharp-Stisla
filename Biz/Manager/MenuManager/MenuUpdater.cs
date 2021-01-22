using Biz.Extension.JavascriptExtension;
using Biz.Extension.NullCheckerExtension;
using Biz.Extension.StringExtension;
using Biz.Manager.PermissionManager;
using Biz.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Biz.Manager.MenuManager
{
	public class MenuUpdater : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public MenuUpdater(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Update(Menu menu)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Menus.Find(menu.Id);

				if (exist.IsNull())
					throw new Exception(MessageResponse.NotFound("Menu"));

				exist.GroupMenuId = menu.GroupMenuId;
				exist.Icon = menu.Icon;
				exist.Module = menu.Module;
				exist.Name = menu.Name;
				exist.Path = menu.Path;

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
