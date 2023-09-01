using System.Security.Cryptography;
using System.Text;

namespace VD.LIB
{
	public class Security
	{
		private static readonly Random _rng = new();
		public static string RandomString(int size)
		{
			char[] buffer = new char[size];

			for (int i = 0; i < size; i++)
			{
				buffer[i] = _chars[_rng.Next(_chars.Length)];
			}
			return new string(buffer);
		}
		public static string CheckHMAC(string key, string? message)
		{
			string result = "";
			if (key == null) return result;
			if (message == null) return result;


			ASCIIEncoding encoding = new();

			byte[] keyByte = encoding.GetBytes(key);

			HMACSHA256 hmacsha256 = new(keyByte);

			byte[] messageBytes = encoding.GetBytes(message);

			byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
			result = ByteToString(hashmessage);
			return result;
		}

		public static string ByteToString(byte[] buff)
		{
			string sbinary = "";

			for (int i = 0; i < buff.Length; i++)
			{
				sbinary += buff[i].ToString("x2"); // hex format
			}
			return (sbinary);
		}

		private const string _chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890~!@#$%^&*()-+:,./?";
	}
}
