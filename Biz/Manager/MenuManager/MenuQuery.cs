using Biz.Extension.IntExtension;
using Biz.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Manager.MenuManager
{
	public class MenuFilter : TableFilter
	{
		public long Id { get; set; }
	}

	public class MenuQuery : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public MenuQuery(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public IQueryable<MenuDTO> GetQuery()
		{
			return db.Menus
				.Join(db.GroupMenus, m => m.GroupMenuId, g => g.Id, (m, g) => new { Menu = m, GroupMenu = g })
				.Select(menu => new MenuDTO()
				{
					Id = menu.Menu.Id,
					GroupMenuId = menu.Menu.GroupMenuId,
					Icon = menu.Menu.Icon,
					Name = menu.Menu.Name,
					Path = menu.Menu.Path,
					Sequence = menu.Menu.Sequence,
					GroupMenuName = menu.GroupMenu.Name
				});
		}

		public Pagination<MenuDTO> Get(MenuFilter filter)
		{
			var query = GetQuery();
			int total = query.Count();
			int totalFilter = total;

			if (filter.Id > 0)
			{
				query = query.Where(x => x.Id == filter.Id);
				totalFilter = query.Count();

				if (totalFilter.IsZero())
					throw new Exception(MessageResponse.NotFound("Menu"));
			}

			if (!string.IsNullOrEmpty(filter.Keyword))
			{
				query = query.Where(x => x.Name.Contains(filter.Keyword));
				totalFilter = query.Count();
			}

			query = query
				.OrderBy(x => x.Id)
				.Skip(filter.Skip)
				.Take(filter.PageSize);

			return new Pagination<MenuDTO>()
			{
				ActivePage = filter.ActivePage,
				Data = query.ToList(),
				PageSize = filter.PageSize,
				RecordsFiltered = totalFilter,
				RecordsTotal = total
			};
		}

		public List<MenuDTO> GetList()
		{
			return GetQuery().ToList();
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}