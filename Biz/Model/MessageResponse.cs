namespace Biz.Model
{
	public static class MessageResponse
	{
		public static string Success()
		{
			return "Data Found";
		}

		public static string LoginSuccess()
		{
			return "Login Success";
		}

		public static string Created()
		{
			return "Data Created";
		}

		public static string Updated()
		{
			return "Data Updated";
		}

		public static string Deleted()
		{
			return "Data Deleted";
		}

		public static string NotFound(string module)
		{
			return $"{module} not found";
		}

		public static string Unauthorize(string action)
		{
			return $"You not allowed to {action} this data";
		}
	}
}
