using Biz.Extension.NullCheckerExtension;
using Biz.Model;
using Repository;
using System;
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
				exist.Sequence = menu.Sequence;

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
