using Biz.Extension.IntExtension;
using Biz.Manager.BookManager;
using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class BookService : AppServiceBase
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public BookService(JObject json) : base(json, "Book")
		{
			// constructor
		}

		public object GetBook()
		{
			if (!CurrentPermission.IsCanRead)
				throw new Exception(MessageResponse.Unauthorize("read"));

			try
			{
				using (var query = new BookQuery(db))
				{
					var data = Json.ToObject<BookFilter>();
					var result = query.Get(data);

					return ServiceResponse.Success(MessageResponse.Success(), result);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object CreateBook()
		{
			if (!CurrentPermission.IsCanCreate)
				throw new Exception(MessageResponse.Unauthorize("write"));

			try
			{
				using (var creator = new BookCreator(db))
				{
					creator.Save(Json.ToObject<Book>());

					return ServiceResponse.Success(MessageResponse.Created(), null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object UpdateBook()
		{
			if (!CurrentPermission.IsCanUpdate)
				throw new Exception(MessageResponse.Unauthorize("update"));

			try
			{
				using (var updater = new BookUpdater(db))
				{
					updater.Update(Json.ToObject<Book>());

					return ServiceResponse.Success(MessageResponse.Updated(), null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object DeleteBook()
		{
			if (!CurrentPermission.IsCanDelete)
				throw new Exception(MessageResponse.Unauthorize("delete"));

			try
			{
				using (var deleter = new BookDeleter(db))
				{
					var id = Json["id"].ToLong();
					deleter.Delete(id);

					return ServiceResponse.Success(MessageResponse.Deleted(), null);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

	}
}
