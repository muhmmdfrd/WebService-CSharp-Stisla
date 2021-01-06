using Biz.Extension.NullCheckerExtension;
using Biz.Model;
using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.RoleManager
{
	public class RoleDeleter : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public RoleDeleter(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Roles.Find(id);

				if (exist.IsNull())
					throw new Exception(MessageResponse.NotFound("Role"));

				db.Roles.Remove(exist);
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
