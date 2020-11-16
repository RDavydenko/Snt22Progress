using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models;
using Snt22Progress.Contracts.Models.Users;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.Logging;

namespace Snt22Progress.BussinesLogic.Services
{
	public class UsersService : IUsersService
	{
		private readonly IRepository<User, int> _userRepository;
		private readonly IMapper _mapper;
		private readonly IPasswordHashService _passwordHashService;
		private readonly IProgressLogger _progressLogger;

		public UsersService(IRepository<User, int> userRepository, IMapper mapper, IPasswordHashService passwordHashService, IProgressLogger progressLogger)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_passwordHashService = passwordHashService;
			_progressLogger = progressLogger;
		}

		public async Task<ResultResponse> RegisterAsync(UserRegisterDto dto)
		{
			try
			{
				var user = await _userRepository.GetAsync($"where lower(email) = lower('{dto.Email}')");
				if (user != null)
				{
					return ResultResponse.GetBadResponse(StatusCode.BadRequest, "Пользователь с таким Email уже зарегистрирован");
				}

				var newUser = _mapper.Map<User>(dto);
				newUser.registered = DateTime.Now;
				newUser.salt = GenerateSalt();
				newUser.password_hash = await _passwordHashService.GetPasswordHashWithSalt(dto.Password, newUser.salt);

				var added = _userRepository.AddAsync(newUser);
				if (added != null)
				{
					return ResultResponse.GetSuccessResponse();
				}
				else
				{
					return ResultResponse.GetInternalServerErrorResponse();
				}
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, dto, GetType().Name, nameof(RegisterAsync));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}
		public async Task<ResultResponse<UserDto>> EditAsync(int userId, UserEditDto dto)
		{
			try
			{
				var user = await _userRepository.GetAsync(userId);
				if (user == null)
				{
					return ResultResponse<UserDto>.GetBadResponse(StatusCode.NotFound, "Пользователь с таким id не найден");
				}

				user.fname = dto.FName;
				user.sname = dto.SName;
				user.mname = dto.MName;
				user.age = dto.Age;
				user.address = dto.Address;
				user.area_number = dto.AreaNumber;

				var updated = await _userRepository.UpdateAsync(user);
				if (updated == null)
				{
					return ResultResponse<UserDto>.GetInternalServerErrorResponse();
				}

				var updatedDto = _mapper.Map<UserDto>(updated);
				return ResultResponse<UserDto>.GetSuccessResponse(updatedDto);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { userId, dto }, GetType().Name, nameof(EditAsync));
				return ResultResponse<UserDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<UserDto>> GetAsync(int userId)
		{
			try
			{
				var user = await _userRepository.GetAsync(userId);
				if (user == null)
				{
					return ResultResponse<UserDto>.GetBadResponse(StatusCode.NotFound, "Пользователь с таким id не найден");
				}
				var userDto = _mapper.Map<UserDto>(user);
				return ResultResponse<UserDto>.GetSuccessResponse(userDto);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, userId, GetType().Name, nameof(GetAsync));
				return ResultResponse<UserDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse> ChangePasswordAsync(int userId, ChangePasswordDto dto)
		{
			try
			{
				var user = await _userRepository.GetAsync(userId);
				if (user == null)
				{
					return ResultResponse.GetBadResponse(StatusCode.NotFound, "Пользователь с таким id не найден");
				}

				var oldHash = user.password_hash;
				var oldConfirmHash = await _passwordHashService.GetPasswordHashWithSalt(dto.OldPassword, user.salt);
				if (oldHash == oldConfirmHash) // Если хеши равны, то и пароли равны (если не менялся алгоритм хеширования и соль)
				{
					var newHash = await _passwordHashService.GetPasswordHashWithSalt(dto.NewPassword, user.salt);
					user.password_hash = newHash;
					var updated = await _userRepository.UpdateAsync(user);
					if (updated != null)
					{
						return ResultResponse.GetSuccessResponse();
					}
				}
				return ResultResponse.GetBadResponse(StatusCode.BadRequest, "Неверный старый пароль");

			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, userId, GetType().Name, nameof(ChangePasswordAsync));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}

		private string GenerateSalt()
		{
			var alphabet = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789!@#$%^&*()_+-=/<>?:;";
			var r = new Random();
			var length = 15;
			var sb = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				sb.Append(alphabet[r.Next(alphabet.Length)]);
			}
			return sb.ToString();
		}
	}
}
