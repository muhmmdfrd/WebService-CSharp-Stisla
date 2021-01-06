using Biz.Extension.IntExtension;
using Biz.Extension.JavascriptExtension;
using Biz.Extension.NullCheckerExtension;
using Biz.Manager.MenuManager;
using Biz.Manager.PermissionManager;
using Biz.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Manager.GroupMenuManager
{
	public class GroupMenuFilter : TableFilter
	{
		public long Id { get; set; }
		public long RoleId { get; set; }
	}

	public class GroupMenuQuery : IDisposable
	{
		private readonly SimpleCrudEntities db;
		private readonly User user;

		public GroupMenuQuery(SimpleCrudEntities db, User user)
		{
			this.db = db;
			this.user = user;
		}

		public IQueryable<GroupMenuDTO> GetQuery()
		{
			return (from groupMenu in db.GroupMenus
					select new GroupMenuDTO()
					{
						Id = groupMenu.Id,
						Name = groupMenu.Name,
						Sequence = groupMenu.Sequence,
						IsCollapse = groupMenu.IsCollapse
					}).Distinct();
		}

		public List<GroupMenuDTO> GetList()
		{
			var query = GetQuery();
			var list = query.ToList();

			var listAccess = db.Roles
				.Select(x => new { x.Id, x.Access })
				.FirstOrDefault(x => x.Id == user.RoleId)
				.Access
				.Deserialize<List<PermissionDTO>>();

			var listMenu = new List<MenuDTO>();

			foreach (var access in listAccess)
			{
				if (access.IsShowMenu)
				{
					var accessMenu = db.Menus
					.Join(db.GroupMenus, m => m.GroupMenuId, gm => gm.Id, (menu, groupMenu) => new { menu, groupMenu })
					.Where(s => s.menu.Id == access.MenuId && access.IsShowMenu)
					.Select(s => new MenuDTO()
					{
						Id = s.menu.Id,
						Icon = s.menu.Icon,
						Sequence = s.menu.Sequence,
						Name = s.menu.Name,
						Path = s.menu.Path,
						GroupMenuId = s.groupMenu.Id
					}).OrderBy(x => x.Sequence).FirstOrDefault();

					if (accessMenu.IsNotNull())
					{
						listMenu.Add(accessMenu);
					}

				}
			}

			foreach (var group in list.ToList())
			{
				var menu = listMenu.Where(x => x.GroupMenuId == group.Id);

				if (menu.Count().IsZero())
				{
					var exist = list.FirstOrDefault(x => x.Id == group.Id);
					list.Remove(exist);
				}
				else
				{
					group.Menus = menu.OrderBy(x => x.Sequence).ToList();
				}	
			}

			return list;
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
