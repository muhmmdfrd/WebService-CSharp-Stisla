using Biz.Manager.BorrowingManager;
using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class BorrowingService : AppServiceBase
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public BorrowingService(JObject json) : base(json, "Borrowing")
		{
			// constructor
		}

		public object GetBorrowing()
		{
			if (!CurrentPermission.IsCanRead)
				throw new Exception(MessageResponse.Unauthorize("read"));

			try
			{
				using (var query = new BorrowingQuery(db))
				{
					var data = Json.ToObject<BorrowingFilter>();
					var result = query.Get(data);

					return ServiceResponse.Success(MessageResponse.Success(), result);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object CreateBorrowing()
		{
			if (!CurrentPermission.IsCanCreate)
				throw new Exception(MessageResponse.Unauthorize("write"));

			try
			{
				using (var creator = new BorrowingCreator(db))
				{
					var data = Json.ToObject<Borrowing>();
					creator.Borrow(data);

					return ServiceResponse.Success(MessageResponse.Created(), null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}
	}
}
