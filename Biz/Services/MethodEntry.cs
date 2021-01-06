namespace Biz.Services
{
	public partial class ServiceEntry
	{
		#region Dashboard
		public object DashboardGet()
		{
			return new DashboardService(Json).GetDashboard();
		}
		#endregion

		#region User
		public object UserGetAll()
		{
			return new UserService(Json).GetUser();
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

		public object Logout()
		{
			return new LoginService(Json).Logout();
		}
		#endregion

		#region Book
		public object BookGetAll()
		{
			return new BookService(Json).GetBook();
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

		#region Borrowing
		public object BorrowingGetAll()
		{
			return new BorrowingService(Json).GetBorrowing();
		}

		public object BorrowingCreate()
		{
			return new BorrowingService(Json).CreateBorrowing();
		}
		#endregion

		#region Role
		public object RoleGetAll()
		{
			return new RoleService(Json).GetRole();
		}

		public object RoleCreate()
		{
			return new RoleService(Json).CreateRole();
		}

		public object RoleUpdate()
		{
			return new RoleService(Json).UpdateRole();
		}

		public object RoleDelete()
		{
			return new RoleService(Json).DeleteRole();
		}
		#endregion

		#region GroupMenu
		public object GroupMenuGetAll()
		{
			return new GroupMenuService(Json).GetGroupMenu();
		}
		#endregion

		#region Menu
		public object MenuGetAll()
		{
			return new MenuService(Json).GetAll();
		}

		public object MenuGetByRole()
		{
			return new MenuService(Json).GetMenu();
		}

		public object MenuCreate()
		{
			return new MenuService(Json).CreateMenu();
		}

		public object MenuUpdate()
		{

			return new MenuService(Json).UpdateMenu();
		}

		public object MenuDelete()
		{
			return new MenuService(Json).DeleteMenu();
		}
		#endregion
	}
}
