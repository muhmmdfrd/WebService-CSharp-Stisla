using Biz.Extension.JavascriptExtension;
using Biz.Manager.PermissionManager;
using Biz.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Manager.RoleManager
{
	public class RoleFilter : TableFilter
	{
		public long Id { get; set; }
	}

	public class RoleQuery : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public RoleQuery(SimpleCrudEntities db)
		{
			this.db = db;
		}

		private List<PermissionDTO> GetAccessByRole(long id)
		{
			return db.Roles
				.AsNoTracking()
				.Select(x => new { x.Access, x.Id })
				.FirstOrDefault(x => x.Id == id)
				.Access
				.Deserialize<List<PermissionDTO>>();			
		}

		public IQueryable<RoleDTO> GetQuery()
		{
			return db.Roles.Select(x => new RoleDTO()
			{
				Id = x.Id,
				Name = x.Name
			});
		}	

		public RoleDTO GetById(long id, bool isAuth = false)
		{
			if (!isAuth)
			{
				return GetQuery().FirstOrDefault(x => x.Id == id);
			}
			else
			{
				var query = GetQuery();
				var list = query.ToList();
				
				foreach (var role in list)
				{
					role.Permissions = GetAccessByRole(role.Id);
				}

				return list.FirstOrDefault(x => x.Id == id);
			}
		}

		public Pagination<RoleDTO> Get(RoleFilter filter)
		{
			var query = GetQuery();
			var total = query.Count();
			var filterred = total;

			if (filter.Id > 0)
			{
				query = query.Where(x => x.Id == filter.Id);
				filterred = query.Count();

				if (filterred == 0)
					throw new Exception("data not found");
			}

			if (!string.IsNullOrEmpty(filter.Keyword))
			{
				query = query.Where(x => x.Name.Contains(filter.Keyword));
				filterred = query.Count();
			}

			query = query
				.OrderBy(x => x.Id)
				.Skip(filter.Skip)
				.Take(filter.PageSize);

			var list = query.ToList();
			foreach (var role in list)
			{
				role.Permissions = GetAccessByRole(role.Id);
			}

			return new Pagination<RoleDTO>()
			{
				ActivePage = filter.ActivePage,
				PageSize = filter.PageSize,
				Data = list,
				RecordsFiltered = filterred,
				RecordsTotal = total
			};
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
