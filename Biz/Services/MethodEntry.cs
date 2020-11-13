using Repository;
using System;

namespace Biz.Services
{
	public partial class ServiceEntry
	{
		public object PersonGetAll()
		{
			return new PersonService(Json).GetPerson();
		}

		public object PersonGetById()
		{
			long id = Convert.ToInt32(Json["Id"]);
			return new PersonService(Json).GetPersonById(id);
		}

		public object PersonCreate()
		{
			var data = Json.ToObject<Person>();
			return new PersonService(Json).CreatePerson(data);
		}

		public object PersonUpdate()
		{
			var data = Json.ToObject<Person>();
			return new PersonService(Json).UpdatePerson(data);
		}

		public object PersonDelete()
		{
			long id = Convert.ToInt32(Json["Id"]);
			return new PersonService(Json).DeletePerson(id);
		}
	}
}
