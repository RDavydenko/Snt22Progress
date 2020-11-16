using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Snt22Progress.BussinesLogic.Services
{
	/// <summary>
	/// Класс настроек аутентификации
	/// </summary>
	public class AuthSettings
    {
        /// <summary>
        /// Если равно false, то SSL при отправке токена не используется.
        /// Однако данный вариант установлен только дя тестирования.
        /// В реальном приложении все же лучше использовать передачу данных по протоколу https
        /// </summary>
        public bool RequireHttpsMetadata { get; } = false;


        /// <summary>
        ///  Указывает, будет ли валидироваться издатель при валидации токена
        /// </summary>
        public bool ValidateIssuer { get; } = true;

        /// <summary>
        /// Строка, представляющая издателя
        /// </summary>
        public string Issuer { get; }
 
        /// <summary>
        /// Будет ли валидироваться потребитель токена
        /// </summary>
        public bool ValidateAudience { get; } = true;

        /// <summary>
        /// Потребитель токена
        /// </summary>
		public string Audience { get; }

		/// <summary>
		/// Будет ли валидироваться время существования
		/// </summary>
		public bool ValidateLifetime { get; } = false;

        /// <summary>
        /// Время жизни токена в минутах
        /// </summary>
		public int Lifetime { get; }

		/// <summary>
		/// Установка ключа безопасности
		/// </summary>
		public string IssuerSigningKey { get; }

        /// <summary>
        /// Валидация ключа безопасности
        /// </summary>
        public bool ValidateIssuerSigningKey { get; } = true;

		public AuthSettings(IConfiguration configuration) 
		{
			if (configuration != null)
			{
                var requireHttpsMetadata = configuration.GetSection("AuthSettings")?.GetSection("RequireHttpsMetadata")?.Value;
                if (string.IsNullOrEmpty(requireHttpsMetadata) == false
                    && string.IsNullOrWhiteSpace(requireHttpsMetadata) == false
                    && bool.TryParse(requireHttpsMetadata, out var _requireHttpsMetadata))
                {
                    RequireHttpsMetadata = _requireHttpsMetadata;
                }

                var validateIssuer = configuration.GetSection("AuthSettings")?.GetSection("ValidateIssuer")?.Value;
                if (string.IsNullOrEmpty(validateIssuer) == false
                    && string.IsNullOrWhiteSpace(validateIssuer) == false
                    && bool.TryParse(validateIssuer, out var _validateIssuer))
				{
                    ValidateIssuer = _validateIssuer;
				}

                var issuer = configuration.GetSection("AuthSettings")?.GetSection("Issuer")?.Value;
                if (string.IsNullOrEmpty(issuer) == false
                    && string.IsNullOrWhiteSpace(issuer) == false)
                {
                    Issuer = issuer;
                }

                var validateAudience = configuration.GetSection("AuthSettings")?.GetSection("ValidateAudience")?.Value;
                if (string.IsNullOrEmpty(validateAudience) == false
                    && string.IsNullOrWhiteSpace(validateAudience) == false
                    && bool.TryParse(validateAudience, out var _validateAudience))
                {
                    ValidateAudience = _validateAudience;
                }

                var audience = configuration.GetSection("AuthSettings")?.GetSection("Audience")?.Value;
                if (string.IsNullOrEmpty(audience) == false
                    && string.IsNullOrWhiteSpace(audience) == false)
                {
                    Audience = audience;
                }

                var validateLifetime = configuration.GetSection("AuthSettings")?.GetSection("ValidateLifetime")?.Value;
                if (string.IsNullOrEmpty(validateLifetime) == false
                    && string.IsNullOrWhiteSpace(validateLifetime) == false
                    && bool.TryParse(validateLifetime, out var _validateLifetime))
                {
                    ValidateLifetime = _validateLifetime;
                }

                var lifetime = configuration.GetSection("AuthSettings")?.GetSection("Lifetime")?.Value;
                if (string.IsNullOrEmpty(lifetime) == false
                    && string.IsNullOrWhiteSpace(lifetime) == false
                    && int.TryParse(lifetime, out var _lifetime))
                {
                    Lifetime = _lifetime;
                }

                var issuerSigningKey = configuration.GetSection("AuthSettings")?.GetSection("IssuerSigningKey")?.Value;
                if (string.IsNullOrEmpty(issuerSigningKey) == false
                    && string.IsNullOrWhiteSpace(issuerSigningKey) == false)
                {
                    IssuerSigningKey = issuerSigningKey;
                }

                var validateIssuerSigningKey = configuration.GetSection("AuthSettings")?.GetSection("ValidateIssuerSigningKey")?.Value;
                if (string.IsNullOrEmpty(validateIssuerSigningKey) == false
                    && string.IsNullOrWhiteSpace(validateIssuerSigningKey) == false
                    && bool.TryParse(validateIssuerSigningKey, out var _validateIssuerSigningKey))
                {
                    ValidateIssuerSigningKey = _validateIssuerSigningKey;
                }
            }
		}

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(IssuerSigningKey));
        }
    }
}
