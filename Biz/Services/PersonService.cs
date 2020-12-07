using Biz.Extension.IntExtension;
using Biz.Manager.PersonManager;
using Biz.Model;
using Newtonsoft.Json.Linq;
using Repository;
using System;

namespace Biz.Services
{
	internal class PersonService : AppServiceBase
	{
		private readonly SimpleCrudEntities db = new SimpleCrudEntities();

		public PersonService(JObject json) : base(json, "Person")
		{
			// constructor
		}

		public object GetPerson()
		{
			try
			{
				using (var query = new PersonQuery(db))
				{
					var data = Json.ToObject<PersonFilter>();

					return ServiceResponse.Success(query.Get(data));
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object CreatePerson()
		{
			try
			{
				using (var creator = new PersonCreator(db))
				{
					var result = creator.Save(Json.ToObject<Person>());
					
					return ServiceResponse.Success(result);
					
					
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object UpdatePerson()
		{
			try
			{
				using (var update = new PersonUpdater(db))
				{
					var result = update.Update(Json.ToObject<Person>());
					
					return ServiceResponse.Success(result);
					
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object DeletePerson()
		{
			try
			{
				using (var deleter = new PersonDeleter(db))
				{
					deleter.Delete(Json["Id"].ToLong());

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
