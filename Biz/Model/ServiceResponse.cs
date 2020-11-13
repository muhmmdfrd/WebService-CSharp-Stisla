namespace Biz.Model
{
	public class ServiceResponse
	{
        public static object Success(object val)
        {
            return new { success = true, message = "", values = val };
        }

        public static object Success(string message, object val)
        {
            return new { success = true, message = message, values = val };
        }

        public static object Fail(string errorMessage)
        {
            return new { success = false, message = errorMessage, values = "" };
        }

        public static object SecurityAccessFail()
        {
            return Fail("Security access error");
        }
    }
}
