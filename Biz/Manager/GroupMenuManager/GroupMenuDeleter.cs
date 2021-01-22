using Biz.Extension.NullCheckerExtension;
using Biz.Model;
using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Biz.Manager.GroupMenuManager
{
	public class GroupMenuDeleter : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public GroupMenuDeleter(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.GroupMenus.Find(id);

				if (exist.IsNull())
					throw new Exception(MessageResponse.NotFound("GroupMenu"));

				var menu = db.Menus.FirstOrDefault(x => x.GroupMenuId == id);

				if (menu.IsNotNull())
					throw new Exception(MessageResponse.Error("menu is exist in group menu"));

				db.GroupMenus.Remove(exist);
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
