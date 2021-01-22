using Biz.Extension.NullCheckerExtension;
using Biz.Model;
using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.GroupMenuManager
{
	public class GroupMenuUpdater : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public GroupMenuUpdater(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Update(GroupMenu groupMenu)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.GroupMenus.Find(groupMenu.Id);

				if (exist.IsNull())
					throw new Exception(MessageResponse.NotFound("GroupMenu"));

				exist.IsCollapse = groupMenu.IsCollapse;
				exist.Name = groupMenu.Name;
				exist.Sequence = groupMenu.Sequence;

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
