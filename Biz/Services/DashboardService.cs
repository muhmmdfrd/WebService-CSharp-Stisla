using Biz.Manager.DashboardManager;
using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class DashboardService : AppServiceBase
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public DashboardService(JObject json) : base(json, "Dashboard")
		{
			// constructor
		}

		public object GetDashboard()
		{
			if (!CurrentPermission.IsCanRead)
				throw new Exception(MessageResponse.Unauthorize("read"));

			try
			{
				using (var query = new DashboardQuery(db))
				{
					return ServiceResponse.Success(MessageResponse.Success(), query.Get());
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}
	}
}
