using Biz.Extension.JavascriptExtension;
using Biz.Extension.NullCheckerExtension;
using Biz.Model;
using Repository;
using System;
using System.Transactions;

namespace Biz.Manager.RoleManager
{
	public class RoleUpdater
	{
		private readonly SimpleCrudEntities db;

		public RoleUpdater(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Update(RoleDTO role)
		{
			using (var transac = new TransactionScope())
			{
				var exist = db.Roles.Find(role.Id);

				if (exist.IsNull())
					throw new Exception(MessageResponse.NotFound("Role"));

				exist.Name = role.Name;
				exist.Access = role.Permissions.Serialize();
				
				db.SaveChanges();
				db.Dispose();
				transac.Complete();
			}
		}
	}
}
