using Biz.Extension.JavascriptExtension;
using Biz.Extension.NullCheckerExtension;
using Biz.Extension.StringExtension;
using Biz.Manager.PermissionManager;
using Biz.Model;
using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Biz.Manager.MenuManager
{
	public class MenuCreator : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public MenuCreator(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public void Save(Menu menu)
		{
			using (var transac = new TransactionScope())
			{
				var existParent = db.GroupMenus.Find(menu.GroupMenuId);

				if (existParent.IsNull())
					throw new Exception(MessageResponse.NotFound("GroupMenu"));

				var lastSequence = db.Menus
					.Where(x => x.GroupMenuId == menu.GroupMenuId)
					.OrderByDescending(x => x.Sequence).FirstOrDefault()
					.Sequence;

				menu.Sequence = lastSequence + 1;

				var result = db.Menus.Add(menu);
				db.SaveChanges();

				UpdateRoleAccess(result);
				transac.Complete();
			}
		}

		private void UpdateRoleAccess(Menu menu)
		{
			foreach (var role in db.Roles)
			{
				if (role.Id == 1)
				{
					string access = role.Access.Trim(new char[] { '\\' });
					string newAccess = new PermissionDTO()
					{
						Id = 0,
						IsCanCreate = true,
						IsCanDelete = true,
						IsCanRead = true,
						IsCanUpdate = true,
						IsShowMenu = true,
						MenuId = menu.Id,
						ModuleId = menu.Module,
						RoleId = role.Id,
						UserId = 0
					}.Serialize().Trim(new char[] { '\\' });

					role.Access = access.ConcatJSON(newAccess);
				}
				else
				{
					string access = role.Access.RemoveBackslash();
					string newAccess = new PermissionDTO()
					{
						Id = 0,
						IsCanCreate = false,
						IsCanDelete = false,
						IsCanRead = false,
						IsCanUpdate = false,
						IsShowMenu = false,
						MenuId = menu.Id,
						ModuleId = menu.Module,
						RoleId = role.Id,
						UserId = 0
					}.Serialize().RemoveBackslash();

					role.Access = access.ConcatJSON(newAccess);
				}

				db.SaveChanges();
			}
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
