using Biz.Manager.PermissionManager;
using Repository;
using System;
using System.Linq;
using System.Transactions;
using System.Web.Script.Serialization;

namespace Biz.Manager.RoleManager
{
	public class RoleCreator : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public RoleCreator(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Save(Role role)
		{
			using (var transac = new TransactionScope())
			{
				if (string.IsNullOrEmpty(role.Access))
					role.Access = GetAccess();

				db.Roles.Add(role);
				db.SaveChanges();

				transac.Complete();
			}
		}

		private string GetAccess()
		{
			var menu = db.Menus.Select(x => new PermissionDTO()
			{
				Id = 0,
				IsCanCreate = false,
				IsCanDelete = false,
				IsCanRead = false,
				IsCanUpdate = false,
				IsShowMenu = false,
				MenuId = x.Id,
				ModuleId = x.Name,
				RoleId = 0,
				UserId = 0
			}).ToList();

			return new JavaScriptSerializer().Serialize(menu);
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
