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
			return (from dashboard in db.vDashboards
					select new DashboardDTO()
					{
						Key = dashboard.key,
						Value = dashboard.value ?? 0
					}) ;;
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
