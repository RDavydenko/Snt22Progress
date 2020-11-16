using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.Web.Api.Controllers
{
	[Produces("application/json")]
	public class BaseApiController : ControllerBase
	{
		public int? UserId
		{
			get
			{
				var claim = (ClaimsIdentity)User?.Identity;
				var userId = claim?.Claims?.Where(x => x.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;
				return userId != null ?
					int.Parse(userId)
					: (int?)null;
			}
		}

		public string UserEmail
		{
			get
			{
				var claim = (ClaimsIdentity)User?.Identity;
				var userEmail = claim?.Claims?.Where(x => x.Type == ClaimTypes.Email)?.FirstOrDefault()?.Value;
				return userEmail;
			}
		}

		public BaseApiController()
		{
		}
	}
}
