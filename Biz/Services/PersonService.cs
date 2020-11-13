using Biz.Manager.PersonManager;
using Biz.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository;
using System;
using System.Collections.Generic;

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

		public object GetPersonById(long id)
		{
			try
			{
				using (var query = new PersonQuery(db))
				{
					return ServiceResponse.Success(query.GetById(id));
				}
			}
			catch (Exception ex)
			{
				return ServiceResponse.Fail(ex.Message);
			}
		}

		public object CreatePerson(Person data)
		{
			try
			{
				using (var creator = new PersonCreator(db))
				{
					var result = creator.Save(data);
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

		public object UpdatePerson(Person data)
		{
			try
			{
				using (var update = new PersonUpdater(db))
				{
					var result = update.Update(data);
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

		public object DeletePerson(long id)
		{
			try
			{
				using (var deleter = new PersonDeleter(db))
				{
					deleter.Delete(id);
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
