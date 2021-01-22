using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Manager.DashboardManager
{
	public class DashboardQuery : IDisposable
	{
		private readonly SimpleCrudEntities db;

		public DashboardQuery(SimpleCrudEntities db)
		{
			this.db = db;
		}

		public IQueryable<DashboardDTO> GetQuery()
		{
			return db.vDashboards.AsNoTracking().Select(x => new DashboardDTO()
			{
				Key = x.key,
				Value = x.value
			});
		}

		public List<DashboardDTO> Get()
		{
			return GetQuery().ToList();
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
