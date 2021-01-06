using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Biz.Extension.StringExtension
{
	public static class StringExtension
	{
		private const string keyCombined = "b14ca5898a4e4133bbce2ea2315a1916";

		public static string Encrypt(this string text)
		{
			byte[] vector = new byte[16];
			byte[] container;

			using (var aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(keyCombined);
				aes.IV = vector;

				var executor = aes.CreateEncryptor(aes.Key, aes.IV);
				using (var memory = new MemoryStream())
				{
					using (var crypto = new CryptoStream(memory, executor, CryptoStreamMode.Write))
					{
						using (var writter = new StreamWriter(crypto))
						{
							writter.Write(text);
						}

						container = memory.ToArray();
					}
				}
			}
			return container.ToBase64();
		}

		public static string Decrypt(this string text)
		{
			byte[] vector = new byte[16];
			byte[] buffer = Convert.FromBase64String(text);

			using (var aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(keyCombined);
				aes.IV = vector;

				var executor = aes.CreateDecryptor(aes.Key, aes.IV);
				using (var memory = new MemoryStream(buffer))
				{
					using (var crypto = new CryptoStream(memory, executor, CryptoStreamMode.Read))
					{
						using (var streamReader = new StreamReader(crypto))
						{
							return streamReader.ReadToEnd();
						}
					}
				}
			}
		}

		public static string ConcatJSON(this string old, string param)
		{
			int index = old.IndexOf(']');
			string first = old.Remove(index);

			return first + "," + param + "]";
		}
			
		private static string ToBase64(this byte[] array)
		{
			return Convert.ToBase64String(array);
		}

		public static string RemoveBackslash(this string word)
		{
			return word.Replace(@"\", string.Empty);
		}
	}
}
