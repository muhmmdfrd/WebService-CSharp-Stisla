using Biz.Manager.GroupMenuManager;
using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class GroupMenuService : AppServiceBase
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public GroupMenuService(JObject json) : base(json, "GroupMenu")
		{
			// contructor
		}

		public object GetGroupMenu()
		{
			try
			{
				using (var query = new GroupMenuQuery(db, CurrentUser))
				{
					var result = query.GetList();

					return ServiceResponse.Success(MessageResponse.Success(), result);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}
	}
}
