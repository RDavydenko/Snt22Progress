using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Interfaces;

namespace Snt22Progress.BussinesLogic.Services
{
	/// <summary>
	/// Сервис, предоставляющий хеширование паролей
	/// </summary>
	public class PasswordHashService : IPasswordHashService
	{
		public async Task<string> GetPasswordHashWithSalt(string password, string salt)
		{
			return await Task.Run(() =>
			{
				HashAlgorithm algorithm = new SHA256Managed();
				byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
				byte[] saltBytes = Encoding.UTF8.GetBytes(salt);


				byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + saltBytes.Length];

				for (int i = 0; i < passwordBytes.Length; i++)
				{
					passwordWithSaltBytes[i] = passwordBytes[i];
				}
				for (int i = 0; i < salt.Length; i++)
				{
					passwordWithSaltBytes[passwordBytes.Length + i] = saltBytes[i];
				}

				return Convert.ToBase64String(algorithm.ComputeHash(passwordWithSaltBytes));
			});
		}
	}
}
