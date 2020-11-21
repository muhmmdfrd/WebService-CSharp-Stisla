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
					return ServiceResponse.Success(query.GetAll());
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
			
		}

		public object GetPersonById()
		{
			try
			{
				using (var query = new PersonQuery(db))
				{
					return ServiceResponse.Success(query.GetById(Convert.ToInt32(Json["Id"])));
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}


		public object GetPersonByKeyword()
		{
			try
			{
				using (var query = new PersonQuery(db))
				{
					return ServiceResponse.Success(query.GetByKeyword(Json["Keyword"].ToString()));
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
					using (var query = new PersonQuery(db))
					{
						return ServiceResponse.Success(query.GetById(result.Id));
					}
					
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
					using (var query = new PersonQuery(db))
					{
						return ServiceResponse.Success(query.GetById(result.Id));
					}
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
					deleter.Delete(Convert.ToInt32(Json["Id"]));
					return ServiceResponse.Success("Data Deleted");
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}


	}
}
