using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Biz.Extension.JavascriptExtension
{
	public static class JSExtension
	{
		public static string Serialize(this object obj)
		{
			return new JavaScriptSerializer().Serialize(obj);
		}

		public static T Deserialize<T>(this string obj)
		{
			return JsonConvert.DeserializeObject<T>(obj);
		}
	}
}
