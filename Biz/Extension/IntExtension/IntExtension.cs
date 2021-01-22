using System;

namespace Biz.Extension.IntExtension
{
	public static class IntExtension
	{
		public static bool IsGreaterThan(this int i, int value)
		{
			return i > value;
		}

		public static bool IsLowerThan(this int i, int value)
		{
			return i < value;
		}

		public static bool IsGreaterThanEquals(this int i, int value)
		{
			return i >= value;
		}

		public static bool IsLowerThanEquals(this int i, int value)
		{
			return i <= value;
		}

		public static long ToLong(this object value)
		{
			return Convert.ToInt64(value);
		}

		public static long ToInt(this object value)
		{
			return Convert.ToInt32(value);
		}

		public static bool IsZero(this int num)
		{
			return num == 0;
		}

		public static bool IsZero(this long num)
		{
			return num == 0;
		}
	}
}
