using Biz.Extension.NullCheckerExtension;
using Biz.Manager.RoleManager;
using Biz.Model;
using Repository;
using System;
using System.Linq;

namespace Biz.Manager.PermissionManager
{
	public static class PermissionQuery
	{
		public static PermissionDTO GetByUserAndModule(long userId, string moduleId)
		{
			using (var db = new SimpleCrudEntities())
			{
				var currentUser = db.Users.Find(userId);

				if (currentUser.IsNull())
					throw new Exception(MessageResponse.NotFound("User"));

				using (var roleQuery = new RoleQuery(db))
				{
					var role = roleQuery.GetById(currentUser.RoleId, true);

					var result =  role.Permissions.Join(db.Menus, p => p.MenuId, m => m.Id, (c, d) => new { c, d })
						.Where(x => x.d.Module.Equals(moduleId))
						.Select(x => x.c).DefaultIfEmpty(new PermissionDTO { UserId = userId, ModuleId = moduleId })
						.First();

					return result;
				}
			}
		}
	}
}
