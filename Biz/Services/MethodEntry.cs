namespace Biz.Services
{
	public partial class ServiceEntry
	{
		#region Person Service
		public object PersonGetAll()
		{
			return new PersonService(Json).GetPerson();
		}

		public object PersonGetById()
		{
			return new PersonService(Json).GetPersonById();
		}

		public object PersonGetByKeyword()
		{
			return new PersonService(Json).GetPersonByKeyword();
		}

		public object PersonCreate()
		{
			return new PersonService(Json).CreatePerson();
		}

		public object PersonUpdate()
		{
			return new PersonService(Json).UpdatePerson();
		}

		public object PersonDelete()
		{
			return new PersonService(Json).DeletePerson();
		}
		#endregion

		#region User Service
		public object UserGetAll()
		{
			return new UserService(Json).GetUser();
		}

		public object UserGetById()
		{
			return new UserService(Json).GetUserById();
		}

		public object UserGetByKeyword()
		{
			return new UserService(Json).GetUserByKeyword();
		}

		public object UserCreate()
		{
			return new UserService(Json).CreateUser();
		}

		public object UserUpdate()
		{
			return new UserService(Json).UpdateUser();
		}

		public object UserDelete()
		{
			return new UserService(Json).DeleteUser();
		}
		#endregion

		#region Login
		public object Login()
		{
			return new LoginService(Json).Login();
		}
		#endregion

		#region Book
		public object BookGetAll()
		{
			return new BookService(Json).GetBook();
		}

		public object BookGetById()
		{
			return new BookService(Json).GetBookById();
		}

		public object BookGetByKeyword()
		{
			return new BookService(Json).GetBookByKeyword();
		}

		public object BookCreate()
		{
			return new BookService(Json).CreateBook();
		}

		public object BookUpdate()
		{
			return new BookService(Json).UpdateBook();
		}

		public object BookDelete()
		{
			return new BookService(Json).DeleteBook();
		}
		#endregion
	}
}
