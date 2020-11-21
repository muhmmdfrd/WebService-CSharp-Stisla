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
			try
			{
				using (var query = new BookQuery(db))
				{
					return ServiceResponse.Success("data found", query.GetBook(Json.ToObject<BookFilter>()));
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object GetBookById()
		{
			try
			{
				using (var query = new BookQuery(db))
				{
					return ServiceResponse.Success("data found", query.GetById(Convert.ToInt64(Json["Id"])));
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object GetBookByKeyword()
		{
			try
			{
				using (var query = new BookQuery(db))
				{
					return ServiceResponse.Success("data found", query.GetByKeyword(Json["Keyword"].ToString()));
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object CreateBook()
		{
			try
			{
				using (var creator = new BookCreator(db))
				{
					var result = creator.Save(Json.ToObject<Book>());
					return ServiceResponse.Success(result);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object UpdateBook()
		{
			try
			{
				using (var updater = new BookUpdater(db))
				{
					var result = updater.Update(Json.ToObject<Book>());
					return ServiceResponse.Success(result);
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object DeleteBook()
		{
			try
			{
				using (var deleter = new BookDeleter(db))
				{
					deleter.Delete(Convert.ToInt64(Json["Id"]));
					return ServiceResponse.Success("data deleted");
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

	}
}
