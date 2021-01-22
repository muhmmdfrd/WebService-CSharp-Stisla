using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Biz.Manager.GroupMenuManager
{
	public class GroupMenuCreator : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public GroupMenuCreator(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Save(GroupMenu groupMenu)
		{
			using (var transac = new TransactionScope())
			{
				int lastSequence = db.GroupMenus
					.OrderByDescending(x => x.Sequence)
					.Select(x => x.Sequence)
					.FirstOrDefault();

				groupMenu.Sequence = lastSequence + 1;

				db.GroupMenus.Add(groupMenu);
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
